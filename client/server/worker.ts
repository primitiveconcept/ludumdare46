import { Message } from "../types";
import { tracerouteCommand } from "./commands/tracerouteCommand";
import { Component } from "./components";
import { ecs } from "./lib/ecs";

const worker = (self as unknown) as Worker;

const entities = ecs([]);
entities.createEntity({ Events: { type: "Events", events: [] } });
entities.createEntity({ Player: { type: "Player" } });

worker.addEventListener("message", (event: { data: string }) => {
  const messages: string[] = [];
  const addMessage = (message: string) => {
    messages.push(message);
  };
  const addEvent = (component: Component) => {
    const eventEntity = entities.withComponents("Events")[0];
    const eventComponent = eventEntity.components.Events;
    eventComponent.events.push(component);
  };

  const [command, ...args] = event.data.split(" ");
  switch (command) {
    case "traceroute": {
      tracerouteCommand({ args, command, addMessage, addEvent });
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

const INTERVAL = 50;
const gameLoop = () => {
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
