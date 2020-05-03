import { WebSocket } from "mock-socket";
import { createMockSocket } from "../support/createMockSocket";

describe("install", () => {
  afterEach(() => {
    cy.alias("mockServer").then(({ closeServer }) => closeServer());
  });

  it("installs an infostealer onto a remote system", () => {
    createMockSocket(({ onCommand, sendMessage }) => {
      onCommand("internal_login threehams", () => {
        sendMessage(100, {
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

        sendMessage(400, {
          update: "InfostealerProcess",
          payload: {
            id: "4",
            command: "infostealer",
            complete: false,
            error: null,
            logins: [],
            target: "199.201.159.1",
          },
        });
        sendMessage(800, {
          update: "InfostealerProcess",
          payload: {
            id: "4",
            command: "infostealer",
            complete: false,
            error: null,
            logins: [
              { username: "root", password: null },
              { username: "admin", password: "Tr0ub4d0r!" },
              { username: null, password: "remarkablepenguinmonstrosity" },
            ],
            target: "199.201.159.1",
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

    cy.findByText("Install Malware").click();
    cy.findByText("infostealer").click();
    cy.getId("messages").should(
      "contain.text",
      "[199.201.159.101] infostealer installed. Running...",
    );
    cy.findByText("infostealer").click();
    cy.getId("infostealerProgram")
      .should("contain.text", 'username found: "root"')
      .should("contain.text", 'password found: "remarkablepenguinmonstrosity"')
      .should("contain.text", 'login found: "admin" / "Tr0ub4d0r!"');
  });
});
