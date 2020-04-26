import React from "react";
import { Markdown } from "../library/Markdown";

type TerminalProgramProps = {
  messages: string[];
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
