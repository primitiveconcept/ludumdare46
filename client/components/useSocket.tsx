import { useMemo, useEffect, useRef } from "react";
import useWebSocket, { ReadyState } from "react-use-websocket";
import { TerminalMessage, ResourcesMessage } from "../types/Message";
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
    if (!lastMessageUnsafe) {
      return null;
    }
    const data: any = camelizeKeys(JSON.parse(lastMessageUnsafe.data));
    if (data?.update === "Terminal") {
      return TerminalMessage.check(data);
    } else if (data?.update === "Resources") {
      return ResourcesMessage.check(data);
    }
    throw new Error(`Unsupported message: ${JSON.stringify(data)}`);
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
