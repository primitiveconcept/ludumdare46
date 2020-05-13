export type Maybe<T> = T | null;
/** All built-in and custom scalars, mapped to their actual values */

export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
};
export type Thing = {
  __typename: "Thing";
  id: Scalars["ID"];
  name: Scalars["String"];
};
export type Query = {
  __typename: "Query";
  thing?: Maybe<Thing>;
  things: Thing[];
};
export type QueryThingArgs = {
  id: Scalars["String"];
};
export type CreateThing = {
  __typename: "CreateThing";
  thing: Thing;
};
export type Mutation = {
  __typename: "Mutation";
  createThing: CreateThing;
};
export type MutationCreateThingArgs = {
  stuff: Scalars["String"];
};
export type StatusGetThingsQueryVariables = {};
export type StatusGetThingsQuery = {
  __typename: "Query";
} & {
  things: Array<
    {
      __typename: "Thing";
    } & StatusThingFragment
  >;
};
export type StatusGetThingQueryVariables = {
  thingId: Scalars["String"];
};
export type StatusGetThingQuery = {
  __typename: "Query";
} & {
  thing?: Maybe<
    {
      __typename: "Thing";
    } & StatusThingFragment
  >;
};
export type StatusCreateThingMutationVariables = {
  stuff: Scalars["String"];
};
export type StatusCreateThingMutation = {
  __typename: "Mutation";
} & {
  createThing: {
    __typename: "CreateThing";
  } & {
    thing: {
      __typename: "Thing";
    } & StatusThingFragment;
  };
};
export type StatusThingFragment = {
  __typename: "Thing";
} & Pick<Thing, "name">;
export type Operations = {
  Query: Query;
  Mutation: Mutation;
  StatusGetThingsQueryVariables: StatusGetThingsQueryVariables;
  [`
  query statusGetThing($thingId: String!) {
    thing(id: $thingId) {
      ...StatusThing
    }
  }
  `]: StatusGetThingsQuery;
  StatusGetThingQueryVariables: StatusGetThingQueryVariables;
  StatusGetThingQuery: StatusGetThingQuery;
  StatusCreateThingMutationVariables: StatusCreateThingMutationVariables;
  StatusCreateThingMutation: StatusCreateThingMutation;
  StatusThingFragment: StatusThingFragment;
};
