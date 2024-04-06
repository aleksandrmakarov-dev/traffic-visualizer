import { StationMarker } from "@/entities/station";
import { useStationContext } from "@/context/StationProvider";

export function MarkerList() {
  const { stations } = useStationContext();

  // useEffect(() => {
  //   const mappedStations = stations?.map((station): Station => {
  //     const stationSensors = sensors?.filter((s) => s.stationId == station.id);

  //     const stationRoadworks: RoadworkResponse[] = getStationRoadworks(
  //       station,
  //       roadworks ?? []
  //     );

  //     return {
  //       ...station,
  //       sensors: stationSensors,
  //       roadworks: stationRoadworks,
  //     };
  //   });

  //   setData(mappedStations);
  // }, [stations, roadworks, sensors]);

  return (
    <div>
      {stations.map((s) => (
        <StationMarker
          key={`station-marker-${s.id}`}
          station={s}
          isSelected={false}
        />
      ))}
    </div>
  );
}
