import { Resources } from "./Resources";
import { Device } from "./Device";
import { Static } from "runtypes";

export type State = {
  messages: string[];
  devices: Array<Static<typeof Device>>;
  resources: Resources | null;
  commandHistory: string[];
};
