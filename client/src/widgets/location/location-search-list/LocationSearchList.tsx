import { useMapContext } from "@/context/MapProvider";
import { useSidebarContext } from "@/context/SidebarProvider";
import { useSearchLocation } from "@/entities/location";
import { LocationResponse } from "@/lib/contracts/location/location.response";
import { cn, getZoom } from "@/lib/utils";
import { FormAlert } from "@/shared/components/FormAlert";
import { Button } from "@/shared/components/ui/button";
import { Search } from "lucide-react";
import { useState } from "react";
import { useSearchParams } from "react-router-dom";

export function LocationSearchList() {
  const [searchParams, setSearchParams] = useSearchParams();
  const { setZoom, setCenter } = useMapContext();
  const { setKey } = useSidebarContext();
  const [selectedId, setSelectedId] = useState<number>(-1);

  const { data, isLoading, isSuccess, isError, error } = useSearchLocation({
    query: searchParams.get("q") ?? "",
  });

  const onSelect = (values: LocationResponse) => {
    setSelectedId(values.place_id);

    setZoom(getZoom(values.category));
    setCenter([values.lat, values.lon]);
  };

  const onCancel = () => {
    const queryParams = new URLSearchParams(searchParams);
    queryParams.delete("q");

    setSearchParams(queryParams);
    setKey(null);
  };

  return (
    <>
      <div className="overflow-auto h-full">
        {isLoading && (
          <div className="p-5 flex flex-col items-center animate-pulse text-center">
            <Search className="mb-1.5" />
            <p>Searching....</p>
          </div>
        )}
        {isError && <FormAlert isError={isError} error={error} />}
        {data?.map((item) => (
          <div
            key={item.place_id}
            className={cn(
              "p-5 border-b last:border-0 hover:bg-muted hover:cursor-pointer"
            )}
            onClick={() => onSelect(item)}
          >
            <p
              className={cn("mb-1", {
                "font-semibold": item.place_id === selectedId,
              })}
            >
              {item.display_name}
            </p>
            <p className="text-sm text-muted-foreground">{item.category}</p>
          </div>
        ))}
        {isSuccess && data.length === 0 && (
          <div className="p-5">
            <p className="font-medium">
              Maps can't find {searchParams.get("q")}
            </p>
            <p className="text-sm text-muted-foreground">
              Make sure your search is spelled correctly. Try adding a city,
              state, or zip code
            </p>
          </div>
        )}
      </div>
      <div className="p-2.5">
        <Button type="button" className="w-full" onClick={onCancel}>
          Close
        </Button>
      </div>
    </>
  );
}
