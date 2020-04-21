export {};

declare module "@emotion/react" {
  export interface Theme {
    tileWidth: number;
    tileHeight: number;
    spaceX: number[];
    spaceY: number[];
  }
}
