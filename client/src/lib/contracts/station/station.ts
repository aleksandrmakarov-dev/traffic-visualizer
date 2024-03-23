import { RoadworkResponse } from "../roadwork/roadwork.response";
import { SensorResponse } from "../sensor/sensor.response";
import { StationResponse } from "./station.response";

export type Station = StationResponse & {
  sensors?: SensorResponse[];
  roadworks?: RoadworkResponse[];
};
