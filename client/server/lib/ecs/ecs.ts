import { ValuesType } from "utility-types";
import { Component } from "../../components";
import { Entity } from "../../types/Entity";
import { v4 as uuid } from "uuid";

export const ecs = <TEntities extends Entity<Component["type"]>[]>(
  entities: TEntities,
) => {
  return {
    find: (id: string) => {
      return entities.find((entity) => entity.id === id);
    },
    with: <TNames extends Component["type"][]>(
      ...names: TNames
    ): Array<Entity<ValuesType<TNames>>> => {
      // TODO incredibly inefficient, switch to an iterator, or cache, or both?
      return (entities.filter((entity) => {
        return !!names.every((name) => entity.components[name]);
      }) as unknown) as Array<Entity<ValuesType<TNames>>>;
    },

    createEntity: (
      id: string | undefined,
      components: Partial<
        { [Key in Component["type"]]: Extract<Component, { type: Key }> }
      >,
    ) => {
      const entity = {
        id: id ?? uuid(),
        components,
        // TODO could improve this by making all undefined properties optional
        // in Entity, or by extracting
      } as Entity<any>;
      entities.push(entity);
      return entity;
    },

    removeEntity: (id: string) => {
      const index = entities.findIndex((entity) => entity.id === id);
      if (index !== -1) {
        entities.splice(index, 1);
      }
    },
  };
};
