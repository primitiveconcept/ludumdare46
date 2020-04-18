import React, { createRef, useEffect, useContext, useCallback } from "react";
import { css } from "@emotion/core";
import { Input, Form } from ".";
import { Command } from "../types";
import { CommandContext } from "./CommandContext";
import { MessageContext } from "./MessageContext";

const prompt = "threehams@local$";

type PromptProps = {
  sendMessage: (message: Command) => void;
};
export const Prompt = ({ sendMessage }: PromptProps) => {
  const { command, setCommand } = useContext(CommandContext);
  const { addMessage } = useContext(MessageContext);
  const inputRef = createRef<HTMLInputElement>();
  const onSubmit = useCallback(
    (event: React.FormEvent) => {
      event.preventDefault();
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
    },
    [addMessage, command, sendMessage, setCommand],
  );
  useEffect(() => {
    const focusInput = () => {
      inputRef.current?.focus();
    };
    document.addEventListener("keypress", focusInput);

    return () => {
      document.removeEventListener("keypress", focusInput);
    };
  }, [inputRef]);

  return (
    <Form display="flex" justifyItems="start" width={1} onSubmit={onSubmit}>
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
    </Form>
  );
};
