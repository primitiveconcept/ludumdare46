import React, { useState } from "react";
import { Markdown } from "../library/Markdown";
import { State } from "../../types/State";

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
        <div>Known Devices</div>
        <br />
        {devices.map((device) => {
          return (
            <div key={device.ip}>
              <a
                href={device.ip}
                data-test="knownIp"
                onClick={(event) => {
                  event.preventDefault();
                  setCurrentIp(device.ip);
                }}
              >
                {device.ip}
              </a>
              <div>&nbsp;{device.status}</div>
              <div>&nbsp;</div>
            </div>
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
        <div>Known Devices</div>
        <br />
        <div>{device.ip}</div>
        {commands[currentCategory].map((command) => {
          return (
            <div key={command}>
              &nbsp;<Markdown>{command}</Markdown>
            </div>
          );
        })}
        <div>
          <a
            href="back"
            onClick={(event) => {
              event.preventDefault();
              setCurrentCategory(null);
            }}
          >
            Back
          </a>
        </div>
      </>
    );
  }

  return (
    <>
      <div>Known Devices</div>
      <br />
      <div>{device.ip}</div>
      {commands.main.map((command) => {
        return (
          <div key={command}>
            &nbsp;<Markdown>{command}</Markdown>
          </div>
        );
      })}
      {!!commands.install.length && (
        <div>
          <a
            href="install"
            onClick={(event) => {
              event.preventDefault();
              setCurrentCategory("install");
            }}
          >
            Install Malware
          </a>
        </div>
      )}
      <div>
        &nbsp;
        <a
          href="back"
          onClick={(event) => {
            event.preventDefault();
            setCurrentIp(null);
          }}
        >
          Back
        </a>
      </div>
      <br />
    </>
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
