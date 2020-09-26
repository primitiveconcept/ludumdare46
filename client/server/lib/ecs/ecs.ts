import { ValuesType } from "utility-types";
import { Component } from "../../components";
import { Entity } from "../../types/Entity";
import { v4 as uuid } from "uuid";

export const ecs = <TEntities extends Entity<Component["type"]>[]>(
  entities: TEntities,
) => {
  return {
    withComponents: <TNames extends Component["type"][]>(
      ...names: TNames
    ): Array<Entity<ValuesType<TNames>>> => {
      // TODO incredibly inefficient, switch to an iterator, or cache, or both?
      return (entities.filter((entity) => {
        return !!names.every((name) => entity.components[name]);
      }) as unknown) as Array<Entity<ValuesType<TNames>>>;
    },

    createEntity: (
      components: Partial<
        { [Key in Component["type"]]: Extract<Component, { type: Key }> }
      >,
    ) => {
      const entity = {
        id: uuid(),
        components,
        // TODO could improve this by making all undefined properties optional
        // in Entity, or by extracting
      } as Entity<any>;
      entities.push(entity);
      return entity;
    },
  };
};
