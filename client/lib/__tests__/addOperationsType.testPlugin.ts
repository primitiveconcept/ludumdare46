import dedent from "dedent";
import { transform } from "@babel/core";
import path from "path";

function runPlugin(code: string) {
  const result = transform(code, {
    babelrc: false,
    filename: "test.ts",
    plugins: [
      "@babel/plugin-syntax-typescript",
      path.resolve(__dirname, "../addOperationsTypePlugin.ts"),
    ],
  });

  if (!result) {
    throw new Error("plugin failed");
  }

  return result.code;
}

describe("addOperationsType", () => {
  it("builds a type object from fragments", () => {
    const code = dedent`
    export type StatusGetThingsQueryVariables = {};
    export type StatusGetThingsQuery = { __typename: "Query" } & {
      things: Array<{ __typename: "Thing" } & StatusStuffFragment>;
    };`;
    const output = runPlugin(code);
    expect(output).toEqual(dedent`
    export type StatusGetThingsQueryVariables = {};
    export type StatusGetThingsQuery = {
      __typename: "Query";
    } & {
      things: Array<{
        __typename: "Thing";
      } & StatusStuffFragment>;
    };
    export type Operations = {
      StatusGetThingsQueryVariables: StatusGetThingsQueryVariables;
      StatusGetThingsQuery: StatusGetThingsQuery;
    };
    `);
  });

  it("ignored unrelated types", () => {
    const code = dedent`
    export type Thing = {};
    export type StatusGetThingsQueryVariables = {};`;
    const output = runPlugin(code);
    expect(output).toEqual(dedent`
    export type Thing = {};
    export type StatusGetThingsQueryVariables = {};
    export type Operations = {
      StatusGetThingsQueryVariables: StatusGetThingsQueryVariables;
    };
    `);
  });
});
