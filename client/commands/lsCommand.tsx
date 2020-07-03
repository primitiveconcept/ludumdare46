import { useFiles } from "../hooks/useFiles";

type LsCommand = {
  files: ReturnType<typeof useFiles>;
  addMessage: (message: string) => void;
  cwd: string;
};
export const lsCommand = ({ addMessage, files, cwd }: LsCommand) => {
  if (!files) {
    return;
  }
  addMessage(
    files
      .filter((file) => file.path === cwd)
      .map((file) => `- ${file.name}${file.type === "Folder" ? "/" : ""}`)
      .join("\n"),
  );
};
