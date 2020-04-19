import React from "react";
import { Resources } from "../types";

type ResourcesBarProps = {
  resources: Resources;
};
export const ResourcesBar = ({ resources }: ResourcesBarProps) => {
  const { bitcoin } = resources;
  return <div>Money: {bitcoin}₿</div>;
};
