import { findPath, targetMatchesRange } from "./generate";

describe("generate", () => {
  it("generates an Internet backbone", () => {
    expect(findPath(undefined, undefined, "8.8.8.8")).toEqual([
      "168.187.254.68",
      "206.92.77.63",
      "173.105.209.109",
      "59.175.142.134",
      "205.4.179.212",
      "8.8.8.17",
    ]);
    expect(findPath(undefined, undefined, "205.4.179.150")).toEqual([
      "168.187.254.68",
      "206.92.77.63",
      "173.105.209.109",
      "59.175.142.134",
      "205.4.179.212",
    ]);
  });

  it("matches partial ranges or something", () => {
    expect(targetMatchesRange("8.8.8", "8.8.8.8")).toBe(true);
  });
});
