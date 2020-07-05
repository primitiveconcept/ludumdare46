import { joinPath } from "../lib/joinPath";
import { CommandHandler } from "./CommandHandler";

export const cdCommand: CommandHandler = ({
  addMessage,
  args,
  command,
  files,
  setCwd,
  state,
}) => {
  const rawPath = args[0];
  if (!rawPath) {
    return;
  }
  if (rawPath === "/") {
    setCwd("/");
    return;
  }
  const path = joinPath(rawPath);
  if (path === ".") {
    return;
  }
  if (path === "..") {
    if (state.cwd === "/") {
      return;
    }
    const newPath = state.cwd.split("/").filter(Boolean).slice(0, -1).join("/");
    setCwd(`/${newPath}`);
    return;
  }
  const newPath = path.startsWith("/") ? path : joinPath(state.cwd, path);
  if (
    newPath === "/" ||
    files?.find((file) => {
      return (
        file.type === "Folder" && newPath === joinPath(file.path, file.name)
      );
    })
  ) {
    setCwd(newPath);
  } else {
    addMessage(`${command}: ${path}: directory not found`);
  }
};
