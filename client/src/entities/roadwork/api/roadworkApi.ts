import { useQuery } from "@tanstack/react-query";
import { AxiosError } from "axios";
import { ErrorResponse } from "@/lib/contracts/common/error.response";
import axios from "@/lib/axios";
import { RoadworkRequest } from "@/lib/contracts/roadwork/roadwork.request";
import { RoadworkResponse } from "@/lib/contracts/roadwork/roadwork.response";

export const roadworkKeys = {
  roadworks: {
    root: ["roadwork"],
    id: (id: string) => [...roadworkKeys.roadworks.root, "id", id],
    query: (...params: any[]) => [...roadworkKeys.roadworks.root, "query", params],
  },
  mutations: {
    create: () => [...roadworkKeys.roadworks.root, "create"],
    update: () => [...roadworkKeys.roadworks.root, "update"],
    delete: () => [...roadworkKeys.roadworks.root, "delete"],
  },
};

async function fetchRoadworks(request: RoadworkRequest):Promise<RoadworkResponse[]> {
  const response = await axios.get<RoadworkResponse[]>(`/roadworks`);
  return response.data;
}

export const useRoadworks = (request: RoadworkRequest,lastUpdate?:Date) => {
  return useQuery<RoadworkResponse[],AxiosError<ErrorResponse>,RoadworkResponse[],unknown[]>({
    queryKey: roadworkKeys.roadworks.query(request,lastUpdate),
    queryFn: async () => {
      return await fetchRoadworks(request);
    },
  });
};