import { useCallback } from "react";
import { helpCommand } from "../commands/helpCommand";
import { State } from "../types";
import { MailProcess } from "../types/MailProcess";
import table from "markdown-table";
import { useFiles } from "./useFiles";
import { joinPath } from "../lib/joinPath";

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
          addMessage(`${baseCommand}: requires a process id`);
        } else {
          const process = state.processes.find((proc) => proc.id === id);
          if (process) {
            setOpenProcessId(process.id);
          } else {
            addMessage(`${baseCommand}: process id ${id} not found`);
          }
        }
      } else if (baseCommand === "background" || baseCommand === "bg") {
        setOpenProcessId(null);
      } else if (baseCommand === "process" || baseCommand === "ps") {
        addMessage(
          table([
            ["ID", "COMMAND"],
            ...state.processes.map((process) => {
              return [process.id, process.command];
            }),
          ]),
        );
      } else if (baseCommand === "cd") {
        const path = args[0];
        if (!path || path === ".") {
          return;
        }
        if (path === "/") {
          setCwd("/");
          return;
        }
        if (path === "..") {
          if (state.cwd === "/") {
            return;
          }
          const newPath = state.cwd
            .split("/")
            .filter(Boolean)
            .slice(0, -1)
            .join("/");
          setCwd(`/${newPath}`);
        } else {
          const newPath = joinPath(state.cwd, path);
          if (
            newPath === "/" ||
            files?.find((file) => {
              return (
                file.type === "Folder" &&
                newPath === joinPath(file.path, file.name)
              );
            })
          ) {
            setCwd(newPath);
          } else {
            addMessage(`${baseCommand}: ${args[0]}: directory not found`);
          }
        }
      } else if (baseCommand === "ls") {
        if (!files) {
          return;
        }
        addMessage(
          files
            .filter((file) => file.path === state.cwd)
            .map((file) => `- ${file.name}${file.type === "Folder" ? "/" : ""}`)
            .join("\n"),
        );
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
