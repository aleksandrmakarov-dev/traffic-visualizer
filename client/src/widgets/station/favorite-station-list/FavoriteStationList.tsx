import { useMapContext } from "@/context/MapProvider";
import { useSidebarContext } from "@/context/SidebarProvider";
import { useStationContext } from "@/context/StationProvider";
import { Station } from "@/lib/contracts/station/station";
import { cn } from "@/lib/utils";
import { Button } from "@/shared/components/ui/button";
import { useEffect, useState } from "react";

export function FavoriteStationList() {
  const { setKey } = useSidebarContext();
  const { favoriteStations, stations } = useStationContext();
  const { setCenter, setZoom } = useMapContext();

  const [favorites, setFavorites] = useState<Station[]>([]);
  const [currentFavorite, setCurrentFavorite] = useState<string | null>(null);

  useEffect(() => {
    setFavorites(stations.filter((st) => favoriteStations?.includes(st.id)));
  }, [favoriteStations, stations]);

  const onNavigate = (station: Station) => {
    setZoom(14);
    const { latitude, longitude } = station.coordinates;
    setCenter([latitude, longitude]);
    setCurrentFavorite(station.id);
  };

  return (
    <>
      <h4 className="text-xl font-medium p-5">Favorite Stations</h4>
      <ul className="overflow-auto">
        {favorites.map((favorite) => (
          <li
            key={favorite.id}
            className={cn(
              "p-5 border-border border-t hover:bg-muted cursor-pointer",
              { "font-medium": currentFavorite === favorite.id }
            )}
            onClick={() => onNavigate(favorite)}
          >
            {favorite.names.en}
          </li>
        ))}
      </ul>
      <div className="p-2.5">
        <Button className="w-full" onClick={() => setKey(null)}>
          Close
        </Button>
      </div>
    </>
  );
}
