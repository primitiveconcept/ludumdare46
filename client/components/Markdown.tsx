import React from "react";
import ReactMarkdown from "markdown-to-jsx";
import { useContext } from "react";
import { MessageContext } from "./MessageContext";

const prompt = "threehams@local$";
const SPACE_CHARACTER = new RegExp("\\|", "g");

type CommandLinkProps = {
  children: string;
  href: string;
};
const CommandLink = ({ href: hrefProp, children }: CommandLinkProps) => {
  const { sendLocalMessage, sendMessage } = useContext(MessageContext);
  const href = hrefProp.replace(SPACE_CHARACTER, " ");

  return (
    <a
      href={href}
      onClick={(event) => {
        event.preventDefault();
        event.currentTarget.blur();
        sendLocalMessage(`${prompt} ${href} `);
        sendMessage(href);
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
