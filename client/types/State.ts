import { Device } from "./Device";
import { Email } from "./Email";
import { Resources } from "./Resources";
import { Process } from "./Process";
import {
  Static,
  Record,
  String,
  Null,
  Union,
  Array,
  Dictionary,
} from "runtypes";
import { Filesystem } from "./Filesystem";

export const State = Record({
  messages: Array(String),
  devices: Array(Device),
  resources: Union(Resources, Null),
  commandHistory: Array(String),
  processes: Array(Process),
  emails: Array(Email),
  filesystems: Dictionary(Filesystem),
  cwd: String,
});
export type State = Static<typeof State>;
