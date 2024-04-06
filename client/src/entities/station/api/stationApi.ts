import { UseQueryOptions, useQuery } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { ErrorResponse } from "@/lib/contracts/common/error.response";
import axios from "@/lib/axios";
import { StationResponse } from "@/lib/contracts/station/station.response";
import { StationHistoryRequest } from "@/lib/contracts/station/station-history.request";
import { StationHistoryResponse } from "@/lib/contracts/station/station-history.response";

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

type UseStationsQuery = UseQueryOptions<
  StationResponse[],
  AxiosError<ErrorResponse>,
  StationResponse[],
  unknown[]
>;

type UseStationsOptions = Omit<UseStationsQuery, "queryKey" | "queryFn">;

async function fetchStations(): Promise<StationResponse[]> {
  const response = await axios.get<StationResponse[]>(`/stations`);
  return response.data;
}

export const useStations = (options?: UseStationsOptions) => {
  return useQuery<
    StationResponse[],
    AxiosError<ErrorResponse>,
    StationResponse[],
    unknown[]
  >({
    queryKey: stationKeys.stations.query(),
    queryFn: async () => {
      return await fetchStations();
    },
    ...options,
  });
};

async function fetchStationHistoryById(
  request: StationHistoryRequest
): Promise<StationHistoryResponse> {
  const response = await axios.get<StationHistoryResponse>(
    `/stations/${request.id}/history`
  );
  return response.data;
}

type UseStationsHistoryByIdQuery = UseQueryOptions<
  StationHistoryResponse,
  AxiosError<ErrorResponse>,
  StationHistoryResponse,
  unknown[]
>;

type UseStationsHistoryByIdOptions = Omit<
  UseStationsHistoryByIdQuery,
  "queryKey" | "queryFn"
>;

export const useStationsHistoryById = (
  request: StationHistoryRequest,
  options?: UseStationsHistoryByIdOptions
) => {
  return useQuery<
    StationHistoryResponse,
    AxiosError<ErrorResponse>,
    StationHistoryResponse,
    unknown[]
  >({
    queryKey: stationKeys.stations.query(request),
    queryFn: async () => {
      return await fetchStationHistoryById(request);
    },
    ...options,
  });
};
