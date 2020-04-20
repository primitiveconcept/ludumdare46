import { Static } from "runtypes";
import { Device } from "./Device";
import { Email } from "./Email";
import { Process } from "./Process";
import { Resources } from "./Resources";

export type State = {
  messages: string[];
  devices: Array<Static<typeof Device>>;
  resources: Resources | null;
  commandHistory: string[];
  processes: Array<Static<typeof Process>>;
  emails: Array<Static<typeof Email>>;
};
