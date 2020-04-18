import {
  Array,
  Union,
  Number,
  Record,
  String,
  Literal,
  Partial,
} from "runtypes";

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
export const Device = Record({
  ip: String,
});

const InitialStateMessage = Record({
  type: Literal("INITIAL_STATE"),
  payload: Record({
    nodeCount: Number,
    bitcoin: Number,
    availableCommands: Array(Commands),
    knownDevices: Array(Device),
  }).And(
    Partial({
      message: String,
    }),
  ),
});

const PortscanMessage = Record({
  type: Portscan,
  payload: Record({
    ip: String,
    status: Status,
    progress: Number,
    ports: Array(Port),
  }).And(
    Partial({
      message: String,
    }),
  ),
});

const SshMessage = Record({
  type: Ssh,
  payload: Record({
    ip: String,
    status: Status,
    permission: Permission,
  }).And(
    Partial({
      message: String,
    }),
  ),
});

const SshCrackMessage = Record({
  type: SshCrack,
  payload: Record({
    ip: String,
    status: Status,
    progress: Number,
    permission: Permission,
  }).And(
    Partial({
      message: String,
    }),
  ),
});

export const MessageData = Union(
  InitialStateMessage,
  SshCrackMessage,
  SshMessage,
  PortscanMessage,
);
