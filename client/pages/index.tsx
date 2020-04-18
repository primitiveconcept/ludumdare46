import "core-js/stable";
import { setAutoFreeze } from "immer";
import { Global } from "@emotion/core";
import React, { useMemo, useEffect } from "react";
import useWebSocket, { ReadyState } from "react-use-websocket";
import Head from "next/head";
import { useImmer } from "use-immer";
import { Message } from "../types/Message";
import { Box, Grid, Messages, Prompt, Status } from "../components";

setAutoFreeze(false);

type Area = {};
type Item = {};

export type State = {
  messages: string[];
  area: Area | null;
  inventory: Item[];
};

export const Index = () => {
  const [state, setState] = useImmer<State>({
    messages: [],
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
  const [sendMessage, lastMessageUnsafe, readyState] = useWebSocket(
    "ws://localhost:31337/echo",
    options,
  );
  const lastMessage = Message.check(lastMessageUnsafe);
  useEffect(() => {
    if (readyState === ReadyState.OPEN) {
      sendMessage("wut");
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
            background: #111;
            color: green;
            margin: 0;
            font-family: 'Fira Code', monospace;
          }
          input, button {
            margin: 0;
            font-family: 'Fira Code', monospace;
          }
        `}
      />
      <Box
        overflow="auto"
        gridArea="leftbar"
        borderRight="1px solid green"
      ></Box>
      <Box overflow="auto" gridArea="main" paddingLeft={3}>
        <Status readyState={readyState} />
        <Messages messages={state.messages} />
      </Box>
      <Box
        overflow="auto"
        gridArea="rightbar"
        borderLeft="1px solid green"
      ></Box>
      <Box borderTop="1px solid green" overflow="auto" gridArea="prompt">
        <Prompt sendMessage={sendMessage} />
      </Box>
    </Grid>
  );
};

export default Index;
