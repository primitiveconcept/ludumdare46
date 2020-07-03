import { joinPath } from "../lib/joinPath";
import { CommandProps } from "./commandProps";

export const cdCommand = ({
  addMessage,
  args,
  command,
  files,
  setCwd,
  state,
}: CommandProps) => {
  const path = args[0];
  if (!path || path === ".") {
    return;
  }
  if (path === "/") {
    setCwd("/");
    return;
  }
  if (path === "..") {
    if (state.cwd === "/") {
      return;
    }
    const newPath = state.cwd.split("/").filter(Boolean).slice(0, -1).join("/");
    setCwd(`/${newPath}`);
  } else {
    const newPath = joinPath(state.cwd, path);
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
  }
};
