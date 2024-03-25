import { useStationContext } from "@/context/StationContext";
import { Station } from "@/lib/contracts/station/station";
import { Tooltip } from "@/shared/components/ui/tooltip";
import L from "leaflet";
import { Marker, Popup } from "react-leaflet";

const createIcon = (html: string) => {
  return L.divIcon({
    html: html,
    className: "customMarker",
    iconSize: [24, 40],
    iconAnchor: [20, 40],
  });
};

interface StationMarkerProps {
  station: Station;
  isSelected?: boolean;
}

export function StationMarker({ station }: StationMarkerProps) {
  const { setSelectedStation } = useStationContext();

  return (
    <Marker
      eventHandlers={{
        click: () => setSelectedStation(station),
      }}
      position={[station.coordinates.latitude, station.coordinates.longitude]}
    >
      <Popup>
        <p>{station.name}</p>
      </Popup>
    </Marker>
  );
}
