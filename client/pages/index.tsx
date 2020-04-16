import "core-js/stable";
import { setAutoFreeze } from "immer";
import { Global } from "@emotion/core";
import React, { useMemo, useEffect } from "react";
import useWebSocket, { ReadyState } from "react-use-websocket";
import Head from "next/head";
import { useImmer } from "use-immer";

setAutoFreeze(false);

type Command = {};
type Area = {};
type Item = {};

type State = {
  messages: string[];
  commands: Command[];
  area: Area | null;
  inventory: Item[];
};

export const Index = () => {
  const [state, setState] = useImmer<State>({
    messages: [],
    commands: [],
    area: null,
    inventory: [],
  });
  const options = useMemo(
    () => ({
      shouldReconnect: () => true,
      reconnectAttempts: 10,
      reconnectInterval: 3000,
    }),
    [],
  );
  const [sendMessage, lastMessage, readyState] = useWebSocket(
    "wss://echo.websocket.org/",
    options,
  );
  useEffect(() => {
    if (readyState === ReadyState.OPEN) {
      sendMessage("Hi");
    }
  }, [readyState, sendMessage]);
  useEffect(() => {
    if (lastMessage) {
      setState((draft) => {
        draft.messages.push(lastMessage.data);
      });
    }
  }, [lastMessage, setState]);

  return (
    <>
      <Head>
        <link
          href="https://fonts.googleapis.com/css?family=Open+Sans"
          rel="stylesheet"
        />
      </Head>
      <div>Status: {readyState}</div>
      <button
        onClick={() => sendMessage(lastMessage?.data === "Hi" ? "Hello" : "Hi")}
      >
        Hello
      </button>

      <Global
        styles={`
          body {
            margin: 0;
            font-family: 'Open Sans', sans-serif;
          }
          canvas {
            display: block;
          }
        `}
      />
      <div>
        {state.messages.map((message, index) => (
          <div key={index}>{message}</div>
        ))}
      </div>
    </>
  );
};

export default Index;
