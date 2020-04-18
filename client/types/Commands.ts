export type SshCommand = {
  type: "ssh";
  payload: {
    ip: string;
    username: string;
    password: string;
  };
};

export type PortscanCommand = {
  type: "portscan";
  payload: {
    ip: string;
  };
};

export type SshCrackCommand = {
  type: "sshCrack";
  payload: {
    ip: string;
  };
};
