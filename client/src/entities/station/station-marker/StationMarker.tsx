import { useSidebarContext } from "@/context/SidebarProvider";
import { useStationContext } from "@/context/StationProvider";
import { useThemeContext } from "@/context/ThemeProvider";
import { Station } from "@/lib/contracts/station/station";
import { StationResponse } from "@/lib/contracts/station/station.response";
import { CreateIconParams, createIcon } from "@/lib/iconMaker";
import { mapValueToColor } from "@/lib/utils";
import { Marker, Popup } from "react-leaflet";

interface StationMarkerProps {
  station: Station;
}

export function StationMarker({ station }: StationMarkerProps) {
  const { selected, setSelected, mode, setMode, setComparator } =
    useStationContext();
  const { setKey } = useSidebarContext();
  const { language } = useThemeContext();

  if (station.sensors?.length === 0) {
    return null;
  }

  const selectionColor = "#3867d6";

  const getColorParams = (): CreateIconParams => {
    const isSelected = selected?.id === station.id;

    return {
      leftColor: isSelected
        ? selectionColor
        : mapValueToColor(station.sensors?.[0]?.value ?? -1),
      rightColor: isSelected
        ? selectionColor
        : mapValueToColor(station.sensors?.[1]?.value ?? -1),
      isRoadwork: (station.roadworks && station.roadworks?.length > 0) ?? false,
    };
  };

  const onStationSelect = () => {
    if (selected?.id === station.id) return;

    if (mode === "select") {
      setSelected(station);
      setKey("select");
    } else {
      setComparator(station);
      setMode("select");
    }
  };

  return (
    <Marker
      eventHandlers={{
        click: onStationSelect,
      }}
      position={[station.coordinates.latitude, station.coordinates.longitude]}
      icon={createIcon(getColorParams())}
    >
      <Popup>
        <p>{station.names[language as keyof StationResponse["names"]]}</p>
      </Popup>
    </Marker>
  );
}
