import { stationKeys } from "@/entities/station";
import axios from "@/lib/axios";
import { ErrorResponse } from "@/lib/contracts/common/error.response";
import { MessageResponse } from "@/lib/contracts/common/message.response";
import { FavoriteStationRequest } from "@/lib/contracts/station/favorite-station.request";
import { useMutation } from "@tanstack/react-query";
import { AxiosError } from "axios";

async function favoriteStation(request: FavoriteStationRequest) {
  const response = await axios.post<MessageResponse>(
    "/stations/favorite",
    request
  );

  return response.data;
}

export const useFavoriteStation = () => {
  return useMutation<
    MessageResponse,
    AxiosError<ErrorResponse>,
    FavoriteStationRequest
  >({
    mutationKey: stationKeys.mutations.favorite(),
    mutationFn: async (data) => await favoriteStation(data),
  });
};
