import React, { useContext } from "react";
import Markdown from "markdown-to-jsx";
import { State } from "../types/State";
import { CommandContext } from "./CommandContext";

type CommandLinkProps = {
  children: string;
  href: string;
};
const CommandLink = ({ href, children }: CommandLinkProps) => {
  const { setCommand } = useContext(CommandContext);
  return (
    <a
      href={href}
      onClick={(event) => {
        event.preventDefault();
        setCommand(`${href} `);
      }}
    >
      {children}
    </a>
  );
};

type MessagesProps = {
  messages: State["messages"];
};
export const Messages = ({ messages }: MessagesProps) => {
  return (
    <div>
      {messages.map((message, index) => (
        <div key={index}>
          <Markdown
            options={{
              overrides: {
                a: {
                  component: CommandLink,
                },
              },
            }}
          >
            {message}
          </Markdown>
        </div>
      ))}
    </div>
  );
};
