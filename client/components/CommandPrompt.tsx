import React, { createRef, useContext, useCallback } from "react";
import { css } from "@emotion/core";
import { Input, Box } from ".";
import { CommandContext } from "./CommandContext";
import { MessageContext } from "./MessageContext";
import { useInputFocus } from "./useInputFocus";

type CommandPromptProps = {
  username: string;
};
export const CommandPrompt = ({ username }: CommandPromptProps) => {
  const { command, setCommand } = useContext(CommandContext);
  const { sendMessage, sendLocalMessage } = useContext(MessageContext);
  const inputRef = createRef<HTMLInputElement>();
  const prompt = username ? `${username}@local$` : `username?`;

  const onSubmit = useCallback(() => {
    sendLocalMessage(`${prompt} ${command}`);
    if (!command.trim()) {
      return;
    }
    sendMessage(command);
    setCommand("");
  }, [command, prompt, sendLocalMessage, sendMessage, setCommand]);
  useInputFocus(onSubmit, inputRef);

  return (
    <Box width={1}>
      {prompt} {command}█
      <Input
        aria-label="Enter Command"
        ref={inputRef}
        size={command.length || 1}
        value={command}
        onChange={(event) => {
          setCommand(event.target.value);
        }}
        type="text"
        css={css`
          opacity: 0.01;
          position: absolute;
          outline: none;
        `}
      />
    </Box>
  );
};
