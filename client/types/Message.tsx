import { Array, Union, Number, Record, String, Null, Literal } from "runtypes";

const Port = Union(Literal(21), Literal(22), Literal(80));
const Status = Union(
  Literal("SUCCESS"),
  Literal("FAILURE"),
  Literal("PENDING"),
);
const Permission = Union(Literal("USER"), Literal("ROOT"));
const Portscan = Literal("PORTSCAN");
const Ssh = Literal("SSH");
const SshCrack = Literal("SSH_CRACK");

const Commands = Union(Portscan, Ssh, SshCrack);

const InitialStateMessage = Record({
  type: Literal("INITIAL_STATE"),
  payload: Record({
    nodeCount: Number,
    bitcoin: Number,
    availableCommands: Array(Commands),
  }),
});

const PortscanMessage = Record({
  type: Portscan,
  payload: Record({
    ip: String,
    status: Status,
    progress: Number,
    ports: Array(Port),
  }),
});

const SshMessage = Record({
  type: Ssh,
  payload: Record({
    ip: String,
    status: Status,
    permission: Permission,
  }),
});

const SshCrackMessage = Record({
  type: SshCrack,
  payload: Record({
    ip: String,
    status: Status,
    progress: Number,
    permission: Permission,
  }),
});

export const MessageData = Union(
  InitialStateMessage,
  SshCrackMessage,
  SshMessage,
  PortscanMessage,
);

export const Message = Union(
  Null,
  Record({
    data: MessageData,
  }),
);
