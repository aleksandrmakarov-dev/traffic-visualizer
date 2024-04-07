import { StationMarker } from "@/entities/station";
import { useStationContext } from "@/context/StationProvider";

export function MarkerList() {
  const { stations } = useStationContext();

  return (
    <div>
      {stations.map((s) => (
        <StationMarker key={`station-marker-${s.id}`} station={s} />
      ))}
    </div>
  );
}
