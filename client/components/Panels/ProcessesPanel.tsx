import React from "react";
import { Process } from "../../types/Process";
import { Static } from "runtypes";
import { Box } from "..";
import { CommandLink } from "../library/CommandLink";

type ProcessesPanelProps = {
  processes: Array<Static<typeof Process>>;
};
export const ProcessesPanel = ({ processes }: ProcessesPanelProps) => {
  return (
    <div>
      <Box marginBottom={1}>Processes</Box>
      {processes.map((process) => {
        const progress =
          process.progress != null ? ` (${process.progress}%)` : "";
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
