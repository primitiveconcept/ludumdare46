import { useMemo, useEffect, useRef } from "react";
import useWebSocket, { ReadyState } from "react-use-websocket";
import { MessageData } from "../types/Message";
import { camelizeKeys } from "humps";

const forceProduction = false;
export const useSocket = (username: string) => {
  let hostname = "";
  if (typeof window !== "undefined") {
    hostname = window.location.hostname;
  }

  // Intentionally prevent rerender after changing this
  // Otherwise we'll ask the server twice for state
  const initial = useRef<boolean>(false);
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
    if (!username) {
      return;
    }

    if (readyState === ReadyState.OPEN) {
      if (!initial.current) {
        sendMessage(`internal_login ${username}`);
        initial.current = true;
      } else {
        sendMessage("internal_reconnect");
      }
    }
  }, [readyState, sendMessage, username]);
  return result;
};
