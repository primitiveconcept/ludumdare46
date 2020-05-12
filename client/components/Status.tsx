import React, { useEffect, useState } from "react";
import { ReadyState } from "react-use-websocket";
import { gql, useQuery, useMutation, Static } from "../lib/client-gql";
import { useLazyQuery } from "@apollo/client";

export const thingsQuery = gql<
  "StatusGetThingsQuery",
  "StatusGetThingsQueryVariables"
>`
  query statusGetThings {
    things {
      ...StatusThing
    }
  }
`;

export const thingQuery = gql<
  "StatusGetThingQuery",
  "StatusGetThingQueryVariables"
>`
  query statusGetThing($thingId: String!) {
    thing(id: $thingId) {
      ...StatusThing
    }
  }
`;

export const createThingMutation = gql<
  "StatusCreateThingMutation",
  "StatusCreateThingMutationVariables"
>`
  mutation statusCreateThing($stuff: String!) {
    createThing(stuff: $stuff) {
      thing {
        ...StatusThing
      }
    }
  }
`;

export const ThingFragment = gql<"StatusThingFragment">`
  fragment StatusThing on Thing {
    name
  }
`;
type ThingFragment = Static<typeof ThingFragment>;

type StatusProps = {
  readyState: ReadyState;
  thing: ThingFragment;
};
export const Status = ({ readyState, thing }: StatusProps) => {
  const thingId = useState<number | null>(null);
  thing.name;
  const [getThing] = useLazyQuery(thingQuery);
  const { data: thingsData, loading: thingLoading } = useQuery(thingsQuery);
  const { data: thingData, loading, error } = useQuery(thingQuery, {
    variables: { thingId: "1234" },
  });
  const [
    createThing,
    { data: thingAgainData, loading: mutationLoading, error: mutationError },
  ] = useMutation(createThingMutation);

  useEffect(() => {
    if (thingId) {
      getThing({ variables: { thingId } });
    }
  }, [getThing, thingId]);

  const name = thingAgainData?.createThing.thing.name;
  return (
    <div>
      Status:{" "}
      {readyState === ReadyState.CONNECTING
        ? "Connecting..."
        : readyState === ReadyState.CLOSED
        ? "Closed"
        : "Connected"}
      <button
        onClick={() => {
          createThing({ variables: { stuff: "asdf" } });
        }}
      >
        create
      </button>
    </div>
  );
};
