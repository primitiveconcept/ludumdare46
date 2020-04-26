import { useCallback } from "react";
import { helpCommand } from "../commands/helpCommand";
import { State } from "../types";
import { MailProcess } from "../types/MailProcess";

type UseLocalCommands = {
  addHistory: (command: string) => void;
  addMessage: (message: string) => void;
  sendCommand: (command: string) => void;
  setOpenProcessId: (processId: string | null) => void;
  startProcess: (process: MailProcess) => void;
  state: State;
  username: string;
};
export const useLocalCommands = ({
  addHistory,
  addMessage,
  sendCommand: sendCommandProp,
  setOpenProcessId,
  startProcess,
  state,
  username,
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
        setOpenProcessId(null);
        return;
      }

      addMessage(`${username}@local$ ${command}`);
      addHistory(command);
      if (baseCommand === "help") {
        addMessage(helpCommand(args));
        addHistory(command);
      } else if (baseCommand === "mail" && !args.length) {
        startProcess({
          id: "mail",
          command: "mail",
        });
        setOpenProcessId("mail");
        addHistory(command);
      } else if (baseCommand === "foreground") {
        const id = args[0];
        if (!id) {
          addMessage("foreground: requires a process id");
        } else {
          const process = state.processDetails[id];
          if (process) {
            setOpenProcessId(process.id);
          } else {
            addMessage(`process id ${id} not found`);
          }
        }
        addHistory(command);
      } else if (baseCommand === "background") {
        setOpenProcessId(null);
      } else {
        sendCommandProp(command);
      }
    },
    [
      addHistory,
      addMessage,
      sendCommandProp,
      setOpenProcessId,
      startProcess,
      state.processDetails,
      username,
    ],
  );
  return sendCommand;
};
