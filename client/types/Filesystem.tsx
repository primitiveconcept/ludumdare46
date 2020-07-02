import { Array, Union, Record, String, Static, Literal } from "runtypes";

export const File = Record({
  id: String,
  type: Literal("File"),
  name: String,
});
export type File = Static<typeof File>;

export const Folder = Record({
  id: String,
  type: Literal("Folder"),
  name: String,
  contents: Array(String),
});
export type Folder = Static<typeof Folder>;

export const Filesystem = Record({
  ip: String,
  contents: Array(Union(File, Folder)),
  root: Array(String),
});
export type Filesystem = Static<typeof Filesystem>;
