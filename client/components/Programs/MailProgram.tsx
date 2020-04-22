import React, { useState, useContext } from "react";
import { Email } from "../../types";
import { Static } from "runtypes";
import { Markdown } from "../library/Markdown";
import { CommandLink } from "../library/CommandLink";
import { Link } from "../library/Link";
import { css } from "@emotion/react";
import { Box } from "..";
import { CommandContext } from "../CommandContext";

type MailProgramProps = {
  emails: Array<Static<typeof Email>>;
};
export const MailProgram = ({ emails }: MailProgramProps) => {
  const [selectedId, setSelectedId] = useState<string | null>(null);
  const { sendCommand } = useContext(CommandContext);

  if (selectedId) {
    const email = emails.find((mail) => mail.id === selectedId)!;

    return (
      <Box>
        <Box marginBottom={1}>
          <Link
            block
            href="back"
            onClick={() => {
              setSelectedId(null);
            }}
            highlightFocus
          >
            Back
          </Link>
        </Box>
        <div>
          <div>From: {email.from}</div>
          <div>To: {email.to}</div>
          <Box marginBottom={1}>Subject: {email.subject}</Box>
          <Markdown>{email.body}</Markdown>
        </div>
      </Box>
    );
  }
  const read = emails.filter((email) => email.status === "Read");
  const unread = emails.filter((email) => email.status === "Unread");

  return (
    <>
      <Box>
        <CommandLink href="close" highlightFocus>
          Close
        </CommandLink>
      </Box>
      <Box marginTop={1}>Unread</Box>
      {!unread.length && <Box>No unread messages.</Box>}
      {!!unread.length &&
        unread.map((email) => {
          return (
            <EmailListItem
              key={email.id}
              email={email}
              onClick={() => {
                sendCommand(`mail read ${email.id}`);
                setSelectedId(email.id);
              }}
            />
          );
        })}
      <Box marginTop={1}>Everything else</Box>
      {!read.length && <Box>No other messages.</Box>}
      {!!read.length &&
        read.map((email) => {
          return (
            <EmailListItem
              key={email.id}
              email={email}
              onClick={() => {
                sendCommand(`mail read ${email.id}`);
                setSelectedId(email.id);
              }}
            />
          );
        })}
    </>
  );
};

type EmailListItemProps = {
  email: Email;
  onClick: () => void;
};
const EmailListItem = ({ email, onClick }: EmailListItemProps) => {
  return (
    <div>
      <Link href="mail open" onClick={onClick}>
        <pre
          css={css`
            display: inline-block;
          `}
        >
          {email.from.slice(0, 8).padEnd(8)} |{" "}
        </pre>
        {email.subject}
      </Link>
    </div>
  );
};
