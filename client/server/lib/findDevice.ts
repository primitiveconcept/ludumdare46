import { DeviceType } from "../types/DeviceConfig";
import { devices } from "./devices";

/**
 * Seed should be universally unique, probably
 * IP (if public) or private IP + gateway IP (if private)
 */
export const findDevice = (seed: string, type: DeviceType) => {
  const config = devices[type];
};
