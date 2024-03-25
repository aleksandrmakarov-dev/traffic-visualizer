import { useQuery } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { ErrorResponse } from "@/lib/contracts/common/error.response";
import axios from "@/lib/axios";
import { SensorRequest } from "@/lib/contracts/sensor/sensor.request";
import { SensorResponse } from "@/lib/contracts/sensor/sensor.response";

export const sensorKeys = {
  sensors: {
    root: ["sensor"],
    id: (id: string) => [...sensorKeys.sensors.root, "id", id],
    query: (...params: any[]) => [...sensorKeys.sensors.root, "query", params],
  },
  mutations: {
    create: () => [...sensorKeys.sensors.root, "create"],
    update: () => [...sensorKeys.sensors.root, "update"],
    delete: () => [...sensorKeys.sensors.root, "delete"],
  },
};

async function fetchSensors(request: SensorRequest): Promise<SensorResponse[]> {
  const params = new URLSearchParams();

  if (request.ids) {
    for (const id of request.ids) {
      params.append("ids", id);
    }
  }

  if (request.stationId) {
    params.append("stationId", request.stationId);
  }

  const response = await axios.get<SensorResponse[]>(`/sensors`, {
    params,
  });

  return response.data;
}

export const useSensors = (request: SensorRequest, lastUpdate?: Date) => {
  return useQuery<
    SensorResponse[],
    AxiosError<ErrorResponse>,
    SensorResponse[],
    unknown[]
  >({
    queryKey: sensorKeys.sensors.query(request, lastUpdate),
    queryFn: async () => {
      return await fetchSensors(request);
    },
  });
};
