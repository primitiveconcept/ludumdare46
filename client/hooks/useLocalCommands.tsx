import { useCallback } from "react";
import { helpCommand } from "../commands/helpCommand";
import { Program } from "../types";

type UseLocalCommands = {
  sendCommand: (command: string) => void;
  setOpenProgram: (program: Program | null) => void;
  addMessage: (message: string) => void;
  username: string;
  addHistory: (command: string) => void;
};
export const useLocalCommands = ({
  sendCommand: sendCommandProp,
  setOpenProgram,
  addMessage,
  username,
  addHistory,
}: UseLocalCommands) => {
  const sendCommand = useCallback(
    (command: string): void => {
      if (!command.trim()) {
        addMessage(`${username}@local$`);
        return;
      }
      const [baseCommand, ...args] = command.split(/ +/);

      // commands without local echo first
      if (baseCommand === "close") {
        setOpenProgram(null);
        return;
      }

      addMessage(`${username}@local$ ${command}`);
      addHistory(command);
      if (baseCommand === "help") {
        addMessage(helpCommand(args));
        addHistory(command);
      } else if (baseCommand === "mail" && !args.length) {
        setOpenProgram("mail");
        addHistory(command);
      } else {
        sendCommandProp(command);
      }
    },
    [addHistory, addMessage, sendCommandProp, setOpenProgram, username],
  );
  return sendCommand;
};
