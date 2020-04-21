import styled from "@emotion/styled";
import {
  alignSelf,
  AlignSelfProps,
  background,
  BackgroundProps,
  display,
  DisplayProps,
  gridArea,
  GridAreaProps,
  height,
  HeightProps,
  justifySelf,
  JustifySelfProps,
  maxWidth,
  MaxWidthProps,
  overflow,
  OverflowProps,
  space,
  SpaceProps,
  width,
  WidthProps,
  BorderProps,
  border,
  BorderLeftProps,
  BorderRightProps,
  BorderTopProps,
  BorderBottomProps,
  borderLeft,
  borderRight,
  borderTop,
  borderBottom,
} from "styled-system";

export const Box = styled.div<
  BackgroundProps &
    DisplayProps &
    SpaceProps &
    MaxWidthProps &
    AlignSelfProps &
    JustifySelfProps &
    GridAreaProps &
    HeightProps &
    OverflowProps &
    WidthProps &
    BorderProps &
    BorderLeftProps &
    BorderRightProps &
    BorderTopProps &
    BorderBottomProps
>`
  ${alignSelf}
  ${background}
  ${border}
  ${borderLeft}
  ${borderRight}
  ${borderTop}
  ${borderBottom}
  ${display}
  ${gridArea}
  ${height}
  ${justifySelf}
  ${maxWidth}
  ${overflow}
  ${space}
  ${width}
`;
