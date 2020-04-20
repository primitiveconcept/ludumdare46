import "core-js/stable";
import { setAutoFreeze } from "immer";
import { Global, css } from "@emotion/core";
import React, { useMemo, useState, createRef, useEffect } from "react";
import Head from "next/head";
import {
  Box,
  Grid,
  Messages,
  CommandPrompt,
  Status,
  Terminal,
  UsernamePrompt,
  ResourcesBar,
  DevicesBar,
} from "../components";
import { CommandContext } from "../components/CommandContext";
import { useSession } from "../components/useSession";
import { useStore } from "../components/useStore";
import { debounce } from "lodash";
import { useCommandHistory } from "../components/useCommandHistory";

setAutoFreeze(false);

export const Index = () => {
  const [username, setUsername] = useSession();
  const { readyState, sendCommand, state } = useStore(username);
  const [command, setCommand] = useState("");
  const scrollRef = createRef<HTMLDivElement>();
  const { setPrevCommand, setNextCommand } = useCommandHistory(
    state.commandHistory,
    setCommand,
  );

  useEffect(() => {
    if (scrollRef.current) {
      scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
    }
  }, [scrollRef, state.messages]);

  useEffect(() => {
    const element = scrollRef.current;
    const onScroll = debounce(() => {
      console.log("scroll");
    }, 500);

    // never scrolljack. unless it's for aesthetic, then it's perfectly fine
    if (element) {
      element.addEventListener("scroll", onScroll);
    }

    return () => {
      if (element) {
        element.removeEventListener("scroll", onScroll);
      }
    };
  }, [scrollRef]);

  const commandContextValue = useMemo(() => {
    return {
      command,
      setCommand,
      sendCommand,
      setPrevCommand,
      setNextCommand,
    };
  }, [command, sendCommand, setNextCommand, setPrevCommand]);

  return (
    <CommandContext.Provider value={commandContextValue}>
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
              input {
                color: #43d731;
                font-size: 20px;
                margin: 0;
                font-family: "Fira Code", monospace;
              }
              button {
                padding: 0;
                border: 0;
                background-color: transparent;
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
              a,
              button {
                color: #bff3b8;
                text-decoration: none;
                &:hover {
                  color: white;
                }
              }
            `}
          />
          <Box gridArea="leftbar" padding={4}>
            {state.resources && <ResourcesBar resources={state.resources} />}
            {state.devices && <DevicesBar devices={state.devices} />}
          </Box>
          <Box
            ref={scrollRef}
            css={css`
              overflow-y: scroll;
            `}
            gridArea="main"
            padding={4}
          >
            <Status readyState={readyState} />
            <Messages messages={state.messages} />
            {username ? (
              <CommandPrompt username={username} />
            ) : (
              <UsernamePrompt setUsername={setUsername} />
            )}
          </Box>
        </Grid>
      </Terminal>
    </CommandContext.Provider>
  );
};

export default Index;
