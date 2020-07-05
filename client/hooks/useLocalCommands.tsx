import { useCallback } from "react";
import { State } from "../types";
import { MailProcess } from "../types/MailProcess";
import { useFiles } from "./useFiles";
import { commands } from "../commands";

type UseLocalCommands = {
  addHistory: (command: string) => void;
  clearHistory: () => void;
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
      const [command, ...args] = fullCommand.split(/ +/);
      if (!command) {
        addMessage(prompt);
        return;
      }
      const commandProps = {
        command,
        args,
        files,
        ...props,
      };

      addMessage(`${prompt} ${fullCommand}`);
      addHistory(fullCommand);
      if (commands[command]) {
        commands[command]?.(commandProps);
      } else {
        sendCommandProp(fullCommand);
      }
    },
    [addHistory, addMessage, files, prompt, props, sendCommandProp],
  );
  return sendCommand;
};
