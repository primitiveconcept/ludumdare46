import { Component } from "../components";
import { Entity } from "../types/Entity";

export type System = {
  entities: {
    withComponent: <TName extends Component["type"]>(
      name: TName,
    ) => Array<Entity<TName>>;
  };
};
