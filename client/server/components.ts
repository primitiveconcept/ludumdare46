import type { EventsComponent } from "./features/events/EventsComponent";
import type { KnownDevicesComponent } from "./features/player/KnownDevicesComponent";
import type { LocationComponent } from "./components/LocationComponent";
import type { NetworkComponent } from "./components/NetworkComponent";
import type { PlayerComponent } from "./features/player/PlayerComponent";
import type { PortscanComponent } from "./features/portscan/PortscanComponent";
import type { StartPortscanComponent } from "./features/portscan/StartPortscanComponent";

export type {
  LocationComponent,
  NetworkComponent,
  PortscanComponent,
  StartPortscanComponent,
  EventsComponent,
  PlayerComponent,
  KnownDevicesComponent,
};

export type Component =
  | LocationComponent
  | NetworkComponent
  | PortscanComponent
  | StartPortscanComponent
  | PlayerComponent
  | EventsComponent
  | KnownDevicesComponent;
