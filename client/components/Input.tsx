import styled from "@emotion/styled";
import {
  alignSelf,
  AlignSelfProps,
  display,
  DisplayProps,
  gridArea,
  GridAreaProps,
  justifySelf,
  JustifySelfProps,
  maxWidth,
  MaxWidthProps,
  overflow,
  OverflowProps,
  space,
  SpaceProps,
} from "styled-system";

export const Input = styled.input<
  DisplayProps &
    SpaceProps &
    MaxWidthProps &
    AlignSelfProps &
    JustifySelfProps &
    GridAreaProps &
    OverflowProps
>`
  background: transparent;
  border: 0;
  ${alignSelf}
  ${display}
  ${gridArea}
  ${justifySelf}
  ${maxWidth}
  ${overflow}
  ${space}
`;
