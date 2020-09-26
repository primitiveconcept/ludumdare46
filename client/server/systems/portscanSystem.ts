import { findComponent } from "../lib/ecs";
import { System } from "./System";

export const portscanSystem = ({ entities }: System) => {
  const eventsComponent = findComponent(
    entities.withComponent("Events")[0],
    "Events",
  );
  eventsComponent.events.forEach((event) => {
    if (event.type === "StartPortscan") {
      const player = entities.withComponent("Player")[0];
      const loc = findComponent(player, "Location");
    }
  });
};
