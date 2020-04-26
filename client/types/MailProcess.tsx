import { Record, String, Literal, Static } from "runtypes";

export const MailProcess = Record({
  id: String,
  command: Literal("mail"),
});
export type MailProcess = Static<typeof MailProcess>;
