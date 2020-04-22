import React from "react";
import { SPACE_CHARACTER } from "./Markdown";
import { Anchor } from "./Anchor";
import { css } from "@emotion/react";

type LinkProps = {
  children: React.ReactNode;
  href: string;
  onClick: React.MouseEventHandler;
  highlightFocus?: boolean;
  block?: boolean;
};
export const Link = ({
  href: hrefProp,
  onClick,
  children,
  block,
}: LinkProps) => {
  const href = hrefProp.replace(SPACE_CHARACTER, " ");
  return (
    <Anchor
      css={css`
        display: ${block ? "block" : "inline-block"};
      `}
      href={href}
      onClick={(event) => {
        event.preventDefault();
        onClick(event);
      }}
    >
      {children}
    </Anchor>
  );
};
