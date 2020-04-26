import { Record, Number, String, Static, Union, Null } from "runtypes";

export const Process = Record({
  id: String,
  command: String,
  origin: String,
  target: String,
  progress: Union(Number, Null),
});
export type Process = Static<typeof Process>;
