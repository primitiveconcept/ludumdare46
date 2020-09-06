import seedrandom from "seedrandom";
import { range } from "lodash";

const SEED = "42";

const shuffle = <T>(arr: T[], seed: string) => {
  const random = seedrandom(seed);
  for (let i = arr.length - 1; i > 0; i--) {
    const j = Math.floor(random() * i);
    const temp = arr[i];
    arr[i] = arr[j];
    arr[j] = temp;
  }
};

const breakRange = (ipRange: string) => {
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

export const findPath = (
  assigned: string | undefined,
  ranges: string[] = DEFAULT_RANGE,
  target: string,
  path: string[] = [],
): string[] => {
  if (targetMatchesRange(assigned, target)) {
    return path;
  }
  const availableRanges = ranges.flatMap(breakRange);
  shuffle(availableRanges, SEED);
  const random = seedrandom(`${SEED}${assigned}`);
  const nodeCount = Math.min(20, availableRanges.length - 1);
  const nodes: [string, string[]][] = [];
  let foundIndex: number | undefined;
  range(0, nodeCount).forEach((index) => {
    const block = availableRanges[index];
    if (!foundIndex && targetMatchesRange(block, target)) {
      foundIndex = index;
    }
    nodes.push([block, []]);
  });
  range(nodeCount, availableRanges.length).forEach((index) => {
    const bucket = Math.floor(random() * nodeCount);
    if (foundIndex !== undefined && foundIndex !== bucket) {
      return;
    }
    const block = availableRanges[index];
    if (!foundIndex && targetMatchesRange(block, target)) {
      foundIndex = bucket;
    }
    nodes[bucket][1].push(block);
  });
  if (foundIndex === undefined) {
    throw new Error("Target was not found in any available ranges");
  }
  const next = nodes[foundIndex];
  return findPath(next[0], next[1], target, [...path, findGateway(next[0])]);
};

/**
 * to 8.8.8.8
 *
 * 192.168.1.1 (local)
 * 142.254.236.5
 * 76.167.27.109
 * 72.129.14.86
 * 66.109.6.202 (backbone)
 * 72.14.209.18 (google)
 * 108.170.237.12 (google)
 * 142.250.226.45 (google)
 * 8.8.8.8 (level 3)
 */

/**
 * to www.google.com
 *
 * 192.168.1.1
 * 142.254.236.5
 * 76.167.27.109
 * 72.129.14.86
 * 72.129.13.2
 * 66.109.6.202 (charter backbone)
 * 72.14.209.18 (google)
 * 108.170.237.10 (google)
 * 209.85.245.229 (google)
 * 172.217.14.68 (google)
 */

/**
 * to www.truecar.com
 *
 * 192.168.1.1
 * 142.254.236.5
 * 76.167.27.109
 * 72.129.14.86
 * 72.129.13.2
 * 66.109.6.202 (backbone)
 * 66.109.5.247 (charter)
 * 99.82.176.54 (amazon)
 * * * *
 */

/**
 * to 199.201.159.1
 *
 * 192.168.1.1 (local)
 * 142.254.236.5 (charter)
 * 76.167.27.109 (charter)
 * 72.129.14.86 (charter)
 * 72.129.13.2 (charter)
 * 66.109.6.64 (charter backbone)
 * 66.109.5.241 (charter)
 * 216.218.249.193 (Hurricane Electric)
 * 184.105.80.201 (Hurricane Electric)
 * 184.105.223.166 (Hurricane Electric)
 * 216.66.36.102 (Hurricane Electric)
 * 66.152.98.9 (First Light Fiber)
 * 66.152.98.14 (First Light Fiber)
 * 66.109.52.102 (First Light Fiber)
 * 216.107.229.218 (SEG Network Technologies)
 * 66.211.128.134 (First Light Fiber)
 * 66.211.128.222 (First Light Fiber)
 * 199.201.159.1 (JLC)
 */

/**
 * rarbg.to
 *
 * 192.168.1.1
 * 142.254.236.5
 * 76.167.27.109
 * 72.129.14.86
 * 72.129.13.2
 * 66.109.6.64
 * Request timed out.
 * 4.69.162.181 (Level 3)
 * 213.244.164.2 (Level 3)
 * 87.245.232.155 (RETN.net)
 * Request timed out.
 * 91.195.120.225 (TOV Highload Systems)
 * Request timed out.
 * 185.37.101.2 (S A and A stroi proekt EOOD) hosting
 * 185.37.102.2
 * 185.37.100.2
 * 185.37.100.6
 * 185.37.100.122
 */

/**
 * Tracing route to mataram.imigrasi.go.id [156.67.211.4]
 * over a maximum of 30 hops:
 *
 * 192.168.1.1
 * 142.254.236.5
 * 76.167.27.109
 * 72.129.14.86
 * 72.129.13.2
 * 66.109.6.202
 * 107.14.19.37
 * 107.14.19.138
 * 203.208.171.97
 * 203.208.172.145
 * 203.208.182.253
 * 203.208.177.110
 * *        *        *     Request timed out.
 * 203.208.177.110
 * *        *        *     Request timed out.
 * *        *        *     Request timed out.
 * *        *        *     Request timed out.
 * *        *        *     Request timed out.
 * 156.67.211.4 (RIPE)
 */
