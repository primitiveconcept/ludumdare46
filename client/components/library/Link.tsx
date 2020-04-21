import React from "react";
import { SPACE_CHARACTER } from "./Markdown";
import { Anchor } from "./Anchor";

type LinkProps = {
  children: React.ReactNode;
  href: string;
  onClick: React.MouseEventHandler;
  highlightFocus?: boolean;
};
export const Link = ({ href: hrefProp, onClick, children }: LinkProps) => {
  const href = hrefProp.replace(SPACE_CHARACTER, " ");
  return (
    <Anchor
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
