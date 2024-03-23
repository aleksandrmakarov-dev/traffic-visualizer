export type SensorResponse = {
  id: string;
  sensorId: string;
  stationId: string;
  name: string;
  timeWindowStart: string | null;
  timeWindowEnd: string | null;
  measuredTime: string | null;
  unit: string;
  value: number;
};
