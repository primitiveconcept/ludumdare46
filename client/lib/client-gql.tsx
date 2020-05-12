import {
  useQuery as apolloUseQuery,
  useMutation as apolloUseMutation,
  QueryHookOptions,
  MutationHookOptions,
} from "@apollo/client";
import { Operations } from "./graphql";
import { DocumentNode } from "graphql";

export type Static<
  TFragment extends { __returnType: unknown }
> = TFragment["__returnType"];

type Document<TReturn = unknown, TVariables = undefined> = DocumentNode & {
  __returnType: TReturn;
  __variables: TVariables;
};

export declare function gql<
  TName extends keyof Operations,
  TVariablesName extends keyof Operations | undefined = undefined
>(
  literals: TemplateStringsArray,
): Document<
  Operations[TName],
  TVariablesName extends keyof Operations
    ? Operations[TVariablesName]
    : undefined
>;

export declare function useQuery<TDocument extends Document<unknown, unknown>>(
  document: TDocument,
  ...options: keyof TDocument["__variables"] extends { length: 0 }
    ? [QueryHookOptions<TDocument["__returnType"], TDocument["__variables"]>?]
    : [QueryHookOptions<TDocument["__returnType"], TDocument["__variables"]>]
): {
  data: TDocument["__returnType"] | undefined;
  loading: boolean;
  error: Error | undefined;
};

export declare function useMutation<
  TDocument extends Document<unknown, unknown>
>(
  document: TDocument,
  options?: {},
): [
  (
    options: MutationHookOptions<
      TDocument["__returnType"],
      TDocument["__variables"]
    >,
  ) => void,
  {
    data: TDocument["__returnType"] | undefined;
    loading: boolean;
    error: Error | undefined;
  },
];
