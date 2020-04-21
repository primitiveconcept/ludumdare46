import { Array, Union, Record, String, Literal, Static } from "runtypes";
import { Device } from "./Device";
import { Process } from "./Process";
import { Email } from "./Email";

export const TerminalMessage = Record({
  update: Literal("Terminal"),
  payload: Record({
    message: String,
  }),
});
export type TerminalMessage = Static<typeof TerminalMessage>;

export const ProcessesMessage = Record({
  update: Literal("Processes"),
  payload: Record({
    processes: Array(Process),
  }),
});
export type ProcessesMessage = Static<typeof ProcessesMessage>;

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

export const Message = Union(
  TerminalMessage,
  ResourcesMessage,
  ProcessesMessage,
  EmailsMessage,
);
export type Message = Static<typeof Message>;
