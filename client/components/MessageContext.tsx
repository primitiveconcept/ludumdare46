import { createContext } from "react";

type MessageContextType = {
  addMessage: (command: string) => void;
};
export const MessageContext = createContext<MessageContextType>(
  (undefined as unknown) as MessageContextType,
);
