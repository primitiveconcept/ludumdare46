import React, { useState } from "react";
import { SendMessage } from "react-use-websocket";
import { css } from "@emotion/core";
import { Input, Form } from ".";

type PromptProps = {
  sendMessage: SendMessage;
};
export const Prompt = ({ sendMessage }: PromptProps) => {
  const [text, setText] = useState("");
  return (
    <Form
      display="flex"
      width={1}
      onSubmit={(event) => {
        event.preventDefault();
        sendMessage(text);
        setText("");
      }}
    >
      <Input
        value={text}
        onChange={(event) => {
          setText(event.target.value);
        }}
        type="text"
        css={css`
          flex: 1 1 auto;
        `}
      />
    </Form>
  );
};
