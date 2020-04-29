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
      } else if (baseCommand === "mail" && !args.length) {
        startProcess({
          id: "mail",
          command: "mail",
          complete: false,
        });
        setOpenProcessId("mail");
      } else if (baseCommand === "foreground" || baseCommand === "fg") {
        const id = args[0];
        if (!id) {
          addMessage("foreground: requires a process id");
        } else {
          const process = state.processes.find((proc) => proc.id === id);
          if (process) {
            setOpenProcessId(process.id);
          } else {
            addMessage(`process id ${id} not found`);
          }
        }
      } else if (baseCommand === "background" || baseCommand === "bg") {
        setOpenProcessId(null);
      } else if (baseCommand === "process" || baseCommand === "ps") {
        addMessage(
          `| ID | COMMAND |
          |----|---------|
          ${state.processes
            .map((process) => {
              return `| ${process.id} | ${process.command} |`;
            })
            .join("  \n")}`,
        );
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
      state.processes,
      username,
    ],
  );
  return sendCommand;
};
