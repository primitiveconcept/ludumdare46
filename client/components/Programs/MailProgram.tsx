import React, { useState } from "react";
import { Email } from "../../types";
import { Static } from "runtypes";
import { Markdown } from "../library/Markdown";
import { CommandLink } from "../library/CommandLink";
import { Link } from "../library/Link";
import { css } from "@emotion/react";
import { Box } from "..";

type MailProgramProps = {
  emails: Array<Static<typeof Email>>;
};
export const MailProgram = ({}: MailProgramProps) => {
  const emails = [
    {
      id: "1",
      from: "me <me@example.com>",
      to: "you <you@example.com>",
      subject: "check this out",
      body: "want to grow your member size? [click here](portscan|8.8.8.8)",
    },
  ];
  const [selectedId, setSelectedId] = useState<string | null>(null);
  let body;
  if (!emails.length) {
    body = <div>You have no messages.</div>;
  } else if (selectedId) {
    const email = emails.find((mail) => mail.id === selectedId)!;
    body = (
      <div>
        <div>From: {email.from}</div>
        <div>To: {email.to}</div>
        <Box marginBottom={1}>Subject: {email.subject}</Box>
        <Markdown>{email.body}</Markdown>
      </div>
    );
  } else {
    body = (
      <div>
        {emails.map((email, index) => {
          return (
            <div key={index}>
              <Link
                href="mail open"
                onClick={() => {
                  setSelectedId(email.id);
                }}
              >
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
        })}
      </div>
    );
  }
  return (
    <div>
      <Box marginBottom={1}>
        {!selectedId && (
          <CommandLink href="close" highlightFocus>
            Close
          </CommandLink>
        )}
        {selectedId && (
          <Link
            href="back"
            onClick={() => {
              setSelectedId(null);
            }}
            highlightFocus
          >
            Back
          </Link>
        )}
      </Box>
      {body}
    </div>
  );
};
