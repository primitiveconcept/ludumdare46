const upperFirst = (text) => {
  return text.charAt(0).toUpperCase() + text.slice(1);
};

module.exports = {
  meta: {
    fixable: true,
  },
  create(context) {
    return {
      TaggedTemplateExpression(node) {
        if (!node.tag.name === "gql") {
          return;
        }
        const query = node.quasi.quasis[0].value.raw;
        const [operationType, operationName] = query.trim().split(/\W/);
        if (!operationType || !operationName) {
          // can't fix
          return;
        }

        let typeParameters;
        if (operationType === "fragment") {
          typeParameters = [`"${upperFirst(operationName)}Fragment"`];
        } else if (operationType === "query" || operationType === "mutation") {
          typeParameters = [
            `"${upperFirst(operationName)}${upperFirst(operationType)}"`,
            `"${upperFirst(operationName)}${upperFirst(
              operationType,
            )}Variables"`,
          ];
        } else {
          return;
        }
        if (!node.typeParameters) {
          context.report({
            node,
            message: "Missing type parameter",

            fix(fixer) {
              return [
                fixer.insertTextAfter(
                  node.tag,
                  `<${typeParameters.join(", ")}>`,
                ),
              ];
            },
          });
        } else {
          const currentTypeParameters = node.typeParameters.params.map(
            (param) => {
              return param.literal.raw;
            },
          );
          if (typeParameters.length !== currentTypeParameters.length) {
            reportIncorrect(context, node, typeParameters);
            return;
          }
          for (const [index, param] of currentTypeParameters.entries()) {
            const correct = typeParameters[index];
            if (param !== correct) {
              reportIncorrect(context, node, typeParameters);
              return;
            }
          }
        }
      },
    };
  },
};

reportIncorrect = (context, node, correct) => {
  context.report({
    node: node.typeParameters,
    message: `Incorrect type parameters, should be <${correct.join(", ")}>`,
    fix: (fixer) => {
      return fixer.replaceText(node.typeParameters, `<${correct.join(", ")}>`);
    },
  });
};
