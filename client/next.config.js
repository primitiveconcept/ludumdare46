const path = require("path");

module.exports = {
  exportPathMap: async function (
    defaultPathMap,
    { dev, dir, outDir, distDir, buildId },
  ) {
    return {
      "/": { page: "/" },
    };
  },
  typescript: {
    ignoreDevErrors: true,
    ignoreBuildErrors: true,
  },
};
