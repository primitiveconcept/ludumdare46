import React, { createRef, useEffect, useContext, useCallback } from "react";
import { css } from "@emotion/core";
import { Input, Box } from ".";
import { Command } from "../types";
import { CommandContext } from "./CommandContext";
import { MessageContext } from "./MessageContext";
import keycode from "keycode";

const prompt = "threehams@local$";

type PromptProps = {
  sendMessage: (message: Command) => void;
};
export const Prompt = ({ sendMessage }: PromptProps) => {
  const { command, setCommand } = useContext(CommandContext);
  const { addMessage } = useContext(MessageContext);
  const inputRef = createRef<HTMLInputElement>();
  const onSubmit = useCallback(() => {
    addMessage(`${prompt} ${command}`);
    const [base, ...args] = command.trim().split(/ +/);
    if (!base) {
      return;
    }
    if (base === "ssh") {
      const [ip, username, password] = args;
      sendMessage({
        type: "SSH",
        payload: {
          ip,
          username,
          password,
        },
      });
    } else {
      addMessage(`${base}: command not found`);
    }
    setCommand("");
  }, [addMessage, command, sendMessage, setCommand]);
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
