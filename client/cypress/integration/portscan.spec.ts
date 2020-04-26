import { WebSocket } from "mock-socket";
import { createMockSocket } from "../support/createMockSocket";

describe("install", () => {
  afterEach(() => {
    cy.alias("mockServer").then(({ closeServer }) => closeServer());
  });

  it("completes a portscan", () => {
    createMockSocket(({ onCommand, sendMessage }) => {
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
                status: "portscanning",
                commands: ["[sshcrack](sshcrack|199.201.159.1)"],
              },
            ],
          },
        });
        sendMessage(500, {
          update: "Processes",
          payload: {
            processes: [
              {
                id: "1",
                command: "portscan 199.201.159.1",
                origin: "localhost",
                target: "199.201.159.1",
                progress: 0,
              },
            ],
          },
        });
        sendMessage(250, {
          update: "PortscanProcess",
          payload: {
            id: "1",
            command: "portscan",
            origin: "localhost",
            target: "199.201.159.101",
            progress: 10,
            error: null,
            ports: [{ name: "ftp", number: 21 }],
          },
        });
        sendMessage(750, {
          update: "PortscanProcess",
          payload: {
            id: "1",
            command: "portscan",
            origin: "localhost",
            target: "199.201.159.101",
            progress: 30,
            error: null,
            ports: [
              { name: "ftp", number: 21 },
              { name: "ssh", number: 22 },
            ],
          },
        });
        sendMessage(4000, {
          update: "Devices",
          payload: {
            devices: [
              {
                ip: "199.201.159.101",
                status: "disconnected",
                commands: ["[sshcrack](sshcrack|199.201.159.101)"],
              },
            ],
          },
        });
      });
    }).as("mockServer");

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
    cy.findByText("portscan (0%)").click();
    cy.getId("portscanProgram").should("contain.text", "21/tcp");
    cy.getId("portscanProgram").should("contain.text", "22/tcp");
  });
});
