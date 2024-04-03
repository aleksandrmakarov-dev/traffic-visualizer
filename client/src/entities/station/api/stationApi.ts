import { useQuery } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { ErrorResponse } from "@/lib/contracts/common/error.response";
import axios from "@/lib/axios";
import { StationRequest } from "@/lib/contracts/station/station.request";
import { StationResponse } from "@/lib/contracts/station/station.response";

export const stationKeys = {
  stations: {
    root: ["station"],
    id: (id: string) => [...stationKeys.stations.root, "id", id],
    query: (...params: any[]) => [
      ...stationKeys.stations.root,
      "query",
      ...params,
    ],
  },
  mutations: {
    create: () => [...stationKeys.stations.root, "create"],
    update: () => [...stationKeys.stations.root, "update"],
    delete: () => [...stationKeys.stations.root, "delete"],
  },
};

async function fetchStations(
  request: StationRequest
): Promise<StationResponse[]> {
  const response = await axios.get<StationResponse[]>(`/stations`);
  return response.data;
}

export const useStations = (request: StationRequest, lastUpdate?: number) => {
  return useQuery<
    StationResponse[],
    AxiosError<ErrorResponse>,
    StationResponse[],
    unknown[]
  >({
    queryKey: stationKeys.stations.query(request, lastUpdate),
    queryFn: async () => {
      return await fetchStations(request);
    },
  });
};
