import { Message } from "../types";
import { tracerouteCommand } from "./commands/tracerouteCommand";
import { WorkerState } from "./types/WorkerState";
import produce, { enableMapSet, enablePatches, setAutoFreeze } from "immer";

setAutoFreeze(false);
enableMapSet();
enablePatches();

const worker = (self as unknown) as Worker;

const state: WorkerState = {
  nodes: new Map(),
};

worker.addEventListener("message", (event) => {
  const [command, ...args] = event.data.split(" ");
  if (command === "traceroute") {
    const messages: string[] = [];
    const addMessage = (message: string) => {
      messages.push(message);
    };
    produce(state, (draft) => {
      tracerouteCommand({ args, draft, addMessage, command });
    });

    const message: Message = {
      update: "Terminal",
      payload: {
        message: messages.join("\n"),
      },
    };
    worker.postMessage(message);
  } else {
    const message: Message = {
      update: "Terminal",
      payload: {
        message: `${command}: command not found`,
      },
    };
    worker.postMessage(message);
  }
});
