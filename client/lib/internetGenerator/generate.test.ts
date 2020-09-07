import { findPath, targetMatchesRange } from "./generate";

describe("findPath", () => {
  it("generates an Internet backbone", () => {
    expect(findPath("199.201.159.1", "8.8.8.8")).toEqual([]);
  });

  it("returns connections from target", () => {
    expect(findPath("199.201.159.1", "90.64.250.105")).toEqual([]);
  });

  it("matches partial ranges or something", () => {
    expect(targetMatchesRange("8.8.8", "8.8.8.8")).toBe(true);
  });
});
