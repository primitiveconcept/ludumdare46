import React from "react";
import { AppProps } from "next/app";
import { ThemeProvider } from "@emotion/react";
import { range } from "lodash";

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <ThemeProvider
      theme={{
        tileWidth: 12,
        tileHeight: 26,
        spaceX: range(4).map((num) => num * 12),
        spaceY: range(4).map((num) => num * 26),
      }}
    >
      <Component {...pageProps} />
    </ThemeProvider>
  );
}

export default MyApp;
