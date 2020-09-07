import seedrandom from "seedrandom";
import { range } from "lodash";

const SEED = "42";

export const findPath = (source: string, target: string) => {
  return traverse(source).reverse().concat(traverse(target));
};

export const traverse = (
  target: string,
  assigned?: string,
  ranges: string[] = DEFAULT_RANGE,
  path: string[] = [],
): string[] => {
  if (targetMatchesRange(assigned, target)) {
    console.log(ranges);
    if (path[path.length - 1] === target) {
      return path;
    }
    return [...path, target];
  }
  const random = seedrandom(`${SEED}${assigned}`);
  const availableBlocks = ranges.flatMap((block) => {
    return breakRange(block, random);
  });
  const depth = path.length;
  shuffle(availableBlocks, SEED);
  const maxConnections = Math.floor(random() * availableBlocks.length);
  const connectionCount = Math.min(maxConnections + 1, availableBlocks.length);
  const nodes: [string, string[]][] = [];

  let foundIndex: number | undefined;
  range(0, connectionCount).forEach((index) => {
    const block = availableBlocks[index];
    if (!foundIndex && targetMatchesRange(block, target)) {
      foundIndex = index;
    }
    nodes.push([block, []]);
  });
  range(connectionCount, availableBlocks.length).forEach((index) => {
    const bucket = Math.floor(random() * connectionCount);
    if (foundIndex !== undefined && foundIndex !== bucket) {
      return;
    }
    const block = availableBlocks[index];
    if (!foundIndex && targetMatchesRange(block, target)) {
      foundIndex = bucket;
    }
    nodes[bucket][1].push(block);
  });
  if (foundIndex === undefined) {
    throw new Error("Target was not found in any available ranges");
  }
  const next = nodes[foundIndex];
  return traverse(target, next[0], next[1], [...path, findGateway(next[0])]);
};

const shuffle = <T>(arr: T[], seed: string) => {
  const random = seedrandom(seed);
  for (let i = arr.length - 1; i > 0; i--) {
    const j = Math.floor(random() * i);
    const temp = arr[i];
    arr[i] = arr[j];
    arr[j] = temp;
  }
};

const breakRange = (ipRange: string, random: () => number) => {
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

export const targetMatchesRange = (
  range: string | undefined,
  target: string,
) => {
  if (!range) {
    return false;
  }
  return target.startsWith(range) && target[range.length] === ".";
};

const DEFAULT_RANGE = range(0, 255).map((num) => num.toString());

const findGateway = (ipRange: string) => {
  const random = seedrandom(`${ipRange}${SEED}`);
  const length = ipRange.split(".").length + 1;
  const suffix = range(0, 5 - length)
    .map(() => {
      return Math.floor(random() * 255);
    })
    .join(".");
  return [ipRange, suffix].join(".");
};
