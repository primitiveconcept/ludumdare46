import { Static } from "runtypes";
import { Device } from "./Message";

export type Inventory = {
  bitcoin: number;
  knownDevices: Array<Static<typeof Device>>;
} | null;
