import { Global } from "@emotion/core";
import React from "react";

export const Index: React.FunctionComponent = () => {
  return (
    <>
      <Global
        styles={`
          body {
            margin: 0;
            font-family: 'Open Sans', sans-serif;
          }
          canvas {
            display: block;
          }
        `}
      />
    </>
  );
};

export default Index;
