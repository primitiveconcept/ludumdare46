import { useImmer } from "use-immer";
import { State } from "../types/State";
import { useSocket } from "./useSocket";
import { useEffect, useCallback, useRef } from "react";
import { ReadyState } from "react-use-websocket";

export const useStore = (username: string) => {
  const initial = useRef<boolean>(false);
  const [state, setState] = useImmer<State>({
    messages: [],
    devices: [],
    resources: null,
    commandHistory: [],
    processes: [],
    emails: [],
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
    if (lastMessage.update === "Processes") {
      setState((draft) => {
        draft.processes = lastMessage.payload.processes;
      });
    }
    if (lastMessage.update === "Emails") {
      setState((draft) => {
        draft.emails = lastMessage.payload.emails;
      });
    }
  }, [lastMessage, setState]);

  const sendCommand = useCallback(
    (command: string) => {
      setState((draft) => {
        draft.commandHistory.push(command);
      });

      // remote commands
      sendMessage(command);
    },
    [setState, sendMessage],
  );
  const addMessage = useCallback(
    (message: string) => {
      setState((draft) => {
        draft.messages.push(message);
      });
    },
    [setState],
  );

  return { addMessage, readyState, sendCommand, state, setState };
};
