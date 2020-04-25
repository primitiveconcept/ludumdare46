import React from "react";
import { State } from "../../types/State";
import { Markdown } from "../library/Markdown";

type TerminalProgramProps = {
  messages: State["messages"];
};
export const TerminalProgram = React.memo(
  ({ messages }: TerminalProgramProps) => {
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
