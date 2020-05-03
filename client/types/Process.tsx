import { PortscanProcess } from "./PortscanProcess";
import { MailProcess } from "./MailProcess";
import { SshCrackProcess } from "./SshCrackProcess";

export type Process = PortscanProcess | MailProcess | SshCrackProcess;
