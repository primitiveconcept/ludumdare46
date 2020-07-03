export const joinPath = (...args: string[]) => {
  if (!args[1]) {
    return args[0];
  }
  return args.join("/").replace(/\/\//g, "/");
};
