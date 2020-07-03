import { MailProcess } from "../types/MailProcess";

type MailCommand = {
  setOpenProcessId: (processId: string | null) => void;
  startProcess: (process: MailProcess) => void;
};
export const mailCommand = ({
  startProcess,
  setOpenProcessId,
}: MailCommand) => {
  startProcess({
    id: "mail",
    command: "mail",
    complete: false,
  });
  setOpenProcessId("mail");
  return;
};
