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
  });
  const {
    lastMessage,
    readyState,
    sendMessage: socketSendMessage,
  } = useSocket();

  useEffect(() => {
    if (!username) {
      return;
    }

    if (readyState === ReadyState.OPEN) {
      socketSendMessage(`internal_login ${username}`);
      if (!initial.current) {
        setState((draft) => {
          draft.messages.push(
            `Authenticating with public key "imported-133+ssh-key"`,
          );
          draft.messages.push(`Logged in as ${username}`);
        });
      }
      initial.current = true;
    }
  }, [readyState, setState, socketSendMessage, username]);

  const sendMessage = useCallback(
    (command: string) => {
      setState((draft) => {
        draft.commandHistory.push(command);
      });
      socketSendMessage(command);
    },
    [setState, socketSendMessage],
  );
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

  return { lastMessage, readyState, sendMessage, state, setState };
};
