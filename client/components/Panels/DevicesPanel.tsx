import React, { useState } from "react";
import { Markdown } from "../library/Markdown";
import { State } from "../../types/State";
import { Link } from "../library/Link";
import { Box } from "..";

type Category = "install";
type DevicesPanelProps = {
  devices: State["devices"];
};
export const DevicesPanel = ({ devices }: DevicesPanelProps) => {
  const [currentIp, setCurrentIp] = useState<string | null>(null);
  const [currentCategory, setCurrentCategory] = useState<Category | null>(null);
  if (!devices.length) {
    return null;
  }
  if (!currentIp) {
    return (
      <>
        <Box marginBottom={1}>Known Devices</Box>
        {devices.map((device) => {
          return (
            <Box key={device.ip} paddingBottom={1}>
              <Link
                href={device.ip}
                data-test="knownIp"
                onClick={() => {
                  setCurrentIp(device.ip);
                }}
              >
                {device.ip}
              </Link>
              <Box paddingLeft={1}>{device.status}</Box>
            </Box>
          );
        })}
      </>
    );
  }

  const device = devices.find((dev) => dev.ip === currentIp)!;
  const commands = {
    main: device.commands.filter((command) => {
      return baseCommand(command) !== "install";
    }),
    install: device.commands.filter((command) => {
      return baseCommand(command) === "install";
    }),
  };

  if (currentCategory) {
    return (
      <>
        <Box marginBottom={1}>Known Devices</Box>
        <div>{device.ip}</div>
        {commands[currentCategory].map((command) => {
          return (
            <Box key={command} paddingLeft={1}>
              <Markdown>{command}</Markdown>
            </Box>
          );
        })}
        <div>
          <Link
            href="back"
            onClick={() => {
              setCurrentCategory(null);
            }}
          >
            Back
          </Link>
        </div>
      </>
    );
  }

  return (
    <Box paddingBottom={1}>
      <Box marginBottom={1}>Known Devices</Box>
      <div>{device.ip}</div>
      {commands.main.map((command) => {
        return (
          <Box key={command} paddingLeft={1}>
            <Markdown>{command}</Markdown>
          </Box>
        );
      })}
      {!!commands.install.length && (
        <div>
          <Link
            href="install"
            onClick={() => {
              setCurrentCategory("install");
            }}
          >
            Install Malware
          </Link>
        </div>
      )}
      <Box paddingLeft={1}>
        <Link
          href="back"
          onClick={() => {
            setCurrentIp(null);
          }}
        >
          Back
        </Link>
      </Box>
    </Box>
  );
};

// copy/paste from https://github.com/probablyup/markdown-to-jsx/blob/master/index.js
// pfffffft this is awful, but it's quicker than changing the backend response!
const linkRegex = /\[(.+)\]\(([^ ]+?)( "(.+)")?\)/;

const baseCommand = (markdown: string) => {
  const match = linkRegex.exec(markdown);
  if (!match) {
    throw new Error(
      `Received a device command with no valid Markdown link: ${markdown}`,
    );
  }
  return match[2].split("|")[0];
};
