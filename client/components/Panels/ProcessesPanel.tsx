import React from "react";
import { Process } from "../../types/Process";
import { Static } from "runtypes";
import { Box } from "..";

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
            <Box paddingLeft={1}>{process.status}</Box>
          </div>
        );
      })}
    </div>
  );
};
