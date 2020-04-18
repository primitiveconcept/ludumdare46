import React, { useState } from "react";
import { Inventory } from "../types";
import { Markdown } from "./Markdown";

type InventoryProps = {
  inventory: Inventory;
};
export const InventoryBar = ({ inventory }: InventoryProps) => {
  const [currentIp, setCurrentIp] = useState<string | null>(null);
  const { bitcoin, devices } = inventory;
  const money = <div>Money: {bitcoin}₿</div>;
  if (!currentIp) {
    return (
      <>
        {money}
        <br />
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
      {money}
      <br />
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
