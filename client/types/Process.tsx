import { Record, String, Static, Union, Null } from "runtypes";

export const Process = Record({
  // The full command run, ex: sshcrack 199.201.159.1
  command: String,
  // human-readable status, ex: Running
  status: Union(String, Null),
});
export type Process = Static<typeof Process>;
