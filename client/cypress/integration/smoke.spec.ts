describe("adjustments", () => {
  describe("bedroom", () => {
    it("persists username across reloads", () => {
      cy.visit("/");
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.getId("messages").should("contain.text", "Logged in as threehams");
      cy.reload();
      cy.getId("messages").should("contain.text", "Logged in as threehams", {
        timeout: 10000,
      });
    });
  });
});
