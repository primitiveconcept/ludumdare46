import { useCallback } from "react";
import { helpCommand } from "../commands/helpCommand";
import { Program } from "../types";

type UseLocalCommands = {
  sendServerCommand: (command: string) => void;
  setOpenProgram: (program: Program | null) => void;
  addMessage: (message: string) => void;
  username: string;
};
export const useLocalCommands = ({
  sendServerCommand,
  setOpenProgram,
  addMessage,
  username,
}: UseLocalCommands) => {
  const sendCommand = useCallback(
    (command: string): void => {
      if (!command.trim()) {
        return;
      }
      const [baseCommand, ...args] = command.split(/ +/);
      if (baseCommand === "help") {
        addMessage(`${username}@local$ ${command}`);
        addMessage(helpCommand(args));
        return;
      }
      if (baseCommand === "mail") {
        addMessage(`${username}@local$ ${command}`);
        setOpenProgram("mail");
        return;
      }
      if (baseCommand === "close") {
        setOpenProgram(null);
        return;
      }
      sendServerCommand(command);
    },
    [addMessage, sendServerCommand, setOpenProgram, username],
  );
  return sendCommand;
};
