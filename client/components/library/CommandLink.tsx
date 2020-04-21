import React from "react";
import { useContext } from "react";
import { CommandContext } from "../CommandContext";
import { SPACE_CHARACTER } from "./Markdown";
import { Anchor } from "./Anchor";

type CommandLinkProps = {
  children: string;
  href: string;
  highlightFocus?: boolean;
};
export const CommandLink = ({ href: hrefProp, children }: CommandLinkProps) => {
  const { sendCommand } = useContext(CommandContext);
  const href = hrefProp.replace(SPACE_CHARACTER, " ");
  return (
    <Anchor
      href={href}
      onClick={(event) => {
        event.preventDefault();
        event.currentTarget.blur();
        sendCommand(href);
      }}
    >
      {children}
    </Anchor>
  );
};
