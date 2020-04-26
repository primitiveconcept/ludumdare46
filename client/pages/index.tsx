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
import { TerminalOverlay } from "../components/TerminalOverlay";
import { useCommandHistory } from "../hooks/useCommandHistory";
import { useLocalCommands } from "../hooks/useLocalCommands";
import { useSession } from "../hooks/useSession";
import { useSteppedScroll } from "../hooks/useSteppedScroll";
import { useStore } from "../hooks/useStore";
import { PortscanProgram } from "../components/Programs/PortscanProgram";
import { MailProgram } from "../components/Programs/MailProgram";

export const Index = () => {
  const [username, setUsername] = useSession();
  const [openProcessId, setOpenProcessId] = useState<string | null>(null);
  const {
    addMessage,
    addHistory,
    readyState,
    sendCommand: sendServerCommand,
    state,
    startProcess,
  } = useStore(username);
  const [command, setCommand] = useState("");
  const { setPrevCommand, setNextCommand } = useCommandHistory(
    state.commandHistory,
    setCommand,
  );
  const sendCommand = useLocalCommands({
    username,
    startProcess,
    sendCommand: sendServerCommand,
    setOpenProcessId,
    addMessage,
    addHistory,
    state,
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
  const openProcess = openProcessId
    ? state.processDetails[openProcessId]
    : undefined;

  return (
    <CommandContext.Provider value={commandContextValue}>
      <TerminalOverlay />
      <Box
        css={css`
          min-height: 100vh;
        `}
      >
        {openProcess && (
          <Box padding={1}>
            {openProcess.command === "portscan" && (
              <PortscanProgram process={openProcess} />
            )}
            {openProcess.command === "mail" && (
              <MailProgram emails={state.emails} />
            )}
          </Box>
        )}

        {!openProcess && (
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
