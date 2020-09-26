import { Component } from "../../components";
import { Entity } from "../../types/Entity";

export const ecs = <TEntities extends Array<Entity<any>>>(data: TEntities) => {
  return {
    withComponents: <TName extends Component["type"]>(
      ...names: TName[]
    ): Array<Entity<TName>> => {
      return data.filter((entity) => {
        names.every((name) =>
          entity.components.find((comp) => comp.type === name),
        );
      });
    },
  };
};

export const findComponent = <
  TName extends Component["type"],
  TEntity extends Entity<TName>
>(
  entity: TEntity,
  name: TName,
) => {
  return entity.components.find((comp) => comp.type === name)!;
};
