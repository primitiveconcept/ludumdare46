import { useMemo } from "react";
import useWebSocket from "react-use-websocket";
import { Message } from "../types/Message";
import { camelizeKeys } from "humps";
import { useRouter } from "next/router";

type UnsafeMessage = Pick<Message, "update"> | { update?: string };

/**
 * Connect to the Websocket endpoint and hide away the
 * minutia of incoming messages.
 *
 * Concerns:
 * - Decide the URL to hit
 * - Connect, reconnect, and provide connection state
 * - Validate incoming message structure
 *
 */
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
  const {
    sendMessage,
    lastMessage: lastMessageUnsafe,
    readyState,
  } = useWebSocket(url, options);
  const lastMessage = useMemo(() => {
    if (!lastMessageUnsafe) {
      return null;
    }

    const updateNames: string[] = Message.alternatives.map(
      (record) => record.fields.update.value,
    );
    const camelized = camelizeKeys(
      JSON.parse(lastMessageUnsafe.data),
    ) as UnsafeMessage;
    const update = camelized.update;
    if (!update || !updateNames.includes(update)) {
      // eslint-disable-next-line no-console
      console.error(
        `Received unknown update type ${update}. Known updates: ${updateNames}`,
      );
      return null;
    }
    const Record = Message.alternatives.find(
      (record) => record.fields.update.value === update,
    )!;

    try {
      return Record.check(camelized);
    } catch (err) {
      // eslint-disable-next-line no-console
      console.error(
        "Message failed validation.",
        err.message,
        "in",
        err.key,
        "in message",
        camelized,
      );
      return null;
    }
  }, [lastMessageUnsafe]);
  const result = useMemo(() => ({ lastMessage, sendMessage, readyState }), [
    lastMessage,
    readyState,
    sendMessage,
  ]);
  return result;
};
