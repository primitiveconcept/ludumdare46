import { css, useTheme } from "@emotion/react";
import "core-js/stable";
import React, { useMemo, useState } from "react";
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
import { useStore } from "../hooks/useStore";
import { PortscanProgram } from "../components/Programs/PortscanProgram";
import { MailProgram } from "../components/Programs/MailProgram";
import { SshCrackProgram } from "../components/Programs/SshcrackProgram";
import { InfostealerProgram } from "../components/Programs/InfostealerProgram";

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
    ? state.processes.find((proc) => proc.id === openProcessId)
    : undefined;

  return (
    <CommandContext.Provider value={commandContextValue}>
      <TerminalOverlay />
      <Box
        css={css`
          min-height: 100vh;
        `}
      >
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
            {!!state.devices.length && <DevicesPanel devices={state.devices} />}
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
            {openProcess?.command === "portscan" && (
              <PortscanProgram process={openProcess} />
            )}
            {openProcess?.command === "sshcrack" && (
              <SshCrackProgram process={openProcess} />
            )}
            {openProcess?.command === "mail" && (
              <MailProgram emails={state.emails} />
            )}
            {openProcess?.command === "infostealer" && (
              <InfostealerProgram process={openProcess} />
            )}
            {!openProcess && (
              <>
                <Status
                  readyState={readyState}
                  thing={{ __typename: "Thing", name: "asdf" }}
                />
                <TerminalProgram messages={state.messages} />
                {username ? (
                  <CommandPrompt username={username} />
                ) : (
                  <UsernamePrompt setUsername={setUsername} />
                )}
              </>
            )}
          </Box>
        </>
      </Box>
    </CommandContext.Provider>
  );
};

export default Index;
