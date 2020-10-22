import { MailProcess } from "../types/MailProcess";
import { State } from "../types";
import { useFiles } from "../hooks/useFiles";

type CommandProps = {
  addHistory: (command: string) => void;
  addMessage: (message: string) => void;
  clearHistory: () => void;
  args: Array<string | undefined>;
  command: string;
  files: ReturnType<typeof useFiles>;
  sendCommand: (command: string) => void;
  setCwd: (cwd: string) => void;
  setOpenProcessId: (processId: string | null) => void;
  startProcess: (process: MailProcess) => void;
  state: State;
};
export type CommandHandler = (props: CommandProps) => void;
