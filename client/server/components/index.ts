import type { EventsComponent } from "./EventsComponent";
import type { LocationComponent } from "./LocationComponent";
import type { NetworkComponent } from "./NetworkComponent";
import type { PlayerComponent } from "./PlayerComponent";
import type { PortscanComponent } from "./PortscanComponent";
import type { StartPortscanComponent } from "./StartPortscanComponent";

export type {
  LocationComponent,
  NetworkComponent,
  PortscanComponent,
  StartPortscanComponent,
  EventsComponent,
  PlayerComponent,
};

export type Component =
  | LocationComponent
  | NetworkComponent
  | PortscanComponent
  | StartPortscanComponent
  | PlayerComponent
  | EventsComponent;
