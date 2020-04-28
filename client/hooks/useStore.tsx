import { useImmer } from "use-immer";
import { State } from "../types/State";
import { useSocket } from "./useSocket";
import { useEffect, useCallback, useRef } from "react";
import { ReadyState } from "react-use-websocket";
import { MailProcess } from "../types/MailProcess";

/**
 * Set up local state to hold onto messages received from the server.
 *
 * Concerns:
 * - Listen to incoming messages and set state.
 * - Provide functions to other hooks to modify state.
 *
 */
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
    if (lastMessage.update === "Emails") {
      setState((draft) => {
        draft.emails = lastMessage.payload.emails;
      });
    }
    if (lastMessage.update === "PortscanProcess") {
      const newProcess = lastMessage.payload;
      setState((draft) => {
        const index = draft.processes.findIndex(
          (process) => process.id === newProcess.id,
        );
        if (index === -1) {
          draft.processes.push(newProcess);
        } else {
          draft.processes[index] = newProcess;
        }
      });
    }
  }, [lastMessage, setState]);

  // This used to do more...
  const sendCommand = sendMessage;

  const addMessage = useCallback(
    (message: string) => {
      setState((draft) => {
        draft.messages.push(message);
      });
    },
    [setState],
  );
  const addHistory = useCallback(
    (message: string) => {
      setState((draft) => {
        draft.commandHistory.push(message);
      });
    },
    [setState],
  );
  const startProcess = useCallback(
    (newProcess: MailProcess) => {
      setState((draft) => {
        const index = draft.processes.findIndex(
          (process) => process.id === newProcess.id,
        );
        if (index === -1) {
          draft.processes.push(newProcess);
        } else {
          draft.processes[index] = newProcess;
        }
      });
    },
    [setState],
  );

  return {
    addHistory,
    addMessage,
    startProcess,
    readyState,
    sendCommand,
    state,
    setState,
  };
};
