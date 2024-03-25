import { useStationContext } from "@/context/StationContext";
import { LocationSearchForm, useSearchLocation } from "@/entities/location";
import { LocationRequest } from "@/lib/contracts/location/location.request";
import { LocationResponse } from "@/lib/contracts/location/location.response";
import { cn } from "@/lib/utils";
import { Button } from "@/shared/components/ui/button";
import { HTMLAttributes, useState } from "react";

interface LocationSearchProps extends HTMLAttributes<HTMLDivElement> {}

export function LocationSearch({ className, ...other }: LocationSearchProps) {
  const { setCenter } = useStationContext();
  const [query, setQuery] = useState<string>();
  const [selectedId, setSelectedId] = useState<number>(-1);

  const { data, isLoading } = useSearchLocation({
    query: query,
  });

  const onSubmit = (values: LocationRequest) => {
    setQuery(values.query);
  };

  const onCancel = () => {
    setQuery("");
  };

  const onClick = (values: LocationResponse) => {
    setSelectedId(values.place_id);
    setCenter([values.lat, values.lon]);
  };

  return (
    <div
      className={cn(
        "w-full flex flex-col",
        { "bg-white h-full dark:bg-gray-900": !!data },
        className
      )}
      {...other}
    >
      <div className="p-2.5">
        <LocationSearchForm
          data={{ query: query }}
          isLoading={isLoading}
          onSubmit={onSubmit}
        />
      </div>
      {query && data && (
        <>
          {
            <div className="py-2.5 overflow-auto h-full">
              {data.length > 0 ? (
                data?.map((item) => (
                  <div
                    key={item.place_id}
                    className={cn(
                      "px-5 py-2.5 border-b last:border-0 hover:bg-muted hover:cursor-pointer"
                    )}
                    onClick={() => onClick(item)}
                  >
                    <p
                      className={cn("mb-1", {
                        "font-semibold": item.place_id === selectedId,
                      })}
                    >
                      {item.display_name}
                    </p>
                    <p className="text-sm text-muted-foreground">
                      {item.category}
                    </p>
                  </div>
                ))
              ) : (
                <div className="px-5">
                  <p className="font-medium">Maps can't find {query}</p>
                  <p className="text-sm text-muted-foreground">
                    Make sure your search is spelled correctly. Try adding a
                    city, state, or zip code
                  </p>
                </div>
              )}
            </div>
          }
          <div className="p-2.5">
            <Button type="button" className="w-full" onClick={onCancel}>
              Close
            </Button>
          </div>
        </>
      )}
    </div>
  );
}
