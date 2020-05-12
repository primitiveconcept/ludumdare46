import fs from "fs";
import { transform } from "@babel/core";
import path from "path";

const filePath = path.join(process.cwd(), "lib", "graphql.tsx");
const code = fs.readFileSync(filePath).toString();

const output = transform(code, {
  babelrc: false,
  filename: "graphql.tsx",
  plugins: [
    "@babel/plugin-syntax-typescript",
    path.resolve(__dirname, "./addOperationsTypePlugin.ts"),
  ],
});

if (!output) {
  throw new Error("Transform failed");
}

fs.writeFileSync(filePath, output.code);
