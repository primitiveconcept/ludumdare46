export const joinPath = (...args: string[]) => {
  const normalized = args.join("/").replace(/\/+/g, "/");
  // root path is a special case for trailing slashes
  // (no lookbehinds in many browsers)
  if (normalized === "/") {
    return normalized;
  }
  // otherwise, strip trailing slash
  return normalized.replace(/\/$/, "");
};
