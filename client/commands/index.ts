import { helpCommand } from "./helpCommand";

export const commands: {
  [key: string]: undefined | ((args: string[]) => string);
} = {
  help: helpCommand,
};
