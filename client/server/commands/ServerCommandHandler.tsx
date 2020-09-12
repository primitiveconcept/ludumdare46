import { WorkerState } from "../types/WorkerState";
import { Draft } from "immer";

type CommandProps = {
  args: Array<string | undefined>;
  addMessage: (message: string) => void;
  command: string;
  // startProcess: (process: MailProcess) => void;
  draft: Draft<WorkerState>;
};
export type ServerCommandHandler = (props: CommandProps) => void;
