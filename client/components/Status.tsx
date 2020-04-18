import React from "react";
import { ReadyState } from "react-use-websocket";

type StatusProps = {
  readyState: ReadyState;
};
export const Status = ({ readyState }: StatusProps) => {
  return (
    <div>
      Status:{" "}
      {readyState === ReadyState.CONNECTING
        ? "Connecting..."
        : readyState === ReadyState.CLOSED
        ? "Closed"
        : "Connected"}
    </div>
  );
};
