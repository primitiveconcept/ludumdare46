import { Message } from "../types";
import { portscanCommand, tracerouteCommand } from "./commands";
import { Component } from "./components";
import { ecs } from "./lib/ecs";
import { clearEventsSystem, portscanSystem } from "./systems";

const worker = (self as unknown) as Worker;

const world = ecs([]);
world.createEntity(undefined, { Events: { type: "Events", events: [] } });
world.createEntity(undefined, {
  Player: { type: "Player" },
  Location: {
    ip: "199.201.159.1",
    type: "Location",
  },
  KnownDevices: {
    items: [],
    type: "KnownDevices",
  },
});
const systems = [portscanSystem, clearEventsSystem];

worker.addEventListener("message", (event: { data: string }) => {
  const messages: string[] = [];
  const addMessage = (message: string) => {
    messages.push(message);
  };
  const addEvent = (component: Component) => {
    const eventEntity = world.with("Events")[0];
    const eventComponent = eventEntity.components.Events;
    eventComponent.events.push(component);
  };

  const [command, ...args] = event.data.split(" ");
  switch (command) {
    case "traceroute": {
      tracerouteCommand({ args, command, addMessage, addEvent });
      break;
    }
    case "portscan": {
      portscanCommand({ args, command, addMessage, addEvent });
      break;
    }
    default: {
      messages.push(`${command}: command not found`);
    }
  }

  if (messages.length) {
    const message: Message = {
      update: "Terminal",
      payload: {
        message: messages.join("\n"),
      },
    };
    worker.postMessage(message);
  }
});

const INTERVAL = 250;
const gameLoop = () => {
  const messages: string[] = [];
  const addMessage = (message: string) => {
    messages.push(message);
  };

  systems.forEach((system) => {
    system({ world, addMessage });
  });

  if (messages.length) {
    const message: Message = {
      update: "Terminal",
      payload: {
        message: messages.join("\n"),
      },
    };
    worker.postMessage(message);
  }

  // Normally a terrible idea, but we don't need per-frame
  // updates, or even consistent timing between frames.
  // Save some cycles.
  setTimeout(gameLoop, INTERVAL);
};

gameLoop();

const initialMessage: Message = {
  update: "Devices",
  payload: {
    devices: [
      {
        ip: "8.8.8.8",
        status: "disconnected",
        commands: ["[portscan](portscan|8.8.8.8)"],
      },
    ],
  },
};
worker.postMessage(initialMessage);
