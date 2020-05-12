module.exports = {
  client: {
    includes: ["./components/**/*.tsx"],
    service: {
      name: "client",
      localSchemaFile: "./schema.graphql",
    },
  },
};
