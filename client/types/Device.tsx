import { Record, String, Array, Static } from "runtypes";

export const Device = Record({
  ip: String,
  status: String,
  commands: Array(String),
});
export type Device = Static<typeof Device>;
