import { WebSocket } from "mock-socket";
import { createMockSocket } from "../support/createMockSocket";

describe("filesystem", () => {
  beforeEach(() => {
    createMockSocket(({ onCommand, sendMessage }) => {
      onCommand("internal_login threehams", () => {
        sendMessage(100, {
          update: "Filesystem",
          payload: {
            ip: "8.8.8.8",
            contents: [
              {
                id: "1",
                contents: ["2", "3"],
                type: "Folder" as const,
                name: "warez",
              },
              {
                id: "2",
                contents: [],
                type: "Folder" as const,
                name: "mp3",
              },
              {
                id: "3",
                type: "File" as const,
                name: "doom2-full.exe",
              },
            ],
            root: ["1"],
          },
        });
      });
    }).as("mockServer");
  });

  it("allows filesystem navigation", () => {
    cy.visit("/", {
      onBeforeLoad(win) {
        // Call some code to initialize the fake server part using MockSocket
        cy.stub(win, "WebSocket" as any, (url: string) => new WebSocket(url));
      },
    });
    cy.findByLabelText("Enter Username").type(`threehams{enter}`);
    cy.getId("messages").should("contain.text", "Logged in as threehams");
    cy.getId("commandPrompt").should("contain.text", "threehams@local:/$");
    cy.get("body").type("ls{enter}");
    cy.getId("messages").should("contain.text", "warez/");
    cy.get("body").type("cd warez{enter}");
    cy.getId("commandPrompt").should("contain.text", "threehams@local:/warez$");
    cy.get("body").type("ls{enter}");
    cy.getId("messages").should("contain.text", "mp3/");
    cy.getId("messages").should("contain.text", "doom2-full.exe");
    cy.get("body").type("cd mp3{enter}");
    cy.getId("commandPrompt").should(
      "contain.text",
      "threehams@local:/warez/mp3$",
    );
    cy.get("body").type("cd ..{enter}");
    cy.getId("commandPrompt").should("contain.text", "threehams@local:/warez$");

    cy.get("body").type("cd ..{enter}");
    cy.getId("commandPrompt").should("contain.text", "threehams@local:/$");
    cy.get("body").type("ls{enter}");
  });

  it("rejects nonexistent folders", () => {
    cy.visit("/", {
      onBeforeLoad(win) {
        // Call some code to initialize the fake server part using MockSocket
        cy.stub(win, "WebSocket" as any, (url: string) => new WebSocket(url));
      },
    });
    cy.findByLabelText("Enter Username").type(`threehams{enter}`);
    cy.getId("messages").should("contain.text", "Logged in as threehams");
    cy.get("body").type("cd nope{enter}");
    cy.getId("messages").should(
      "contain.text",
      "cd: nope: directory not found",
    );
    cy.getId("commandPrompt").should("contain.text", "threehams@local:/$");
    cy.get("body").type("cd ..{enter}");
    cy.getId("commandPrompt").should("contain.text", "threehams@local:/$");
    cy.get("body").type("ls{enter}");
    cy.getId("messages").should("contain.text", "warez/");
    cy.get("body").type("cd warez/mp3{enter}");
    cy.getId("commandPrompt").should(
      "contain.text",
      "threehams@local:/warez/mp3$",
    );
    cy.get("body").type("cd /{enter}");
    cy.getId("commandPrompt").should("contain.text", "threehams@local:/$");
  });
});
