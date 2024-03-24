import { useEffect, useState } from "react";
import { MarkerContent } from "./MarkerContent";
import { useRoadworks } from "@/entities/roadwork";
import { useStationContext } from "@/context/StationContext";
import { getStationRoadworks } from "../scripts/getStationRoadworks";
import { useSensors } from "@/entities/sensor";
import { useStations } from "@/entities/station";
import { Station } from "@/lib/contracts/station/station";

// sensors that contains information about left and right road side
const sensorIds: string[] = ["5158", "5161"];

export function MarkerList(): JSX.Element | null {
  const { roadworksUpdatedAt, sensorsUpdatedAt, stationsUpdatedAt } =
    useStationContext();

  const { data: roadworks } = useRoadworks({}, roadworksUpdatedAt);
  const { data: sensors } = useSensors({ ids: sensorIds }, sensorsUpdatedAt);
  const { data: stations } = useStations({}, stationsUpdatedAt);

  const [data, setData] = useState<Station[] | undefined>();

  useEffect(() => {
    const mappedStations = stations?.map((station): Station => {
      const stationSensors = sensors?.filter((s) => s.stationId == station.id);

      const stationRoadworks = roadworks
        ? getStationRoadworks(station, roadworks)
        : [];

      return {
        ...station,
        sensors: stationSensors,
        roadworks: stationRoadworks,
      };
    });

    setData(mappedStations);
  }, [stations, roadworks, sensors]);

  return (
    <div>
      {data?.map((s) => (
        <MarkerContent key={s.id} station={s} />
      ))}
    </div>
  );
}
