import { PortscanProcess } from "./PortscanProcess";
import { MailProcess } from "./MailProcess";
import { SshCrackProcess } from "./SshCrackProcess";
import { InfostealerProcess } from "./InfostealerProcess";

export type Process =
  | PortscanProcess
  | MailProcess
  | SshCrackProcess
  | InfostealerProcess;
