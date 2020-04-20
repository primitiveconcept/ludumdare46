import { Record, String, Union, Literal } from "runtypes";

export const Email = Record({
  // From address, ex: Hacker <hacker@example.com>
  from: String,
  // To address, may not be the player! ex: You <you@example.com>
  to: String,
  // true if read, false if not
  status: Union(Literal("Read"), Literal("Unread"), Literal("Archived")),
  // Text of the message, can include Markdown
  text: String,
});
