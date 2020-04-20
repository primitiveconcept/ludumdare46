import { useMemo } from "react";
import useWebSocket from "react-use-websocket";
import {
  TerminalMessage,
  ResourcesMessage,
  ProcessesMessage,
  EmailsMessage,
} from "../types/Message";
import { camelizeKeys } from "humps";
import { useRouter } from "next/router";

enum UpdateTypes {
  Terminal = "Terminal",
  Devices = "Devices",
  Processes = "Processes",
  Emails = "Emails",
}

// Controls connect, reconnect, and validation of messages
// Does nothing with state
export const useSocket = () => {
  let hostname = "";
  if (typeof window !== "undefined") {
    hostname = window.location.hostname;
  }
  const router = useRouter();
  const forceLocal = router.query.forceLocal;
  const forceProduction = router.query.forceProduction;

  // Intentionally prevent rerender after changing this
  // Otherwise we'll ask the server twice for state
  const options = useMemo(
    () => ({
      shouldReconnect: () => true,
      reconnectAttempts: 10,
      reconnectInterval: 3000,
    }),
    [],
  );

  let url;
  if (forceLocal) {
    url = "ws://localhost:31337/game";
  } else if (forceProduction) {
    url = "ws://dev.primitiveconcept.com:31337/game";
  } else {
    url = `ws://${hostname}:31337/game`;
  }
  const [sendMessage, lastMessageUnsafe, readyState] = useWebSocket(
    url,
    options,
  );
  const lastMessage = useMemo(() => {
    if (!lastMessageUnsafe) {
      return null;
    }

    const data: any = camelizeKeys(JSON.parse(lastMessageUnsafe.data));
    if (data?.update === UpdateTypes.Terminal) {
      return TerminalMessage.check(data);
    } else if (data?.update === UpdateTypes.Devices) {
      return ResourcesMessage.check(data);
    } else if (data?.update === UpdateTypes.Processes) {
      return ProcessesMessage.check(data);
    } else if (data?.update === UpdateTypes.Emails) {
      return EmailsMessage.check(data);
    }
    throw new Error(
      `Unsupported update: ${data?.update}. Valid updates are ${JSON.stringify(
        Object.values(UpdateTypes),
      )}. Message: ${JSON.stringify(data)}.`,
    );
  }, [lastMessageUnsafe]);
  const result = useMemo(() => ({ lastMessage, sendMessage, readyState }), [
    lastMessage,
    readyState,
    sendMessage,
  ]);
  return result;
};
