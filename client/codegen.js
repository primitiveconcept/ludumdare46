module.exports = {
  overwrite: true,
  schema: "./schema.graphql",
  documents: "components/**/*.tsx",
  config: { nonOptionalTypename: true },
  generates: {
    "lib/graphql.tsx": {
      plugins: [
        "typescript",
        "typescript-operations",
        "./lib/addOperationsType",
      ],
    },
  },
};
