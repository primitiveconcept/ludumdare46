import { Record, String, Array } from "runtypes";

export const Device = Record({
  ip: String,
  status: String,
  commands: Array(String),
});
