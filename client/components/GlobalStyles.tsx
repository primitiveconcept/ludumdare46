import { Global, css } from "@emotion/core";
import React from "react";

export const GlobalStyles = () => {
  return (
    <Global
      styles={css`
        body {
          margin: 0;
          text-shadow: 0.02956275843481219px 0 1px rgba(0, 30, 255, 0.5),
            -0.02956275843481219px 0 1px rgba(255, 0, 80, 0.3), 0 0 3px;
          background-color: black;
          background-image: radial-gradient(#111, #181818 120%);
          min-height: 100vh;
        }

        body,
        input {
          word-break: break-all;
          color: #43d731;
          font-size: 20px;
          margin: 0;
          font-family: "Fira Code", monospace;
        }

        pre {
          font-size: inherit;
          font-family: "Fira Code", monospace;
          margin: 0;
        }

        ul {
          margin-top: 0;
          margin-bottom: 0;
          padding-left: 0;
        }

        li {
          list-style-type: none;
        }

        a {
          color: #bff3b8;
          text-decoration: none;
          &:hover {
            color: white;
          }
        }
      `}
    />
  );
};
