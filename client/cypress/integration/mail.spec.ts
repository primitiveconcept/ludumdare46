import { WebSocket, Server } from "mock-socket";
import { createMockSocket } from "../support/createMockSocket";

describe("mail", () => {
  let mockServer: Server;
  beforeEach(() => {
    mockServer = createMockSocket(({ onCommand, sendMessage }) => {
      onCommand("internal_login threehams", () => {
        sendMessage(100, {
          update: "Emails",
          payload: {
            emails: [
              {
                id: "1",
                status: "Unread",
                from: "me <me@example.com>",
                to: "you <you@example.com>",
                subject: "check this out",
                body:
                  "want to grow your member size? [click here](portscan|8.8.8.8)",
              },
            ],
          },
        });
      });
      onCommand("mail read 1", () => {
        sendMessage(100, {
          update: "Emails",
          payload: {
            emails: [
              {
                id: "1",
                status: "Read",
                from: "me <me@example.com>",
                to: "you <you@example.com>",
                subject: "check this out",
                body:
                  "want to grow your member size? [click here](portscan|8.8.8.8)",
              },
            ],
          },
        });
      });
    });
  });

  afterEach(() => {
    mockServer.close();
  });

  it("allows malicious links in emails to send commands", () => {
    cy.visit("/", {
      onBeforeLoad(win) {
        // Call some code to initialize the fake server part using MockSocket
        cy.stub(win, "WebSocket" as any, (url: string) => new WebSocket(url));
      },
    });
    cy.findByLabelText("Enter Username").type(`threehams{enter}`);
    cy.getId("messages").should("contain.text", "Logged in as threehams");

    cy.findByText(/1 unread/i).click();
    cy.findByText(/check this out/i).click();
    cy.findByText("click here").click();
    cy.findByText("Back").click();
    cy.findByText("Close").click();
    cy.findByText("(0 unread)");
    cy.getId("messages").should("contain.text", "portscan 8.8.8.8");
  });

  it("can navigate email by keyboard", () => {
    cy.visit("/", {
      onBeforeLoad(win) {
        // Call some code to initialize the fake server part using MockSocket
        cy.stub(win, "WebSocket" as any, (url: string) => new WebSocket(url));
      },
    });
    cy.findByLabelText("Enter Username").type(`threehams{enter}`);
    cy.getId("messages").should("contain.text", "Logged in as threehams");

    cy.get("body").type("mail{enter}");

    cy.focused().should("contain.text", "check this out").click();

    cy.findByText("click here").click();
    cy.findByText("Back").click();
    cy.findByText("Close").click();
    cy.findByText("(0 unread)");
    cy.getId("messages").should("contain.text", "portscan 8.8.8.8");
  });
});
