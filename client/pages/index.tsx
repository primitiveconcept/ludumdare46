import "core-js/stable";
import { setAutoFreeze } from "immer";
import { Global, css } from "@emotion/core";
import React, { useMemo, useState, useEffect } from "react";
import Head from "next/head";
import {
  Box,
  Messages,
  CommandPrompt,
  Status,
  Terminal,
  UsernamePrompt,
  ResourcesBar,
  DevicesBar,
  Flex,
} from "../components";
import { CommandContext } from "../components/CommandContext";
import { useSession } from "../components/useSession";
import { useStore } from "../components/useStore";
import { useCommandHistory } from "../components/useCommandHistory";
import { useSteppedScroll } from "../components/useSteppedScroll";
import { TerminalOverlay } from "../components/TerminalOverlay";

setAutoFreeze(false);

export const Index = () => {
  const [username, setUsername] = useSession();
  const { readyState, sendCommand, state } = useStore(username);
  const [command, setCommand] = useState("");
  // const scrollRef = createRef<HTMLDivElement>();
  const { setPrevCommand, setNextCommand } = useCommandHistory(
    state.commandHistory,
    setCommand,
  );

  const scrollToBottom = useSteppedScroll();

  useEffect(() => {
    scrollToBottom();
  }, [scrollToBottom, state.messages]);

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
      <TerminalOverlay />
      <Terminal>
        <Flex
          css={css`
            min-height: 100vh;
          `}
        >
          <Box
            width={200}
            gridArea="leftbar"
            padding={4}
            css={css`
              position: sticky;
            `}
          >
            {state.resources && <ResourcesBar resources={state.resources} />}
            {state.devices && <DevicesBar devices={state.devices} />}
          </Box>
          <Box gridArea="main" padding={4}>
            <Status readyState={readyState} />
            <Messages messages={state.messages} />
            {username ? (
              <CommandPrompt username={username} />
            ) : (
              <UsernamePrompt setUsername={setUsername} />
            )}
          </Box>
        </Flex>
      </Terminal>
    </CommandContext.Provider>
  );
};

export default Index;
