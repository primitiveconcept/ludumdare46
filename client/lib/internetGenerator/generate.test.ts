import { findPath } from "./generate";

describe("findPath", () => {
  it("generates an Internet backbone", () => {
    expect(
      findPath("199.201.159.1", "8.8.8.8").map((connection) => connection.ip),
    ).toEqual([
      "199.201.159.1",
      "199.201.118.237",
      "236.220.134.225",
      "90.64.250.105",
      "172.135.237.170",
      "8.146.211.16",
      "8.8.8.8",
    ]);
  });

  it("takes a shorter path if the target matches part of the path", () => {
    expect(
      findPath("199.201.159.1", "236.220.134.224").map(
        (connection) => connection.ip,
      ),
    ).toEqual([
      "199.201.159.1",
      "199.201.118.237",
      "236.220.134.225",
      "236.220.134.224",
    ]);
  });

  it("paths directly to a node if it along the path up", () => {
    expect(
      findPath("199.201.159.1", "236.220.134.224").map(
        (connection) => connection.ip,
      ),
    ).toEqual([
      "199.201.159.1",
      "199.201.118.237",
      "236.220.134.225",
      "236.220.134.224",
    ]);
  });

  it("returns the target if the source and target are the same", () => {
    expect(
      findPath("199.201.159.1", "199.201.159.1").map(
        (connection) => connection.ip,
      ),
    ).toEqual(["199.201.159.1"]);
  });
});
