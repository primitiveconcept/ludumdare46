import { Process } from "../types";
import table from "markdown-table";

type PsCommand = {
  addMessage: (message: string) => void;
  processes: Process[];
};
export const psCommand = ({ addMessage, processes }: PsCommand) => {
  return addMessage(
    table([
      ["ID", "COMMAND"],
      ...processes.map((process) => {
        return [process.id, process.command];
      }),
    ]),
  );
};
