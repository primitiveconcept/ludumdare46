import "core-js/stable";
import { setAutoFreeze } from "immer";
import { Global, css } from "@emotion/core";
import React, { useEffect } from "react";
import Head from "next/head";
import { useImmer } from "use-immer";
import {
  Box,
  Grid,
  Messages,
  Prompt,
  Status,
  Terminal,
  InventoryBar,
} from "../components";
import { useSocket } from "../components/useSocket";
import { useSession } from "../components/useSession";
import { Inventory } from "../types";

setAutoFreeze(false);

export type State = {
  messages: string[];
  inventory: Inventory;
};

export const Index = () => {
  const [state, setState] = useImmer<State>({
    messages: [],
    inventory: null,
  });
  const sessionId = useSession();
  const { lastMessage, readyState, sendMessage } = useSocket(sessionId);
  useEffect(() => {
    if (!lastMessage) {
      return;
    }
    const message = lastMessage.payload.message;
    if (message) {
      setState((draft) => {
        draft.messages.push(message);
      });
    }
    if (lastMessage.type === "INITIAL_STATE") {
      setState((draft) => {
        draft.inventory = {
          bitcoin: lastMessage.payload.bitcoin,
          knownDevices: lastMessage.payload.knownDevices,
        };
      });
    }
  }, [lastMessage, setState]);

  return (
    <Terminal>
      <Grid
        height="100vh"
        gridTemplateAreas={`"leftbar main"
                      "leftbar main"`}
        gridTemplateRows="1fr auto"
        gridTemplateColumns="300px 1fr"
      >
        <Head>
          <link
            href="https://fonts.googleapis.com/css2?family=Fira+Code:wght@500&display=swap"
            rel="stylesheet"
          />
        </Head>
        <Global
          styles={css`
            body {
              margin: 0;
            }

            body,
            input,
            button {
              color: #43d731;
              font-size: 20px;
              margin: 0;
              font-family: "Fira Code", monospace;
            }
            ul {
              margin-top: 0;
              margin-bottom: 0;
              padding-left: 0;
            }
            li {
              list-style-type: none;
            }
          `}
        />
        <Box overflow="auto" gridArea="leftbar" padding={4}>
          <InventoryBar inventory={state.inventory} />
        </Box>
        <Box overflow="auto" gridArea="main" padding={4}>
          <Status readyState={readyState} />
          <Messages messages={state.messages} />
          <Prompt sendMessage={sendMessage} />
        </Box>
      </Grid>
    </Terminal>
  );
};

export default Index;
