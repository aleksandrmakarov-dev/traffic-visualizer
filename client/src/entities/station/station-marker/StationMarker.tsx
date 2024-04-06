import { Station } from "@/lib/contracts/station/station";
import { StationResponse } from "@/lib/contracts/station/station.response";
import { CreateIconParams, createIcon } from "@/lib/iconMaker";
import { mapValueToColor } from "@/lib/utils";
import { Marker, Popup } from "react-leaflet";

interface StationMarkerProps {
  station: Station;
  isSelected?: boolean;
}

export function StationMarker({ station, isSelected }: StationMarkerProps) {
  // const { language, selectedStation, setSelectedStation } = useStationContext();

  const selectionColor = "#3867d6";

  const getColorParams = (): CreateIconParams => {
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
    // if (selectedStation?.id === station.id) return;
    // setSelectedStation(station);
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
        <p>{station.names["en" as keyof StationResponse["names"]]}</p>
      </Popup>
    </Marker>
  );
}
