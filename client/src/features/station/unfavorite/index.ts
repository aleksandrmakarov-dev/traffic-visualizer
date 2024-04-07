import { stationKeys } from "@/entities/station";
import axios from "@/lib/axios";
import { ErrorResponse } from "@/lib/contracts/common/error.response";
import { UnfavoriteStationRequest } from "@/lib/contracts/station/unfavorite-station.request";
import { useMutation } from "@tanstack/react-query";
import { AxiosError } from "axios";

async function unfavoriteStation(request: UnfavoriteStationRequest) {
  const response = await axios.delete<null>(
    `/stations/favorite/${request.stationId}`
  );

  return response.data;
}

export const useUnfavoriteStation = () => {
  return useMutation<null, AxiosError<ErrorResponse>, UnfavoriteStationRequest>(
    {
      mutationKey: stationKeys.mutations.favorite(),
      mutationFn: async (data) => await unfavoriteStation(data),
    }
  );
};
