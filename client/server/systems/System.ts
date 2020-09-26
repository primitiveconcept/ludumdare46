import { ValuesType } from "utility-types";
import { Component } from "../components";
import { Entity } from "../types/Entity";

export type System = {
  entities: {
    withComponents: <TNames extends Component["type"][]>(
      ...names: TNames
    ) => Array<Entity<ValuesType<TNames>>>;
  };
};
