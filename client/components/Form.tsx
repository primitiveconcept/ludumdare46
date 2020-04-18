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
  JustifyItemsProps,
  justifyItems,
} from "styled-system";

export const Form = styled.form<
  BackgroundProps &
    DisplayProps &
    SpaceProps &
    MaxWidthProps &
    AlignSelfProps &
    JustifySelfProps &
    JustifyItemsProps &
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
  ${justifyItems}
  ${justifySelf}
  ${maxWidth}
  ${overflow}
  ${space}
  ${width}
`;
