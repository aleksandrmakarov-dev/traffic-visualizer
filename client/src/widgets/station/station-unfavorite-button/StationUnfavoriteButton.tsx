import { stationKeys } from "@/entities/station";
import { useUnfavoriteStation } from "@/features/station/unfavorite";
import { Button } from "@/shared/components/ui/button";
import { useQueryClient } from "@tanstack/react-query";
import { Star } from "lucide-react";

interface StationUnfavoriteButtonProps {
  stationId: string;
}

export function StationUnfavoriteButton({
  stationId,
}: StationUnfavoriteButtonProps) {
  const queryClient = useQueryClient();

  const { mutate, isPending } = useUnfavoriteStation();

  const onClick = () => {
    mutate(
      {
        stationId: stationId,
      },
      {
        onSuccess: () => {
          queryClient.cancelQueries({
            queryKey: stationKeys.stations.favorite(),
          });

          queryClient.setQueryData<string[]>(
            stationKeys.stations.favorite(),
            queryClient
              .getQueryData<string[]>(stationKeys.stations.favorite())
              ?.filter((id) => id !== stationId)
          );
        },
      }
    );
  };

  return (
    <Button
      className="hover:bg-yellow-100/80"
      size="icon"
      variant="ghost"
      loading={isPending}
      onClick={onClick}
    >
      <Star fill="currentColor" className="text-yellow-500" />
    </Button>
  );
}
