module.exports = {
  overwrite: true,
  schema: "./schema.graphql",
  documents: "components/**/*.tsx",
  config: { nonOptionalTypename: true },
  hooks: {
    afterAllFileWrite: [
      'babel-node --extensions ".ts" ./lib/addOperationsType.ts',
    ],
  },
  generates: {
    "lib/graphql.tsx": {
      plugins: ["typescript", "typescript-operations"],
    },
  },
};
