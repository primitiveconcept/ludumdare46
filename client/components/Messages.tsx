import React from "react";
import { State } from "../pages/index";

type MessagesProps = {
  messages: State["messages"];
};
export const Messages = ({ messages }: MessagesProps) => {
  return (
    <div>
      {messages.map((message, index) => (
        <div key={index}>{message}</div>
      ))}
    </div>
  );
};
