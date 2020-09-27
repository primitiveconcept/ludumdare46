import { findDevice } from "../../lib/findDevice";
import { findPath } from "../../lib/findPath";
import { System } from "../../types/System";

export const portscanSystem = ({ world, addMessage }: System) => {
  const eventsComponent = world.with("Events")[0].components.Events;
  eventsComponent.events.forEach((event) => {
    if (event.type === "StartPortscan") {
      const player = world.with("Player", "Location", "KnownDevices")[0];
      const sourceIp = event.source ?? player.components.Location.ip;
      const path = findPath(sourceIp, event.target);
      if (!path) {
        addMessage(`portscan: no path to destination`);
        return;
      }
      const target =
        world.find(event.target) ??
        world.createEntity(event.target, {
          Location: {
            type: "Location",
            ip: event.target,
          },
        });
      target.components.Portscan = {
        lastPortScanned: 0,
        source: sourceIp,
        type: "Portscan",
        startedAt: new Date().getTime(),
      };
      let knownDevice = player.components.KnownDevices.items.find(
        (device) => device.ip === target.id,
      );
      if (!knownDevice) {
        knownDevice = {
          ip: event.target,
          ports: [],
        };
        player.components.KnownDevices.items.push(knownDevice);
      }
    }
  });

  world.with("Portscan").forEach((entity) => {
    const player = world.with("Player", "KnownDevices")[0];
    const { source, startedAt, lastPortScanned } = entity.components.Portscan;
    const path = findPath(source, entity.id);
    const latency = path.reduce((sum, node) => {
      return sum + node.latency;
    }, 0);
    const portScanned = Math.floor(
      ((new Date().getTime() - startedAt) / latency) * 1000,
    );
    if (portScanned !== lastPortScanned) {
      entity.components.Portscan.lastPortScanned = portScanned;
      const ports = findDevice(entity.id, "workstation").ports;
      const knownDevice = player.components.KnownDevices.items.find(
        (item) => item.ip === entity.id,
      )!;
      const foundPorts = ports.filter((port) => port <= portScanned);
      foundPorts.length > knownDevice.ports.length &&
        addMessage(`found ports: ${foundPorts.join(", ")}`);
      knownDevice.ports = foundPorts;
    }
    if (portScanned > 65535) {
      entity.components.Portscan = undefined!;
      addMessage(`portscan complete`);
    }
  });
};
