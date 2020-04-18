import React from "react";
import { State } from "../types/State";
import { Markdown } from "./Markdown";

type MessagesProps = {
  messages: State["messages"];
};
export const Messages = ({ messages }: MessagesProps) => {
  return (
    <div>
      {messages.map((message, index) => (
        <div key={index}>
          <Markdown>{message}</Markdown>
        </div>
      ))}
    </div>
  );
};
