import React from "react";
import { format } from "date-fns";
import { Markdown } from "../library/Markdown";
import { PortscanProcess } from "../../types/PortscanProcess";
import { CommandLink } from "../library/CommandLink";
import table from "markdown-table";

type TemplateValues = {
  startDate: Date;
  ip: string;
  latency: number;
  ports: Array<{ name: string; number: number }>;
};
const template = ({ startDate, ip, latency, ports }: TemplateValues) => `
Starting pscan 4.3.3 at ${format(startDate, "yyyy-mm-dd")}  
Scan report for ${ip}  
Host is up (latency ${latency}ms)  

${table([
  ["PORT", "STATE", "SERVICE"],
  ...ports.map((port) => {
    return [`${port.number}/tcp`, "open", port.name];
  }),
])}
`;

const startDate = new Date();

type Props = {
  process: PortscanProcess;
};
export const PortscanProgram = ({ process }: Props) => {
  return (
    <div data-test="portscanProgram">
      <CommandLink marginBottom={1} block href="background">
        Close
      </CommandLink>
      <Markdown>
        {template({
          startDate,
          ip: process.target,
          latency: 27,
          ports: process.ports,
        })}
      </Markdown>
    </div>
  );
};