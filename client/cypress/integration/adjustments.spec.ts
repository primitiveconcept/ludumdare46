describe("adjustments", () => {
  describe("bedroom", () => {
    beforeEach(() => {
      cy.visit("/");
      cy.findByText("Linksys v1.25.1720");
    });

    it("connects to a remote machine", () => {
      cy.getId({ name: "knownIp", index: 0 }).then((element) => {
        const ip = element.text();
        cy.findByLabelText("Enter Command").type(
          `ssh ${ip} admin admin{enter}`,
        );
        cy.findByText(`Connecting to ${ip}`);
        cy.findByText("Connected");
      });
    });
  });
});
