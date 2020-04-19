import { useMemo, useEffect } from "react";
import useWebSocket, { ReadyState } from "react-use-websocket";
import { MessageData } from "../types/Message";
import { camelizeKeys } from "humps";

const forceProduction = false;
export const useSocket = () => {
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
  const [sendMessage, lastMessageUnsafe, readyState] = useWebSocket(
    forceProduction
      ? `ws://dev.primitiveconcept.com:31337/game`
      : `ws://${hostname}:31337/game`,
    options,
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
      sendMessage("internal_login");
    }
  }, [readyState, sendMessage]);
  return result;
};
