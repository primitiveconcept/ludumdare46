import React from "react";
import ReactMarkdown from "markdown-to-jsx";
import { CommandLink } from "./CommandLink";

export const SPACE_CHARACTER = new RegExp("\\|", "g");

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
