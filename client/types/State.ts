import { Device } from "./Device";
import { Email } from "./Email";
import { Resources } from "./Resources";
import { PortscanProcess } from "./PortscanProcess";
import { MailProcess } from "./MailProcess";

export type State = {
  messages: string[];
  devices: Device[];
  resources: Resources | null;
  commandHistory: string[];
  processes: Array<PortscanProcess | MailProcess>;
  emails: Email[];
};
