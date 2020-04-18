import React from "react";
import { Inventory } from "../types";

type InventoryProps = {
  inventory: Inventory | null;
};
export const InventoryBar = ({ inventory }: InventoryProps) => {
  if (!inventory) {
    return null;
  }
  const { bitcoin, knownDevices } = inventory;
  return (
    <>
      <div>Bitcoin: {bitcoin}</div>
      <br />
      <div>Known devices</div>
      <ul>
        {knownDevices?.map((node) => {
          return <li key={node.ip}>{node.ip}</li>;
        })}
      </ul>
    </>
  );
};
