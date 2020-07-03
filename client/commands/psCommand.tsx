import { Process } from "../types";
import table from "markdown-table";
import { CommandProps } from "./commandProps";

export const psCommand = ({ addMessage, state }: CommandProps) => {
  return addMessage(
    table([
      ["ID", "COMMAND"],
      ...state.processes.map((process) => {
        return [process.id, process.command];
      }),
    ]),
  );
};
