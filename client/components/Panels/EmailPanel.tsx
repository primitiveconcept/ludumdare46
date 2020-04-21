import React from "react";
import { Email } from "../../types";
import { Static } from "runtypes";
import { CommandLink } from "../library/CommandLink";

type EmailPanelProps = {
  emails: Array<Static<typeof Email>>;
};
export const EmailPanel = ({ emails }: EmailPanelProps) => {
  const unreadCount = emails.filter((email) => email.status === "Unread")
    .length;
  const text = `(${unreadCount} unread)`;
  return (
    <div>
      <div>
        Mail <CommandLink href="mail">{text}</CommandLink>
      </div>
    </div>
  );
};
