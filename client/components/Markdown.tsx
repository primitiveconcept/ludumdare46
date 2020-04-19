import React from "react";
import ReactMarkdown from "markdown-to-jsx";
import { useContext } from "react";
import { CommandContext } from "./CommandContext";

const SPACE_CHARACTER = new RegExp("\\|", "g");

type CommandLinkProps = {
  children: string;
  href: string;
};
const CommandLink = ({ href: hrefProp, children }: CommandLinkProps) => {
  const { sendCommand } = useContext(CommandContext);
  const href = hrefProp.replace(SPACE_CHARACTER, " ");

  return (
    <a
      href={href}
      onClick={(event) => {
        event.preventDefault();
        event.currentTarget.blur();
        sendCommand(href);
      }}
    >
      {children}
    </a>
  );
};

type MarkdownProps = {
  children: string;
};
export const Markdown = ({ children }: MarkdownProps) => (
  <ReactMarkdown
    options={{
      overrides: {
        a: {
          component: CommandLink,
        },
      },
    }}
  >
    {children}
  </ReactMarkdown>
);
