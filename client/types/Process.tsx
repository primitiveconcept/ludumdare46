import { Record, String } from "runtypes";

export const Process = Record({
  // The full command run, ex: sshcrack 199.201.159.1
  command: String,
  // human-readable status, ex: Running
  status: String,
});
