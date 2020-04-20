import { useImmer } from "use-immer";
import { State } from "../types/State";
import { useSocket } from "./useSocket";
import { useEffect, useCallback, useRef } from "react";
import { ReadyState } from "react-use-websocket";
import { commands } from "../commands";

export const useStore = (username: string) => {
  const initial = useRef<boolean>(false);
  const [state, setState] = useImmer<State>({
    messages: [],
    devices: [],
    resources: null,
    commandHistory: [],
  });
  const { lastMessage, readyState, sendMessage } = useSocket();

  useEffect(() => {
    if (!username) {
      return;
    }

    if (readyState === ReadyState.OPEN) {
      sendMessage(`internal_login ${username}`);
      if (!initial.current) {
        setState((draft) => {
          draft.messages.push(
            `Authenticating with public key "imported-133+ssh-key"`,
          );
          draft.messages.push(`Logged in as ${username}`);
          draft.messages.push(
            `Type or click [help](help) for a list of commands.`,
          );
        });
      }
      initial.current = true;
    }
  }, [readyState, setState, sendMessage, username]);

  useEffect(() => {
    if (!lastMessage) {
      return;
    }
    if (lastMessage.update === "Terminal") {
      setState((draft) => {
        draft.messages.push(lastMessage.payload.message);
      });
    }
    if (lastMessage.update === "Devices") {
      setState((draft) => {
        draft.devices = lastMessage.payload.devices;
      });
    }
  }, [lastMessage, setState]);

  const sendCommand = useCallback(
    (command: string) => {
      if (!command.trim()) {
        return;
      }
      setState((draft) => {
        draft.messages.push(`${username}@local$ ${command}`);
      });
      setState((draft) => {
        draft.commandHistory.push(command);
      });

      // local commands
      const [baseCommand, ...args] = command.split(/ +/);
      const handler = commands[baseCommand];
      if (handler) {
        setState((draft) => {
          draft.messages.push(handler(args));
        });
        return;
      }

      // remote commands
      sendMessage(command);
    },
    [setState, sendMessage, username],
  );

  return { readyState, sendCommand, state, setState };
};
