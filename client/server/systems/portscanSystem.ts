import { System } from "./System";

export const portscanSystem = ({ entities }: System) => {
  const eventsComponent = entities.withComponents("Events")[0].components
    .Events;
  eventsComponent.events.forEach((event) => {
    if (event.type === "StartPortscan") {
      const player = entities.withComponents("Player", "Location")[0];
      const loc = player.components.Location;
    }
  });
};
