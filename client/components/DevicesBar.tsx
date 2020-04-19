import React, { useState } from "react";
import { Markdown } from "./Markdown";
import { State } from "../types/State";

type DevicesBarProps = {
  devices: State["devices"];
};
export const DevicesBar = ({ devices }: DevicesBarProps) => {
  const [currentIp, setCurrentIp] = useState<string | null>(null);
  if (!devices.length) {
    return null;
  }
  if (!currentIp) {
    return (
      <>
        <div>Known devices</div>
        <ul>
          {devices.map((device) => {
            return (
              <li key={device.ip}>
                <a
                  href={device.ip}
                  data-test="knownIp"
                  onClick={(event) => {
                    event.preventDefault();
                    setCurrentIp(device.ip);
                  }}
                >
                  {device.ip}
                </a>
                <div>&nbsp;{device.status}</div>
                <div>&nbsp;</div>
              </li>
            );
          })}
        </ul>
      </>
    );
  }

  const device = devices.find((dev) => dev.ip === currentIp)!;
  return (
    <>
      <div>{device.ip}</div>
      <ul>
        {device.commands.map((command) => {
          return (
            <li key={command}>
              <Markdown>{command}</Markdown>
            </li>
          );
        })}
        <li>
          <a
            href="back"
            onClick={(event) => {
              event.preventDefault();
              setCurrentIp(null);
            }}
          >
            Back
          </a>
        </li>
      </ul>
    </>
  );
};
