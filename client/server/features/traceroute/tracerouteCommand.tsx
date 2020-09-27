import { findPath } from "../../lib/findPath";
import { ServerCommandHandler } from "../../types/ServerCommandHandler";

export const tracerouteCommand: ServerCommandHandler = ({
  args,
  addMessage,
}) => {
  const ip = args[0];
  if (!ip) {
    return "usage: traceroute [ip]";
  }
  const path = findPath("199.201.159.1", ip);
  const message = path
    .map((connection, index) => {
      return `* ${index + 1}  ${connection.ip} ${connection.latency}ms`;
    })
    .join("\n");
  addMessage(`traceroute to ${ip}, 64 hops max, 52 byte packages

${message}`);
};
