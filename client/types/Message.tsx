import { Union, String, Record, Null } from "runtypes";

export const Message = Union(
  Null,
  Record({
    data: String,
  }),
);
