import { range } from "lodash";

export const breakRange = (ipRange: string, random: () => number) => {
  if (random() < 0.9) {
    return ipRange;
  }
  const length = ipRange.split(".").length + 1;
  if (length >= 4) {
    return [ipRange];
  }
  return range(1, 255).map((part) => {
    return `${ipRange}.${part}`;
  });
};
