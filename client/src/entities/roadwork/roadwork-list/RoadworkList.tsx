interface RoadworkListProps<T> {
  items?: T[];
  render: (item: T) => React.ReactNode;
}

export function RoadworkList<T>({ items, render }: RoadworkListProps<T>) {
  return <ul>{items?.map((item) => render(item))}</ul>;
}
