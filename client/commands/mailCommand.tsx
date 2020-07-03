import { CommandHandler } from "./commandHandler";

export const mailCommand: CommandHandler = ({
  startProcess,
  setOpenProcessId,
}) => {
  startProcess({
    id: "mail",
    command: "mail",
    complete: false,
  });
  setOpenProcessId("mail");
  return;
};
