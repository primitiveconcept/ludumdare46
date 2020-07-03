import { useFiles } from "../hooks/useFiles";
import { joinPath } from "../lib/joinPath";

type CdCommand = {
  baseCommand: string;
  path: string;
  addMessage: (message: string) => void;
  cwd: string;
  setCwd: (cwd: string) => void;
  files: ReturnType<typeof useFiles>;
};
export const cdCommand = ({
  baseCommand,
  path,
  setCwd,
  cwd,
  addMessage,
  files,
}: CdCommand) => {
  if (!path || path === ".") {
    return;
  }
  if (path === "/") {
    setCwd("/");
    return;
  }
  if (path === "..") {
    if (cwd === "/") {
      return;
    }
    const newPath = cwd.split("/").filter(Boolean).slice(0, -1).join("/");
    setCwd(`/${newPath}`);
  } else {
    const newPath = joinPath(cwd, path);
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
      addMessage(`${baseCommand}: ${path}: directory not found`);
    }
  }
};
