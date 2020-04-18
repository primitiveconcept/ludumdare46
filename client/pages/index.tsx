import "core-js/stable";
import { setAutoFreeze } from "immer";
import { Global } from "@emotion/core";
import React, { useMemo, useEffect, useCallback } from "react";
import useWebSocket, { ReadyState } from "react-use-websocket";
import Head from "next/head";
import { useImmer } from "use-immer";
import { Message, MessageData } from "../types/Message";
import { Box, Grid, Messages, Prompt, Status } from "../components";
import { Command } from "../types";
import { v4 as uuidV4 } from "uuid";
import { useCookies } from "react-cookie";

setAutoFreeze(false);

type Area = {};
type Item = {};

export type State = {
  messages: string[];
  area: Area | null;
  inventory: Item[];
};

const useSession = () => {
  const [cookies, setCookie] = useCookies();
  let sessionId: string | undefined = cookies.sessionId;
  if (sessionId) {
    return sessionId;
  }
  const id = uuidV4();
  setCookie("sessionId", id);
  return id;
};

const useSocket = (sessionId: string) => {
  const options = useMemo(
    () => ({
      shouldReconnect: () => true,
      reconnectAttempts: 10,
      reconnectInterval: 3000,
    }),
    [],
  );

  const [sendMessageUnsafe, lastMessageUnsafe, readyState] = useWebSocket(
    "ws://localhost:31337/echo",
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
      ? MessageData.check(JSON.parse(lastMessageUnsafe.data))
      : null;
  }, [lastMessageUnsafe]);
  const result = useMemo(() => ({ lastMessage, sendMessage, readyState }), [
    lastMessage,
    readyState,
    sendMessage,
  ]);
  return result;
};

export const Index = () => {
  const [state, setState] = useImmer<State>({
    messages: [],
    area: null,
    inventory: [],
  });
  const sessionId = useSession();
  const { lastMessage, readyState, sendMessage } = useSocket(sessionId);
  useEffect(() => {
    if (readyState === ReadyState.OPEN) {
      sendMessage({ type: "INITIAL_STATE" });
    }
  }, [readyState, sendMessage]);
  useEffect(() => {
    const message = lastMessage?.payload.message;
    if (message) {
      setState((draft) => {
        draft.messages.push(message);
      });
    }
  }, [lastMessage, setState]);

  return (
    <Grid
      height="100vh"
      gridTemplateAreas={`"leftbar main rightbar"
                      "leftbar prompt rightbar"`}
      gridTemplateRows="1fr auto"
      gridTemplateColumns="200px 1fr 200px"
    >
      <Head>
        <link
          href="https://fonts.googleapis.com/css2?family=Fira+Code:wght@500&display=swap"
          rel="stylesheet"
        />
      </Head>
      <Global
        styles={`
          body {
            background-color: black;
            background-image: radial-gradient(
              #080808,
              #111 120%
            );
            height: 100vh;
            margin: 0;
          }

          body,
          input,
          button {
            color: #43D731;
            font-size: 20px;
            margin: 0;
            font-family: "Fira Code", monospace;
            text-shadow: 0 0 5px #43D731;
          }

          body::after {
            content: "";
            position: absolute;
            pointer-events: none;
            top: 0;
            left: 0;
            width: 100vw;
            height: 100vh;
            background: repeating-linear-gradient(
              0deg,
              rgba(black, 0.15),
              rgba(black, 0.15) 1px,
              transparent 1px,
              transparent 2px
            );
          }

        `}
      />
      <Box
        overflow="auto"
        gridArea="leftbar"
        borderRight="3px solid #43D731"
      ></Box>
      <Box overflow="auto" gridArea="main" padding={4}>
        <Status readyState={readyState} />
        <Messages messages={state.messages} />
      </Box>
      <Box
        overflow="auto"
        gridArea="rightbar"
        borderLeft="3px solid #43D731"
      ></Box>
      <Box borderTop="3px solid #43D731" overflow="auto" gridArea="prompt">
        <Prompt sendMessage={sendMessage} />
      </Box>
    </Grid>
  );
};

export default Index;
