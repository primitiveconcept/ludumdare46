import React, { createRef, useEffect, useContext, useCallback } from "react";
import { css } from "@emotion/core";
import { Input, Box } from ".";
import { CommandContext } from "./CommandContext";
import { MessageContext } from "./MessageContext";
import keycode from "keycode";

const prompt = "threehams@local$";

export const Prompt = () => {
  const { command, setCommand } = useContext(CommandContext);
  const { sendMessage, sendLocalMessage } = useContext(MessageContext);
  const inputRef = createRef<HTMLInputElement>();

  const onSubmit = useCallback(() => {
    sendLocalMessage(`${prompt} ${command}`);
    if (!command.trim()) {
      return;
    }
    sendMessage(command);
    setCommand("");
  }, [command, sendLocalMessage, sendMessage, setCommand]);

  useEffect(() => {
    const focusInput = (event: KeyboardEvent) => {
      inputRef.current?.focus();
      if (event.keyCode === keycode.codes.enter) {
        onSubmit();
      }
    };
    document.addEventListener("keypress", focusInput);

    return () => {
      document.removeEventListener("keypress", focusInput);
    };
  }, [inputRef, onSubmit]);

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
