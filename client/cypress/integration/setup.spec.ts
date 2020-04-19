describe("adjustments", () => {
  describe("bedroom", () => {
    beforeEach(() => {
      cy.visit("/");
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.getId("messages").should("contain.text", "Logged in as threehams");
    });

    it("persists username across reloads", () => {
      cy.reload();
      cy.getId("messages").should("contain.text", "Logged in as threehams", {
        timeout: 10000,
      });
    });

    it("logs in and shows a device list", () => {
      cy.findByText("Known Devices");
      cy.getId({ name: "knownIp", index: 0 }).click();
      cy.findByText("Known Devices").should("not.exist");
      cy.findByText("portscan").click();
      cy.findByText("Found open port: Ssh");
      cy.findByText("Back").click();
    });

    it("can crack into a device and install a keylogger", () => {
      cy.findByText("Known Devices");
      cy.getId({ name: "knownIp", index: 0 })
        .click()
        .then((element) => {
          return element.text();
        })
        .as("ip");
      cy.findByText("Known Devices").should("not.exist");
      cy.findByText("portscan").click();
      cy.findByText("portscan").should("not.exist");
      cy.get("@ip").then((ip) => {
        cy.getId("messages").should("contain.text", `portscan ${ip}`);
        cy.findByText("Found open port: Ssh");
        cy.findByText("sshcrack").click();
        cy.findByText("sshcrack").should("not.exist");
        cy.getId("messages").should("contain.text", `sshcrack ${ip}`);
        cy.getId("messages").should(
          "contain.text",
          `Found user/pass match for ${ip}: admin/admin`,
        );
        cy.findByText("Install Malware").click();
        cy.findByText("keylogger").click();
      });
    });
  });
});
