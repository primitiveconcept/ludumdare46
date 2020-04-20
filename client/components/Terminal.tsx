import React from "react";
import { css } from "@emotion/core";

type TerminalProps = {
  children: React.ReactNode;
};
export const Terminal = ({ children }: TerminalProps) => {
  return (
    <div
      css={css`
        /* stolen from http://aleclownes.com/2017/02/01/crt-display.html, need to come up with my own */
        /* animation: textShadow 1.6s infinite; */
      `}
    >
      {children}
    </div>
  );
};
