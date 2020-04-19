import { Array, Union, Record, String, Literal } from "runtypes";

const Status = String;

export const Device = Record({
  ip: String,
  status: Status,
  commands: Array(String),
});

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
