interface SensorItemProps {
  label?: string;
  value?: number;
  units: string;
}

export function SensorItem({ label, value, units }: SensorItemProps) {
  return (
    <li>
      {label && <span>{label}: </span>}
      <span>{value ?? "-"}</span>
      <span> {units}</span>
    </li>
  );
}
