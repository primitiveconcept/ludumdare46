import { System } from "./System";

export const clearEventsSystem = ({ world }: System) => {
  world.with("Events")[0].components.Events.events = [];
};
