import { SensorResponse } from "../sensor/sensor.response";
import { Coordinates } from "./station.response";

export type StationHistoryResponse = {
  id: string;
  stationId: string;
  tmsNumber: number;
  dataUpdatedTime: string;
  name: string;
  coordinates: Coordinates;
  sensors: SensorResponse[];
};
