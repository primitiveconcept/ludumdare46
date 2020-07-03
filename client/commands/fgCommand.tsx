import { Process } from "../types";

type FgCommand = {
  id: string;
  baseCommand: string;
  processes: Process[];
  addMessage: (message: string) => void;
  setOpenProcessId: (processId: string | null) => void;
};
export const fgCommand = ({
  id,
  baseCommand,
  processes,
  addMessage,
  setOpenProcessId,
}: FgCommand) => {
  if (!id) {
    addMessage(`${baseCommand}: requires a process id`);
  } else {
    const process = processes.find((proc) => proc.id === id);
    if (process) {
      setOpenProcessId(process.id);
    } else {
      addMessage(`${baseCommand}: process id ${id} not found`);
    }
  }
};
