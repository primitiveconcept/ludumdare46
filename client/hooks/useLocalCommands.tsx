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
        return;
      }
      addHistory(command);
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
      addMessage(`${username}@local$ ${command}`);
      sendCommandProp(command);
    },
    [addHistory, addMessage, sendCommandProp, setOpenProgram, username],
  );
  return sendCommand;
};
