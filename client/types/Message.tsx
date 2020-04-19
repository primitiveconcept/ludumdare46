import { Array, Union, Record, String, Literal } from "runtypes";

const Status = String;

export const Device = Record({
  ip: String,
  status: Status,
  commands: Array(String),
});

const TerminalMessage = Record({
  update: Literal("Terminal"),
  payload: Record({
    message: String,
  }),
});

const ResourcesMessage = Record({
  update: Literal("Resources"),
  payload: Record({
    bitcoin: String,
    devices: Array(Device),
  }),
});

export const MessageData = Union(TerminalMessage, ResourcesMessage);
