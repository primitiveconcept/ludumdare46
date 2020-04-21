import { WebSocket } from "mock-socket";
import { createMockSocket } from "../support/createMockSocket";

describe("adjustments", () => {
  describe("bedroom", () => {
    it("installs an infostealer onto a remote system", () => {
      createMockSocket(({ onCommand, sendMessage }) => {
        onCommand("internal_login threehams", () => {
          sendMessage(100, {
            update: "Devices",
            payload: {
              devices: [
                {
                  ip: "8.8.8.8",
                  status: "disconnected",
                  commands: ["[portscan](portscan|8.8.8.8)"],
                },
              ],
            },
          });
        });

        onCommand("portscan 8.8.8.8", () => {
          sendMessage(100, {
            update: "Devices",
            payload: {
              devices: [
                {
                  ip: "8.8.8.8",
                  status: "portscanning",
                  commands: [],
                },
              ],
            },
          });
          sendMessage(250, {
            update: "Terminal",
            payload: {
              message: "[8.8.8.8] Found open port: 22 (SSH)",
            },
          });
          sendMessage(350, {
            update: "Devices",
            payload: {
              devices: [
                {
                  ip: "8.8.8.8",
                  status: "portscanning",
                  commands: ["[sshcrack](sshcrack|8.8.8.8)"],
                },
              ],
            },
          });
        });

        onCommand("sshcrack 8.8.8.8", () => {
          sendMessage(100, {
            update: "Devices",
            payload: {
              devices: [
                {
                  ip: "8.8.8.8",
                  status: "sshcrack (0%)",
                  commands: [],
                },
              ],
            },
          });
          sendMessage(250, {
            update: "Terminal",
            payload: {
              message: "[8.8.8.8] Found user/pass match: admin/admin",
            },
          });
          sendMessage(350, {
            update: "Devices",
            payload: {
              devices: [
                {
                  ip: "8.8.8.8",
                  status: "connected",
                  commands: ["[infostealer](install|infostealer|8.8.8.8)"],
                },
              ],
            },
          });
        });

        onCommand("install infostealer 8.8.8.8", () => {
          sendMessage(100, {
            update: "Devices",
            payload: {
              devices: [
                {
                  ip: "8.8.8.8",
                  status: "install: infostealer (0%)",
                  commands: [],
                },
              ],
            },
          });
          sendMessage(250, {
            update: "Terminal",
            payload: {
              message: "[8.8.8.8] infostealer installed. Running...",
            },
          });
          sendMessage(350, {
            update: "Devices",
            payload: {
              devices: [
                {
                  ip: "8.8.8.8",
                  status: "connected",
                  commands: [],
                },
              ],
            },
          });
        });
      });

      cy.visit("/", {
        onBeforeLoad(win) {
          // Call some code to initialize the fake server part using MockSocket
          cy.stub(win, "WebSocket" as any, (url: string) => new WebSocket(url));
        },
      });
      cy.findByLabelText("Enter Username").type(`threehams{enter}`);
      cy.getId("messages").should("contain.text", "Logged in as threehams");

      cy.findByText("Known Devices");
      cy.findByText("8.8.8.8").click();
      cy.findByText("portscan").click();
      cy.findByText("portscan").should("not.exist");
      cy.getId("messages").should(
        "contain.text",
        `threehams@local$ portscan 8.8.8.8`,
      );
      cy.getId("messages").should(
        "contain.text",
        "[8.8.8.8] Found open port: 22 (SSH)",
      );
      cy.findByText("sshcrack").click();
      cy.findByText("sshcrack").should("not.exist");
      cy.getId("messages").should(
        "contain.text",
        `threehams@local$ sshcrack 8.8.8.8`,
      );
      cy.getId("messages").should(
        "contain.text",
        `[8.8.8.8] Found user/pass match: admin/admin`,
      );
      cy.findByText("Install Malware").click();
      cy.findByText("infostealer").click();
    });
  });
});
