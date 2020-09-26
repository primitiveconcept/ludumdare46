import { ecs } from "./ecs";

describe("ecs", () => {
  describe("withComponents", () => {
    it("selects entities with a single component", () => {
      const entities = ecs([]);
      entities.createEntity({ Location: { type: "Location", ip: "8.8.8.8" } });
      entities.createEntity({ Player: { type: "Player" } });
      expect(entities.withComponents("Location")[0].components).toEqual({
        Location: { type: "Location", ip: "8.8.8.8" },
      });
    });
  });
});
