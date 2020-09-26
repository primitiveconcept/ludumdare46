import { Component } from "../components";

export type Entity<TName extends Component["type"]> = {
  id: string;
  components: Array<Extract<Component, { type: TName }>>;
};
