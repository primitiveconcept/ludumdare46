import { CommandProps } from "./commandProps";

export const lsCommand = ({ addMessage, files, state }: CommandProps) => {
  if (!files) {
    return;
  }
  addMessage(
    files
      .filter((file) => file.path === state.cwd)
      .map((file) => `- ${file.name}${file.type === "Folder" ? "/" : ""}`)
      .join("\n"),
  );
};
