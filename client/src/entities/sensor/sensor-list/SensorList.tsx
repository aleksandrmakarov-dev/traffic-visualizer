interface SensorListProps<T> {
  items?: T[];
  render: (item: T) => React.ReactNode;
}

export function SensorList<T>({ items, render }: SensorListProps<T>) {
  return <ul>{items?.map((item) => render(item))}</ul>;
}
