import { createContext } from "react";

type CommandContextType = {
  command: string;
  setCommand: (command: string) => void;
  sendCommand: (command: string) => void;
};
export const CommandContext = createContext<CommandContextType>(
  (undefined as unknown) as CommandContextType,
);
