import {
  Array,
  Union,
  Record,
  String,
  Literal,
  Number,
  Null,
  Static,
} from "runtypes";
import { Port } from "./Port";

export const PortscanProcess = Record({
  id: String,
  command: Literal("portscan"),
  origin: String,
  target: String,
  progress: Union(Number, Null),
  error: Union(String, Null),
  ports: Array(Port),
});
export type PortscanProcess = Static<typeof PortscanProcess>;
