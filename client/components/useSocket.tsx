import { useMemo, useCallback, useEffect } from "react";
import useWebSocket, { ReadyState } from "react-use-websocket";
import { MessageData } from "../types/Message";
import { camelizeKeys } from "humps";

const forceProduction = false;
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
    forceProduction
      ? `ws://dev.primitiveconcept.com:31337/game`
      : `ws://${hostname}:31337/game`,
    options,
  );
  const sendMessage = useCallback(
    (command: string) => {
      sendMessageUnsafe(
        JSON.stringify({
          command,
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
      sendMessage("initial");
    }
  }, [readyState, sendMessage]);
  return result;
};
