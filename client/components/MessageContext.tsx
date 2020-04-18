import { createContext } from "react";

type MessageContextType = {
  sendLocalMessage: (command: string) => void;
  sendMessage: (command: string) => void;
};
export const MessageContext = createContext<MessageContextType>(
  (undefined as unknown) as MessageContextType,
);
