import { useSidebarContext } from "@/context/SidebarProvider";
import { LocationSearchForm } from "@/entities/location";
import { LocationRequest } from "@/lib/contracts/location/location.request";
import { useSearchParams } from "react-router-dom";

export function LocationSearch() {
  const [searchParams, setSearchParams] = useSearchParams();
  const { setKey } = useSidebarContext();

  const onSubmit = (values: LocationRequest) => {
    const queryParams = new URLSearchParams(searchParams);
    queryParams.set("q", values.query);
    setSearchParams(queryParams);
    setKey("search");
  };

  return (
    <LocationSearchForm
      data={{ query: searchParams.get("q") ?? "" }}
      onSubmit={onSubmit}
    />
  );
}
