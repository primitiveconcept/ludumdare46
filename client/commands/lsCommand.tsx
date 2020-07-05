import { CommandHandler } from "./CommandHandler";
import table from "markdown-table";
import { format } from "date-fns";
import { File, Folder } from "../types";
import { joinPath } from "../lib/joinPath";

const rsplit = <T extends string>(
  text: T,
  separator: string,
  limit?: number,
) => {
  const split = text.split(separator);
  return limit
    ? [split.slice(0, -limit).join(separator)].concat(split.slice(-limit))
    : split;
};

const displayName = (file: File | Folder) => {
  if (file.type === "Folder") {
    return `${file.name}/`;
  }
  if (file.executable) {
    return `${file.name}*`;
  }
  return file.name;
};

export const lsCommand: CommandHandler = ({
  addMessage,
  files: allFiles,
  state,
  args,
}) => {
  if (!allFiles) {
    return;
  }

  const files = allFiles.filter((file) => file.path === state.cwd);
  if (args[0]?.includes("a")) {
    const current = allFiles.find((file) => {
      if (state.cwd === "/") {
        return !file.name;
      }
      const parts = rsplit(state.cwd, "/", 1);
      const path = joinPath("/", parts[0]);
      const folder = parts[1];
      return file.path === path && folder === file.name;
    })!;
    const parent = allFiles.find((file) => {
      if (state.cwd === "/") {
        return !file.name;
      }
      const parts = rsplit(state.cwd, "/", 2);
      const path = joinPath("/", parts[0]);
      const folder = parts[1] ?? "";
      return file.path === path && folder === file.name;
    })!;
    files.unshift({
      ...parent,
      name: "..",
    });
    files.unshift({
      ...current,
      name: ".",
    });
  }

  if (args[0]?.includes("l")) {
    addMessage(
      table(
        files.map((file) => {
          const updatedAt = new Date(file.updatedAt);
          const yearFormat =
            updatedAt.getFullYear() === new Date().getFullYear()
              ? "hh:mm"
              : "yyyy";
          return [
            file.owner,
            file.size.toString(),
            format(updatedAt, `MMM dd ${yearFormat}`),
            displayName(file),
          ];
        }),
      ),
    );
    return;
  }
  addMessage(files.map((file) => `- ${displayName(file)}`).join("\n"));
};
