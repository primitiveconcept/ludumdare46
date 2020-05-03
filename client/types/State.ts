import { Device } from "./Device";
import { Email } from "./Email";
import { Resources } from "./Resources";
import { Process } from "./Process";

export type State = {
  messages: string[];
  devices: Device[];
  resources: Resources | null;
  commandHistory: string[];
  processes: Process[];
  emails: Email[];
};
