import { Array, Union, Record, String, Literal } from "runtypes";
import { Device } from "./Device";

export const TerminalMessage = Record({
  update: Literal("Terminal"),
  payload: Record({
    message: String,
  }),
});

export const ResourcesMessage = Record({
  update: Literal("Devices"),
  payload: Record({
    devices: Array(Device),
  }),
});

export const MessageData = Union(TerminalMessage, ResourcesMessage);
