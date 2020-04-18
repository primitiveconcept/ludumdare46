import { Static } from "runtypes";
import { Device } from "./Message";

export type Inventory = {
  bitcoin: string;
  devices: Array<Static<typeof Device>>;
};
