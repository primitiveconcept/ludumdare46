import React from "react";
import { useContext } from "react";
import { CommandContext } from "./CommandContext";
import { SPACE_CHARACTER } from "./Markdown";

type CommandLinkProps = {
  children: string;
  href: string;
};
export const CommandLink = ({ href: hrefProp, children }: CommandLinkProps) => {
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
