describe("adjustments", () => {
  describe("bedroom", () => {
    beforeEach(() => {
      cy.visit("/");
    });

    it("logs in and shows a device list", () => {
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.findByText("Logged in as threehams");
      cy.findByText("Known Devices");
      cy.getId({ name: "knownIp", index: 0 }).click();
      cy.findByText("Known Devices").should("not.exist");
      cy.findByText("Back").click();
      cy.findByText("Known Devices");
    });

    it("connects to a remote machine and saves the username", () => {
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.findByText("Logged in as threehams");
      cy.reload();
      cy.findByText("Logged in as threehams", { timeout: 15000 });
    });
  });
});
