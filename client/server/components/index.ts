import type { PortscanComponent } from "./PortscanComponent";
import type { StartPortscanComponent } from "./StartPortscanComponent";
import type { EventsComponent } from "./EventsComponent";
import type { PlayerComponent } from "./PlayerComponent";
import type { NetworkComponent } from "./NetworkComponent";

export type {
  NetworkComponent,
  PortscanComponent,
  StartPortscanComponent,
  EventsComponent,
  PlayerComponent,
};

export type Component =
  | NetworkComponent
  | PortscanComponent
  | StartPortscanComponent
  | PlayerComponent
  | EventsComponent;
