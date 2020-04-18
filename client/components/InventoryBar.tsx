import React from "react";
import { Inventory } from "../types";

type InventoryProps = {
  inventory: Inventory | null;
};
export const InventoryBar = ({ inventory }: InventoryProps) => {
  return (
    <>
      <div>Bitcoin: {inventory?.bitcoin ?? "?"}</div>
      <br />
      <div>Known devices</div>
      <ul>
        {inventory?.knownDevices?.map((node) => {
          return (
            <li key={node.ip}>
              <button data-test="knownIp">{node.ip}</button>
            </li>
          );
        })}
      </ul>
    </>
  );
};
