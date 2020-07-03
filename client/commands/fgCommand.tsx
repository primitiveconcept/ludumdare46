import { CommandProps } from "./commandProps";

export const fgCommand = ({
  command,
  args,
  state,
  addMessage,
  setOpenProcessId,
}: CommandProps) => {
  const id = args[0];
  if (!id) {
    addMessage(`${command}: requires a process id`);
  } else {
    const process = state.processes.find((proc) => proc.id === id);
    if (process) {
      setOpenProcessId(process.id);
    } else {
      addMessage(`${command}: process id ${id} not found`);
    }
  }
};
