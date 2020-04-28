import React from "react";
import { Box } from "..";
import { CommandLink } from "../library/CommandLink";
import { Process } from "../../types";

type ProcessesPanelProps = {
  processes: Process[];
};
export const ProcessesPanel = ({ processes }: ProcessesPanelProps) => {
  return (
    <div>
      <Box>Processes</Box>
      {processes.map((process) => {
        const progress =
          "progress" in process && process.progress != null
            ? ` (${process.progress}%)`
            : "";
        return (
          <CommandLink href={`foreground ${process.id}`} key={process.command}>
            {process.command.split(" ")[0]}
            {progress}
          </CommandLink>
        );
      })}
    </div>
  );
};
