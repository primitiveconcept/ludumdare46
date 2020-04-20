import React from "react";
import { Email } from "../types";
import { Static } from "runtypes";
import { Markdown } from "./Markdown";
import { CommandLink } from "./CommandLink";

type MailProgramProps = {
  emails: Array<Static<typeof Email>>;
};
export const MailProgram = ({ emails }: MailProgramProps) => {
  let body;
  if (!emails.length) {
    body = <div>You have no messages.</div>;
  } else {
    body = (
      <div>
        {emails.map((email, index) => {
          return (
            <div key={index}>
              <div>From: {email.from}</div>
              <div>To: {email.to}</div>
              <br />
              <Markdown>{email.text}</Markdown>
            </div>
          );
        })}
      </div>
    );
  }
  return (
    <div>
      <div>
        <CommandLink href="close">Close</CommandLink>
      </div>
      {body}
    </div>
  );
};
