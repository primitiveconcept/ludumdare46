import { CommandHandler } from "./CommandHandler";
import { findPath } from "../lib/internetGenerator/generate";

export const tracerouteCommand: CommandHandler = ({ addMessage, args }) => {
  const ip = args[0];
  if (!ip) {
    return "usage: traceroute [ip]";
  }
  addMessage(`traceroute to ${ip}, 64 hops max, 52 byte packages`);
  const path = findPath("199.201.159.1", ip);
  path.map((address, index) => {
    addMessage(`${index + 1}  ${address}`);
  });
};
