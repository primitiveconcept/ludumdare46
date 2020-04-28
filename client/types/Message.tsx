import { Array, Union, Record, String, Literal, Static } from "runtypes";
import { Device } from "./Device";
import { Email } from "./Email";
import { PortscanProcess } from "./PortscanProcess";

export const TerminalMessage = Record({
  update: Literal("Terminal"),
  payload: Record({
    message: String,
  }),
});
export type TerminalMessage = Static<typeof TerminalMessage>;

export const ResourcesMessage = Record({
  update: Literal("Devices"),
  payload: Record({
    devices: Array(Device),
  }),
});
export type ResourcesMessage = Static<typeof ResourcesMessage>;

export const EmailsMessage = Record({
  update: Literal("Emails"),
  payload: Record({
    emails: Array(Email),
  }),
});
export type EmailsMessage = Static<typeof EmailsMessage>;

export const PortscanProcessMessage = Record({
  update: Literal("PortscanProcess"),
  payload: PortscanProcess,
});
export type PortscanProcessMessage = Static<typeof PortscanProcessMessage>;

export const Message = Union(
  TerminalMessage,
  ResourcesMessage,
  EmailsMessage,
  PortscanProcessMessage,
);
export type Message = Static<typeof Message>;
