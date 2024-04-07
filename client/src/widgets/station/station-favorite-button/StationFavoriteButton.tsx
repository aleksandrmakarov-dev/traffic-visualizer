import { useSession } from "@/context/SessionProvider";
import { stationKeys } from "@/entities/station";
import { useFavoriteStation } from "@/features/station/favorite";
import { Button } from "@/shared/components/ui/button";
import { useQueryClient } from "@tanstack/react-query";
import { Star } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { toast } from "sonner";

interface StationFavoriteButtonProps {
  stationId: string;
}

export function StationFavoriteButton({
  stationId,
}: StationFavoriteButtonProps) {
  const navigate = useNavigate();
  const { session } = useSession();

  const queryClient = useQueryClient();

  const { mutate, isPending } = useFavoriteStation();

  const onFavorite = () => {
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
              ?.concat(stationId)
          );
        },
      }
    );
  };

  const onToast = () => {
    toast("Only signed in users can use function", {
      description: "Sign in or create your account to add station to favorite",
      action: {
        label: "Sign in",
        onClick: () => {
          navigate("/auth/sign-up");
        },
      },
    });
  };

  return (
    <Button
      size="icon"
      variant="ghost"
      loading={isPending}
      onClick={session ? onFavorite : onToast}
    >
      <Star className="text-gray-500" />
    </Button>
  );
}
