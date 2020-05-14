// @ts-check
const { GraphQLSchema, DocumentNode } = require("graphql");
const util = require("util");

const upperFirst = (text) => {
  return text.charAt(0).toUpperCase() + text.slice(1);
};
const suffix = (def) => {
  if (def.kind === "OperationDefinition") {
    return upperFirst(def.operation);
  }
  return "Fragment";
};
const typeFor = (def) => {
  const baseName = `${upperFirst(def.name.value)}${suffix(def)}`;
  if (def.kind === "OperationDefinition") {
    return `{ data: ${baseName}, variables: ${baseName}Variables }`;
  }
  return `{ data: ${baseName}}`;
};

const indentAndPad = (sdl) => {
  const indented = sdl
    .split("\n")
    .map((line) => {
      return `  ${line}`;
    })
    .join("\n");
  return `
${indented}
`;
};

module.exports = {
  plugin: (schema, documents, config, info) => {
    return documents
      .map((doc) => {
        const sdlMap = Object.fromEntries(
          doc.rawSDL.split("\n\n").map((sdl) => {
            const [, operationName] = sdl.split(/\W/);
            return [operationName, indentAndPad(sdl)];
          }),
        );
        // const sdl = doc.document.rawSDL.split("\n\n");
        const types = doc.document.definitions.map((def) => {
          return `[\`${sdlMap[def.name.value]}\`]: ${typeFor(def)};`;
        });

        return `export type Operations = {
          ${types.join("\n")}
        }`;
      })
      .join("\n");
  },
};
