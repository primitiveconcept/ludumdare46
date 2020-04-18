import { useMemo, useCallback, useEffect } from "react";
import useWebSocket, { ReadyState } from "react-use-websocket";
import { MessageData } from "../types/Message";
import { Command } from "../types";
import { camelizeKeys } from "humps";

export const useSocket = (sessionId: string) => {
  let hostname = "";
  if (typeof window !== "undefined") {
    hostname = window.location.hostname;
  }
  const options = useMemo(
    () => ({
      shouldReconnect: () => true,
      reconnectAttempts: 10,
      reconnectInterval: 3000,
    }),
    [],
  );
  const [sendMessageUnsafe, lastMessageUnsafe, readyState] = useWebSocket(
    `ws://${hostname}:31337/echo`,
    options,
  );
  const sendMessage = useCallback(
    (message: Command) => {
      sendMessageUnsafe(
        JSON.stringify({
          ...message,
          sessionId,
        }),
      );
    },
    [sendMessageUnsafe, sessionId],
  );
  const lastMessage = useMemo(() => {
    return lastMessageUnsafe
      ? MessageData.check(camelizeKeys(JSON.parse(lastMessageUnsafe.data)))
      : null;
  }, [lastMessageUnsafe]);
  const result = useMemo(() => ({ lastMessage, sendMessage, readyState }), [
    lastMessage,
    readyState,
    sendMessage,
  ]);
  useEffect(() => {
    if (readyState === ReadyState.OPEN) {
      sendMessage({ type: "INITIAL_STATE" });
    }
  }, [readyState, sendMessage]);
  return result;
};
