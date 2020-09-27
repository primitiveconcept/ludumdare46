import seedrandom from "seedrandom";
import { DeviceType } from "../types/DeviceConfig";
import { devices } from "./devices";

/**
 * Seed should be universally unique, probably
 * IP (if public) or private IP + gateway IP (if private)
 */
export const findDevice = (ip: string, type: DeviceType) => {
  const random = seedrandom(ip);
  const config = devices[type];
  return {
    ip,
    ports: config.openPorts.filter(() => {
      return random() > 0.5;
    }),
  };
};
