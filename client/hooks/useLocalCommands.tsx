import { useCallback } from "react";
import { useFiles } from "./useFiles";
import {
  mailCommand,
  helpCommand,
  fgCommand,
  psCommand,
  cdCommand,
  lsCommand,
  bgCommand,
} from "../commands";
import { MailProcess } from "../types/MailProcess";
import { State } from "../types";

type UseLocalCommands = {
  addHistory: (command: string) => void;
  addMessage: (message: string) => void;
  sendCommand: (command: string) => void;
  setCwd: (cwd: string) => void;
  setOpenProcessId: (processId: string | null) => void;
  startProcess: (process: MailProcess) => void;
  state: State;
  username: string;
};
export const useLocalCommands = (props: UseLocalCommands) => {
  const {
    username,
    state,
    addMessage,
    addHistory,
    sendCommand: sendCommandProp,
  } = props;
  const files = useFiles(state.filesystems["8.8.8.8"]);
  const prompt = `${username}@local:${state.cwd}$`;

  const sendCommand = useCallback(
    (fullCommand: string): void => {
      if (!fullCommand.trim()) {
        addMessage(prompt);
        return;
      }
      const [command, ...args] = fullCommand.split(/ +/);
      const commandProps = {
        command,
        args,
        files,
        ...props,
      };

      addMessage(`${prompt} ${fullCommand}`);
      addHistory(fullCommand);
      if (command === "help") {
        return helpCommand(commandProps);
      } else if (command === "mail" && !args.length) {
        return mailCommand(commandProps);
      } else if (command === "foreground" || command === "fg") {
        return fgCommand(commandProps);
      } else if (command === "background" || command === "bg") {
        return bgCommand(commandProps);
      } else if (command === "process" || command === "ps") {
        return psCommand(commandProps);
      } else if (command === "cd") {
        return cdCommand(commandProps);
      } else if (command === "ls") {
        return lsCommand(commandProps);
      } else {
        sendCommandProp(fullCommand);
      }
    },
    [addHistory, addMessage, files, prompt, props, sendCommandProp],
  );
  return sendCommand;
};
