import { Array, Union, Record, String, Literal } from "runtypes";
import { Device } from "./Device";
import { Process } from "./Process";
import { Email } from "./Email";

export const TerminalMessage = Record({
  update: Literal("Terminal"),
  payload: Record({
    message: String,
  }),
});

export const ProcessesMessage = Record({
  update: Literal("Processes"),
  payload: Record({
    processes: Array(Process),
  }),
});

export const ResourcesMessage = Record({
  update: Literal("Devices"),
  payload: Record({
    devices: Array(Device),
  }),
});

export const EmailsMessage = Record({
  update: Literal("Emails"),
  payload: Record({
    emails: Array(Email),
  }),
});

export const MessageData = Union(
  TerminalMessage,
  ResourcesMessage,
  ProcessesMessage,
);
