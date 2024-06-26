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
    favorite: () => [...stationKeys.stations.root, "favorite"],
  },
  mutations: {
    create: () => [...stationKeys.stations.root, "create"],
    update: () => [...stationKeys.stations.root, "update"],
    delete: () => [...stationKeys.stations.root, "delete"],
    favorite: () => [...stationKeys.stations.root, "favorite"],
    unfavorite: () => [...stationKeys.stations.root, "unfavorite"],
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
    `/stations/${request.id}/history?timeRange=${request.timeRange}`
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
    queryKey: stationKeys.stations.query(request.id),
    queryFn: async () => {
      return await fetchStationHistoryById(request);
    },
    ...options,
  });
};

type UseFavoriteStationsQuery = UseQueryOptions<
  string[],
  AxiosError<ErrorResponse>,
  string[],
  unknown[]
>;

type UseFavoriteStationsOptions = Omit<
  UseFavoriteStationsQuery,
  "queryKey" | "queryFn"
>;

async function fetchFavoriteStations(): Promise<string[]> {
  const response = await axios.get<string[]>(`/stations/favorite`);
  return response.data;
}

export const useFavoriteStations = (options?: UseFavoriteStationsOptions) => {
  return useQuery<string[], AxiosError<ErrorResponse>, string[], unknown[]>({
    queryKey: stationKeys.stations.favorite(),
    queryFn: async () => {
      return await fetchFavoriteStations();
    },
    ...options,
  });
};
