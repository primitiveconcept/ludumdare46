import "core-js/stable";
import { setAutoFreeze } from "immer";
import { css } from "@emotion/core";
import React, { useMemo, useState, useEffect } from "react";
import Head from "next/head";
import {
  Box,
  MessagesProgram,
  CommandPrompt,
  Status,
  UsernamePrompt,
  ResourcesPanel,
  DevicesPanel,
  Flex,
  ProcessesPanel,
} from "../components";
import { CommandContext } from "../components/CommandContext";
import { useSession } from "../hooks/useSession";
import { useStore } from "../hooks/useStore";
import { useCommandHistory } from "../hooks/useCommandHistory";
import { useSteppedScroll } from "../hooks/useSteppedScroll";
import { TerminalOverlay } from "../components/TerminalOverlay";
import { EmailPanel } from "../components/Panels/EmailPanel";
import { MailProgram } from "../components/Programs/MailProgram";
import { Program } from "../types";
import { useLocalCommands } from "../hooks/useLocalCommands";
import { GlobalStyles } from "../components/GlobalStyles";

setAutoFreeze(false);

export const Index = () => {
  const [username, setUsername] = useSession();
  const [openProgram, setOpenProgram] = useState<Program | null>(null);
  const {
    addMessage,
    addHistory,
    readyState,
    sendCommand: sendServerCommand,
    state,
  } = useStore(username);
  const [command, setCommand] = useState("");
  const { setPrevCommand, setNextCommand } = useCommandHistory(
    state.commandHistory,
    setCommand,
  );
  const sendCommand = useLocalCommands({
    username,
    sendCommand: sendServerCommand,
    setOpenProgram,
    addMessage,
    addHistory,
  });
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
      <GlobalStyles />
      <TerminalOverlay />
      <Box
        css={css`
          min-height: 100vh;
        `}
      >
        <Box
          css={css`
            display: inline-block;
            width: 220px;
            grid-area: "leftbar";
            position: sticky;
            top: 0;
            vertical-align: top;
          `}
          paddingLeft={3}
          paddingTop={3}
        >
          {!!state.resources && <ResourcesPanel resources={state.resources} />}
          {!!state.devices.length && <DevicesPanel devices={state.devices} />}
          {!!state.emails && <EmailPanel emails={state.emails} />}
          {!!state.processes.length && (
            <ProcessesPanel processes={state.processes} />
          )}
        </Box>
        <Box
          css={css`
            display: inline-block;
            grid-area: "main";
            width: calc(100% - 220px);
          `}
          padding={3}
        >
          {!openProgram && (
            <>
              <Status readyState={readyState} />
              <MessagesProgram messages={state.messages} />
              {username ? (
                <CommandPrompt username={username} />
              ) : (
                <UsernamePrompt setUsername={setUsername} />
              )}
            </>
          )}
          {openProgram === "mail" && <MailProgram emails={state.emails} />}
        </Box>
      </Box>
    </CommandContext.Provider>
  );
};

export default Index;
