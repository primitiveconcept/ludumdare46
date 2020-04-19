describe("adjustments", () => {
  describe("bedroom", () => {
    beforeEach(() => {
      cy.visit("/");
    });

    it("connects to a remote machine", () => {
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.findByText("Logged in as threehams");
    });
  });
});
