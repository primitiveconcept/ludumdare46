import React from "react";
import { State } from "../../types/State";
import { Markdown } from "../library/Markdown";

type MessagesProgramProps = {
  messages: State["messages"];
};
export const MessagesProgram = React.memo(
  ({ messages }: MessagesProgramProps) => {
    return (
      <div data-test="messages">
        {messages.map((message, index) => (
          <div key={index}>
            <Markdown>{message}</Markdown>
          </div>
        ))}
      </div>
    );
  },
);
