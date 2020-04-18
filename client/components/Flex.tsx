import styled from "@emotion/styled";
import {
  alignContent,
  AlignContentProps,
  alignItems,
  AlignItemsProps,
  display,
  DisplayProps,
  flex,
  flexBasis,
  FlexBasisProps,
  flexDirection,
  FlexDirectionProps,
  FlexProps,
  flexWrap,
  FlexWrapProps,
  justifyContent,
  JustifyContentProps,
  maxWidth,
  MaxWidthProps,
  space,
  SpaceProps,
  WidthProps,
  width,
} from "styled-system";

export const Flex = styled.div<
  AlignContentProps &
    AlignItemsProps &
    DisplayProps &
    FlexProps &
    FlexBasisProps &
    FlexDirectionProps &
    FlexWrapProps &
    JustifyContentProps &
    MaxWidthProps &
    SpaceProps &
    WidthProps
>`
  ${alignContent}
  ${alignItems}
  ${display}
  ${flex}
  ${flexBasis}
  ${flexDirection}
  ${flexWrap}
  ${justifyContent}
  ${maxWidth}
  ${space}
  ${width}
`;

Flex.defaultProps = {
  display: "flex",
};
