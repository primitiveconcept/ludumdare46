import { CommandHandler } from "./commandHandler";

export const bgCommand: CommandHandler = ({ setOpenProcessId }) => {
  setOpenProcessId(null);
};
