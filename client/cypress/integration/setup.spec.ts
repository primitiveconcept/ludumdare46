describe("adjustments", () => {
  describe("bedroom", () => {
    beforeEach(() => {
      cy.visit("/");
    });

    it("logs in and shows a device list", () => {
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.findByText("Logged in as threehams");
    });

    it("connects to a remote machine and saves the username", () => {
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.findByText("Logged in as threehams");
      cy.reload();
      cy.findByText("Logged in as threehams", { timeout: 15000 });
    });
  });
});
