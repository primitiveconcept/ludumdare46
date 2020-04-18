import React, { useState, createRef, useEffect } from "react";
import { css } from "@emotion/core";
import { Input, Form } from ".";
import { Command } from "../types";

type PromptProps = {
  sendMessage: (message: Command) => void;
};
export const Prompt = ({ sendMessage }: PromptProps) => {
  const [text, setText] = useState("");
  const inputRef = createRef<HTMLInputElement>();
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
    <Form
      display="flex"
      justifyItems="start"
      width={1}
      onSubmit={(event) => {
        event.preventDefault();
        sendMessage(text);
        setText("");
      }}
    >
      &gt; {text}█
      <Input
        ref={inputRef}
        size={text.length || 1}
        value={text}
        onChange={(event) => {
          setText(event.target.value);
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
