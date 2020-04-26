import React from "react";
import { useContext } from "react";
import { CommandContext } from "../CommandContext";
import { SPACE_CHARACTER } from "./Markdown";
import { Anchor } from "./Anchor";
import { css } from "@emotion/react";

type CommandLinkProps = {
  children: React.ReactNode;
  href: string;
  highlightFocus?: boolean;
  block?: boolean;
};
export const CommandLink = ({
  block,
  href: hrefProp,
  children,
}: CommandLinkProps) => {
  const { sendCommand } = useContext(CommandContext);
  const href = hrefProp.replace(SPACE_CHARACTER, " ");
  return (
    <Anchor
      css={css`
        display: ${block ? "block" : "inline-block"};
      `}
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
