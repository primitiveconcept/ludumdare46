import styled from "@emotion/styled";
import {
  display,
  DisplayProps,
  gridTemplateAreas,
  GridTemplateAreasProps,
  gridTemplateRows,
  GridTemplateRowsProps,
  height,
  HeightProps,
  maxWidth,
  MaxWidthProps,
  space,
  SpaceProps,
  GridTemplateColumnsProps,
  gridTemplateColumns,
} from "styled-system";

export const Grid = styled.div<
  DisplayProps &
    MaxWidthProps &
    SpaceProps &
    HeightProps &
    GridTemplateAreasProps &
    GridTemplateRowsProps &
    GridTemplateColumnsProps
>`
  ${gridTemplateAreas}
  ${gridTemplateRows}
  ${gridTemplateColumns}
  ${display}
  ${height}
  ${maxWidth}
  ${space}
`;

Grid.defaultProps = {
  display: "grid",
};
