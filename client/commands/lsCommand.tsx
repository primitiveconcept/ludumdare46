import { CommandHandler } from "./CommandHandler";

export const lsCommand: CommandHandler = ({ addMessage, files, state }) => {
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
