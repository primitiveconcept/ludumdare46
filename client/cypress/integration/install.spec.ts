import { WebSocket } from "mock-socket";
import { createMockSocket, CloseServer } from "../support/createMockSocket";

describe("install", () => {
  let closeServer: CloseServer;

  afterEach(() => {
    closeServer();
  });

  it("installs an infostealer onto a remote system", () => {
    closeServer = createMockSocket(({ onCommand, sendMessage }) => {
      onCommand("internal_login threehams", () => {
        sendMessage(100, {
          update: "Devices",
          payload: {
            devices: [
              {
                ip: "199.201.159.101",
                status: "disconnected",
                commands: ["[portscan](portscan|199.201.159.101)"],
              },
            ],
          },
        });
      });

      onCommand("portscan 199.201.159.101", () => {
        sendMessage(100, {
          update: "Devices",
          payload: {
            devices: [
              {
                ip: "199.201.159.101",
                status: "portscan (0%)",
                commands: [],
              },
            ],
          },
        });
        sendMessage(300, {
          update: "Devices",
          payload: {
            devices: [
              {
                ip: "199.201.159.101",
                status: "portscan (50%)",
                commands: [],
              },
            ],
          },
        });
        sendMessage(700, {
          update: "Terminal",
          payload: {
            message: "[199.201.159.101] Found open port: 22 (SSH)",
          },
        });
        sendMessage(750, {
          update: "Devices",
          payload: {
            devices: [
              {
                ip: "199.201.159.101",
                status: "portscan (100%)",
                commands: ["[sshcrack](sshcrack|199.201.159.101)"],
              },
            ],
          },
        });
      });

      onCommand("sshcrack 199.201.159.101", () => {
        sendMessage(100, {
          update: "Devices",
          payload: {
            devices: [
              {
                ip: "199.201.159.101",
                status: "sshcrack (0%)",
                commands: [],
              },
            ],
          },
        });
        sendMessage(250, {
          update: "Terminal",
          payload: {
            message: "[199.201.159.101] Found user/pass match: admin/admin",
          },
        });
        sendMessage(350, {
          update: "Devices",
          payload: {
            devices: [
              {
                ip: "199.201.159.101",
                status: "connected",
                commands: [
                  "[infostealer](install|infostealer|199.201.159.101)",
                ],
              },
            ],
          },
        });
      });

      onCommand("install infostealer 199.201.159.101", () => {
        sendMessage(100, {
          update: "Devices",
          payload: {
            devices: [
              {
                ip: "199.201.159.101",
                status: "install: infostealer (0%)",
                commands: [],
              },
            ],
          },
        });
        sendMessage(150, {
          update: "Devices",
          payload: {
            devices: [
              {
                ip: "199.201.159.101",
                status: "connected",
                commands: [],
              },
            ],
          },
        });
        sendMessage(350, {
          update: "Terminal",
          payload: {
            message: "[199.201.159.101] infostealer installed. Running...",
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
    cy.findByText("199.201.159.101").click();
    cy.findByText("portscan").click();
    cy.findByText("portscan").should("not.exist");
    cy.getId("messages").should(
      "contain.text",
      `threehams@local$ portscan 199.201.159.101`,
    );
    cy.getId("messages").should(
      "contain.text",
      "[199.201.159.101] Found open port: 22 (SSH)",
    );
    cy.findByText("sshcrack").click();
    cy.findByText("sshcrack").should("not.exist");
    cy.getId("messages").should(
      "contain.text",
      `threehams@local$ sshcrack 199.201.159.101`,
    );
    cy.getId("messages").should(
      "contain.text",
      `[199.201.159.101] Found user/pass match: admin/admin`,
    );
    cy.findByText("Install Malware").click();
    cy.findByText("infostealer").click();
    cy.getId("messages").should(
      "contain.text",
      "[199.201.159.101] infostealer installed. Running...",
    );
  });
});
