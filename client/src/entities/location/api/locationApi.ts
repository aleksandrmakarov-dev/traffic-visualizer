import { useQuery } from "@tanstack/react-query";

// keys

import { LocationRequest } from "@/lib/contracts/location/location.request";
import { LocationResponse } from "@/lib/contracts/location/location.response";
import axios, { AxiosError } from "axios";
import { ErrorResponse } from "@/lib/contracts/common/error.response";

export const locationKeys = {
  locations: {
    root: ["location"],
    id: (id: string) => [...locationKeys.locations.root, "id", id],
    query: (params?: any) => [...locationKeys.locations.root, "query", params],
  },
  mutations: {
    create: () => [...locationKeys.locations.root, "create"],
    update: () => [...locationKeys.locations.root, "update"],
    delete: () => [...locationKeys.locations.root, "delete"],
  },
};

async function fetchlocations(request: LocationRequest):Promise<LocationResponse[]> {
  const response = await axios.get<LocationResponse[]>(`${import.meta.env.VITE_BACKEND_BASE_URL}/locations/search?query=${request.query}`);
  return response.data;
}

export const useSearchLocation = (params: LocationRequest) => {
  return useQuery<LocationResponse[],AxiosError<ErrorResponse>,LocationResponse[],unknown[]>({
    queryKey: locationKeys.locations.query(params),
    queryFn: async () => {
      return await fetchlocations(params);
    },
    enabled:!!params.query
  });
};