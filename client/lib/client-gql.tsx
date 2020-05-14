import {
  useQuery as apolloUseQuery,
  useMutation as apolloUseMutation,
  QueryHookOptions,
  MutationHookOptions,
} from "@apollo/client";
import apolloGql from "graphql-tag";
import { Operations } from "./graphql";
import { DocumentNode } from "graphql";

export type Static<
  TFragment extends { __returnType: unknown }
> = TFragment["__returnType"];

type Document<
  TOperation extends { data: unknown; variables: unknown }
> = DocumentNode & {
  __returnType: TOperation["data"];
  __variables: TOperation["variables"];
};

export const gql = (apolloGql as unknown) as <TName extends string>(
  literal: TName,
) => Document<
  TName extends keyof Operations
    ? Operations[TName]
    : { data: unknown; variables: unknown }
>;

export const useQuery = apolloUseQuery as <
  TDocument extends Document<{ data: unknown; variables: unknown }>
>(
  document: TDocument,
  ...options: keyof TDocument["__variables"] extends { length: 0 }
    ? [QueryHookOptions<TDocument["__returnType"], TDocument["__variables"]>?]
    : [QueryHookOptions<TDocument["__returnType"], TDocument["__variables"]>]
) => {
  data: TDocument["__returnType"] | undefined;
  loading: boolean;
  error: Error | undefined;
};

export const useMutation = apolloUseMutation as <
  TDocument extends Document<{ data: unknown; variables: unknown }>
>(
  document: TDocument,
  options?: {},
) => [
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
