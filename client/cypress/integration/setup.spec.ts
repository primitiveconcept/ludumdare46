describe("adjustments", () => {
  describe("bedroom", () => {
    beforeEach(() => {
      cy.visit("/");
    });

    it("logs in and shows a device list", () => {
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.getId("messages").should("contain.text", "Logged in as threehams");
      cy.findByText("Known Devices");
      cy.getId({ name: "knownIp", index: 0 })
        .click()
        .then((element) => {
          return element.text();
        })
        .as("ip");
      cy.findByText("Known Devices").should("not.exist");
      cy.findByText("Port Scan").click();
      cy.get("@ip").then((ip) => {
        cy.findByText("Found open port: Ftp");
        cy.findByText("Found open port: Ssh");
        // cy.findByText(`Running portscan against ${ip}`);
      });
      cy.findByText("Back").click();
      cy.findByText("Known Devices");
    });

    it("connects to a remote machine and saves the username", () => {
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.getId("messages").should("contain.text", "Logged in as threehams");
      cy.reload();
      cy.getId("messages").should("contain.text", "Logged in as threehams", {
        timeout: 10000,
      });
    });
  });
});
