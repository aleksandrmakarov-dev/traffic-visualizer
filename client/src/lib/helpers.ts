import { RoadworkResponse } from "./contracts/roadwork/roadwork.response";
import { StationResponse } from "./contracts/station/station.response";

export function getStationRoadworks(
  station: StationResponse,
  roadworks: RoadworkResponse[]
) {
  if (roadworks.length === 0) return [];
  const stationRoadNumber = station.roadNumber;
  const stationRoadSection = station.roadSection;
  const stationRoadworks1 = roadworks
    .filter((rw) => rw.primaryPointRoadNumber === stationRoadNumber)
    .filter((rw) => rw.primaryPointRoadSection === stationRoadSection);
  const stationRoadworks2 = roadworks
    .filter((rw) => rw.secondaryPointRoadNumber === stationRoadNumber)
    .filter((rw) => rw.secondaryPointRoadSection === stationRoadSection);
  const stationRoadworks = Array.from(
    new Set([...stationRoadworks1, ...stationRoadworks2])
  );
  return stationRoadworks;
}
