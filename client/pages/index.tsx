import { css, useTheme } from "@emotion/react";
import "core-js/stable";
import React, { useEffect, useMemo, useState } from "react";
import {
  Box,
  CommandPrompt,
  DevicesPanel,
  TerminalProgram,
  ProcessesPanel,
  ResourcesPanel,
  Status,
  UsernamePrompt,
} from "../components";
import { CommandContext } from "../components/CommandContext";
import { EmailPanel } from "../components/Panels/EmailPanel";
import { MailProgram } from "../components/Programs/MailProgram";
import { TerminalOverlay } from "../components/TerminalOverlay";
import { useCommandHistory } from "../hooks/useCommandHistory";
import { useLocalCommands } from "../hooks/useLocalCommands";
import { useSession } from "../hooks/useSession";
import { useSteppedScroll } from "../hooks/useSteppedScroll";
import { useStore } from "../hooks/useStore";
import { Program } from "../types";

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
  const theme = useTheme();

  return (
    <CommandContext.Provider value={commandContextValue}>
      <TerminalOverlay />
      <Box
        css={css`
          min-height: 100vh;
        `}
      >
        {openProgram === "mail" && (
          <Box padding={1}>
            <MailProgram emails={state.emails} />
          </Box>
        )}

        {!openProgram && (
          <>
            <Box
              css={css`
                display: inline-block;
                width: ${theme.tileWidth * 24}px;
                position: sticky;
                top: 0;
                vertical-align: top;
              `}
              paddingLeft={2}
              paddingTop={1}
            >
              {!!state.resources && (
                <ResourcesPanel resources={state.resources} />
              )}
              {!!state.devices.length && (
                <DevicesPanel devices={state.devices} />
              )}
              {!!state.emails && <EmailPanel emails={state.emails} />}
              {!!state.processes.length && (
                <ProcessesPanel processes={state.processes} />
              )}
            </Box>
            <Box
              css={css`
                display: inline-block;
                width: calc(100% - ${theme.tileWidth * 24}px);
              `}
              paddingX={1}
              paddingY={1}
            >
              <Status readyState={readyState} />
              <TerminalProgram messages={state.messages} />
              {username ? (
                <CommandPrompt username={username} />
              ) : (
                <UsernamePrompt setUsername={setUsername} />
              )}
            </Box>
          </>
        )}
      </Box>
    </CommandContext.Provider>
  );
};

export default Index;
