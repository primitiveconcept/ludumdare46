import { ThemeProvider } from "@emotion/react";
import { setAutoFreeze } from "immer";
import { range } from "lodash";
import { AppProps } from "next/app";
import React from "react";
import { GlobalStyles } from "../components/GlobalStyles";

setAutoFreeze(false);

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <>
      <GlobalStyles />
      <ThemeProvider
        theme={{
          tileWidth: 12,
          tileHeight: 27,
          spaceX: range(4).map((num) => num * 12),
          spaceY: range(4).map((num) => num * 26),
        }}
      >
        <Component {...pageProps} />
      </ThemeProvider>
    </>
  );
}

export default MyApp;
