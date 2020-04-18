import { Array, Union, Record, String, Literal } from "runtypes";

const Status = String;
// const Permission = Union(Literal("None"), Literal("User"), Literal("Root"));

// const Command = Union(
//   Literal("portscan"),
//   Literal("ssh"),
//   Literal("sshcrack"),
//   Literal("disconnect"),
// );
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
