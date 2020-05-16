interface Array<T> {
  includes(
    searchElement: T extends string
      ? string
      : T extends number
      ? number
      : T extends boolean
      ? boolean
      : T,
    fromIndex?: number,
  ): boolean;

  indexOf(
    searchElement: T extends string
      ? string
      : T extends number
      ? number
      : T extends boolean
      ? boolean
      : T,
    fromIndex?: number,
  ): number;
}
