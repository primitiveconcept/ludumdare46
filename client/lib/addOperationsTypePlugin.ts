import * as BabelTypes from "@babel/types";
import { Visitor } from "@babel/traverse";

type Babel = {
  types: typeof BabelTypes;
};
export default function addOperationsType(
  babel: Babel,
): { visitor: Visitor<{}> } {
  const { types: t } = babel;
  const typeNames: string[] = [];
  return {
    visitor: {
      TSTypeAliasDeclaration(path) {
        if (
          looksLike(path.node, {
            id: {
              name: (name: string) =>
                name.match(/(Fragment|Query|Mutation|Variables)$/),
            },
          })
        ) {
          typeNames.push(path.node.id.name);
        }
      },
      Program: {
        exit(path) {
          path.node.body.push(
            t.exportNamedDeclaration(
              t.tsTypeAliasDeclaration(
                t.identifier("Operations"),
                null,
                t.tsTypeLiteral(
                  typeNames.map((name) => {
                    return t.tsPropertySignature(
                      t.identifier(name),
                      t.tsTypeAnnotation(t.tsTypeReference(t.identifier(name))),
                    );
                  }),
                ),
              ),
            ),
          );
        },
      },
    },
  };
}

function looksLike(
  a: { [key: string]: any },
  b: { [key: string]: any },
): boolean {
  return (
    a &&
    b &&
    Object.keys(b).every((bKey) => {
      const bVal = b[bKey];
      const aVal = a[bKey];
      if (typeof bVal === "function") {
        return bVal(aVal);
      }
      return isPrimitive(bVal) ? bVal === aVal : looksLike(aVal, bVal);
    })
  );
}

function isPrimitive(val: unknown): val is { [key: string]: unknown } {
  return val == null || /^[sbn]/.test(typeof val);
}
