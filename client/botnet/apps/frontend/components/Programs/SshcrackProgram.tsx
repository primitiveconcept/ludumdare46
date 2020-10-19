import React, { useEffect } from 'react';
import { format } from 'date-fns';
import { Markdown } from '../library/Markdown';
import { SshCrackProcess } from '../../types/SshCrackProcess';
import { CommandLink } from '../library/CommandLink';
import { dictionary } from '../../server/features/sshcrack/dictionary';
import { useSteppedScroll } from '../../hooks/useSteppedScroll';

type TemplateValues = {
  startDate: Date;
  ip: string;
  progress: number;
  complete: boolean;
  error: string | null;
};
const template = ({
  startDate,
  ip,
  progress,
  complete,
  error,
}: TemplateValues): string => {
  const header = `brute v4.4 starting at ${format(startDate, 'yyyy-mm-dd')}`;
  const attempts = dictionary
    .slice(0, (progress / 100) * Math.floor(dictionary.length - 1))
    .map((word) => {
      return `- [-] ${ip} - Failed: 'user:${word}'`;
    })
    .join('\n');
  if (!complete) {
    return `${header}

${attempts}`;
  }
  return `${header}

${attempts}
${!error ? `- [+] ${ip} - Success: 'msfadmin:Password123'  ` : ''}

[*] Command shell session  
[*] Scanned 1 of 1 hosts (100% complete)  
`;
};

const startDate = new Date();

type Props = {
  process: SshCrackProcess;
};
export const SshCrackProgram = ({ process }: Props) => {
  const scrollToBottom = useSteppedScroll();

  useEffect(() => {
    scrollToBottom();
  }, [scrollToBottom, process.progress]);

  return (
    <div data-test="sshCrackProgram">
      <CommandLink marginBottom={1} block href="background">
        Close
      </CommandLink>
      <Markdown>
        {template({
          startDate,
          ip: process.target,
          progress: process.progress,
          complete: process.complete,
          error: process.error,
        })}
      </Markdown>
    </div>
  );
};
