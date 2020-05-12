const rule = require("../require-gql-type-parameters");
const RuleTester = require("eslint").RuleTester;
const path = require("path");

const ruleTester = new RuleTester({
  parser: path.resolve(
    process.cwd(),
    "node_modules",
    "@typescript-eslint",
    "parser",
  ),
});

ruleTester.run("require-gql-type-parameters", rule, {
  valid: [
    `
    export const thingsQuery = gql<
      "ThingsQuery",
      "ThingsQueryVariables"
    >\`
      query things {
        thing
      }
    \`;
    `,
  ],
  invalid: [
    // missing type parameters
    {
      code: `
      export const thingsQuery = gql\`
        query things {
          thing
        }
      \`;
      `,
      errors: 1,
      output: `
      export const thingsQuery = gql<"ThingsQuery", "ThingsQueryVariables">\`
        query things {
          thing
        }
      \`;
      `,
    },
    // incorrect params
    {
      code: `
      export const thingsQuery = gql\<"Things", "ThingsVariables">\`
        query things {
          thing
        }
      \`;
      `,
      errors: 1,
      output: `
      export const thingsQuery = gql<"ThingsQuery", "ThingsQueryVariables">\`
        query things {
          thing
        }
      \`;
      `,
    },
    // too few params
    {
      code: `
      export const thingsQuery = gql<"Thing">\`
        query things {
          thing
        }
      \`;
      `,
      errors: 1,
      output: `
      export const thingsQuery = gql<"ThingsQuery", "ThingsQueryVariables">\`
        query things {
          thing
        }
      \`;
      `,
    },
    // too many params
    {
      code: `
      export const thingsQuery = gql<"Thing">\`
        query things {
          thing
        }
      \`;
      `,
      errors: 1,
      output: `
      export const thingsQuery = gql<"ThingsQuery", "ThingsQueryVariables">\`
        query things {
          thing
        }
      \`;
      `,
    },
  ],
});
