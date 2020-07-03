import { CommandProps } from "./commandProps";

export const mailCommand = ({
  startProcess,
  setOpenProcessId,
}: CommandProps) => {
  startProcess({
    id: "mail",
    command: "mail",
    complete: false,
  });
  setOpenProcessId("mail");
  return;
};
