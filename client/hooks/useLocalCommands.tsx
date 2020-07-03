import { useCallback } from "react";
import { State } from "../types";
import { MailProcess } from "../types/MailProcess";
import { useFiles } from "./useFiles";
import {
  mailCommand,
  helpCommand,
  fgCommand,
  psCommand,
  cdCommand,
  lsCommand,
} from "../commands";

type UseLocalCommands = {
  addHistory: (command: string) => void;
  addMessage: (message: string) => void;
  sendCommand: (command: string) => void;
  setOpenProcessId: (processId: string | null) => void;
  startProcess: (process: MailProcess) => void;
  state: State;
  username: string;
  setCwd: (cwd: string) => void;
};
export const useLocalCommands = ({
  addHistory,
  addMessage,
  sendCommand: sendCommandProp,
  setOpenProcessId,
  startProcess,
  state,
  username,
  setCwd,
}: UseLocalCommands) => {
  const files = useFiles(state.filesystems["8.8.8.8"]);

  const sendCommand = useCallback(
    (command: string): void => {
      const prompt = `${username}@local:${state.cwd}$`;
      if (!command.trim()) {
        addMessage(prompt);
        return;
      }
      const [baseCommand, ...args] = command.split(/ +/);

      addMessage(`${prompt} ${command}`);
      addHistory(command);
      if (baseCommand === "help") {
        return helpCommand({ args, addMessage });
      } else if (baseCommand === "mail" && !args.length) {
        return mailCommand({
          startProcess,
          setOpenProcessId,
        });
      } else if (baseCommand === "foreground" || baseCommand === "fg") {
        return fgCommand({
          id: args[0],
          baseCommand,
          addMessage,
          processes: state.processes,
          setOpenProcessId,
        });
      } else if (baseCommand === "background" || baseCommand === "bg") {
        setOpenProcessId(null);
      } else if (baseCommand === "process" || baseCommand === "ps") {
        return psCommand({
          addMessage,
          processes: state.processes,
        });
      } else if (baseCommand === "cd") {
        return cdCommand({
          baseCommand,
          path: args[0],
          addMessage,
          cwd: state.cwd,
          setCwd,
          files,
        });
      } else if (baseCommand === "ls") {
        return lsCommand({
          addMessage,
          cwd: state.cwd,
          files,
        });
      } else {
        sendCommandProp(command);
      }
    },
    [
      addMessage,
      username,
      addHistory,
      state.cwd,
      state.processes,
      startProcess,
      setOpenProcessId,
      setCwd,
      files,
      sendCommandProp,
    ],
  );
  return sendCommand;
};
