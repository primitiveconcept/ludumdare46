export type SshCommand = {
  type: "SSH";
  payload: {
    ip: string;
    username: string;
    password: string;
  };
};

export type PortscanCommand = {
  type: "PORTSCAN";
  payload: {
    ip: string;
  };
};

export type SshCrackCommand = {
  type: "SSH_CRACK";
  payload: {
    ip: string;
  };
};

export type InitialStateCommand = {
  type: "INITIAL_STATE";
};

export type Command =
  | SshCommand
  | PortscanCommand
  | SshCrackCommand
  | InitialStateCommand;
