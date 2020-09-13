import { findNetwork } from "./findNetwork";

describe("findNetwork", () => {
  it("generates an Internet backbone", () => {
    expect(findNetwork("8.8.8")).toEqual({
      range: "",
    });
  });
});
