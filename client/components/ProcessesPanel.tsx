import React from "react";
import { Process } from "../types/Process";
import { Static } from "runtypes";

type ProcessesPanelProps = {
  processes: Array<Static<typeof Process>>;
};
export const ProcessesPanel = ({ processes }: ProcessesPanelProps) => {
  return (
    <div>
      {processes.map((process) => {
        return (
          <div key={process.command}>
            <div>{process.command.split(" ")[0]}</div>
            <div>&nbsp;{process.status}</div>
          </div>
        );
      })}
    </div>
  );
};
