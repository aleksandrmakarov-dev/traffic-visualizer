import { useStationContext } from "@/context/StationContext";
import { Station } from "@/lib/contracts/station/station";
import L from "leaflet";
import { Marker, Popup } from "react-leaflet";

const mapValueToColor = (value: number) => {
  const min = -1;
  const max = 100;

  // Clamp traffic percentage between 0 and 100
  const clampedPercentage = Math.min(Math.max(value, min), max);

  let color: string;

  if (value == -1) {
    return "#95a5a6";
  }

  if (clampedPercentage >= 90) {
    // dark green
    color = "#16a085";
  } else if (clampedPercentage >= 80) {
    // green
    color = "#2ecc71";
  } else if (clampedPercentage >= 70) {
    // yellow
    color = "#f1c40f";
  } else if (clampedPercentage >= 60) {
    // orange
    color = "#f39c12";
  } else if (clampedPercentage >= 50) {
    // dark orange
    color = "#d35400";
  } else {
    // red
    color = "#c0392b";
  }

  return color;
};

const selectionColor = "#3867d6";

const icon = (color1: string, color2: string, shadowOpacity: number = 0.25) => `
<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" version="1.1" width="48" height="48" viewBox="0 0 256 256" xml:space="preserve">
<g style="stroke: none; stroke-width: 0; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: none; fill-rule: nonzero; opacity: 1;" transform="translate(1.4065934065934016 1.4065934065934016) scale(2.81 2.81)" >
	<path d="M 48.647 69.718 c 13.692 0.652 24.265 4.924 24.265 10.098 C 72.912 85.44 60.415 90 45 90 s -27.912 -4.56 -27.912 -10.184 c 0 -5.173 10.573 -9.446 24.265 -10.098" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: #353b48; fill-rule: nonzero; opacity: ${shadowOpacity};" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
	<path d="M 45 79.665 l 21.792 -6.211 c -3.033 -1.381 -7.032 -2.466 -11.622 -3.122 L 45 79.665 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: #2f3640; fill-rule: nonzero; opacity: ${shadowOpacity};" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
	<path d="M 45 0 C 30.802 0 19.291 11.51 19.291 25.709 c 0 20.07 21.265 33.961 25.709 53.956 C 48.304 53.11 48.304 26.555 45 0 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: ${color1}; fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
	<path d="M 45 14.965 c -6.011 0 -10.885 4.873 -10.885 10.885 S 38.989 36.735 45 36.735 C 47.897 29.478 47.897 22.222 45 14.965 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(255,255,255); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
	<path d="M 45 0 c 14.198 0 25.709 11.51 25.709 25.709 c 0 20.07 -21.265 33.961 -25.709 53.956 V 0 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: ${color2}; fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
	<path d="M 45 14.965 c 6.011 0 10.885 4.873 10.885 10.885 S 51.011 36.735 45 36.735 V 14.965 z" style="stroke: none; stroke-width: 1; stroke-dasharray: none; stroke-linecap: butt; stroke-linejoin: miter; stroke-miterlimit: 10; fill: rgb(255,255,255); fill-rule: nonzero; opacity: 1;" transform=" matrix(1 0 0 1 0 0) " stroke-linecap="round" />
</g>
</svg>`;

const createIcon = (color1: string, color2: string) => {
  return L.divIcon({
    html: icon(color1, color2),
    className: "customMarker",
    iconSize: [48, 48],
    iconAnchor: [24, 48],
    popupAnchor: [0, -36],
  });
};

interface StationMarkerProps {
  station: Station;
  isSelected?: boolean;
}

export function StationMarker({ station, isSelected }: StationMarkerProps) {
  const { setSelectedStation } = useStationContext();

  return (
    <Marker
      eventHandlers={{
        click: () => setSelectedStation(station),
      }}
      position={[station.coordinates.latitude, station.coordinates.longitude]}
      icon={createIcon(
        isSelected
          ? selectionColor
          : mapValueToColor(station.sensors?.[0]?.value ?? -1),
        isSelected
          ? selectionColor
          : mapValueToColor(station.sensors?.[1]?.value ?? -1)
      )}
    >
      <Popup>
        <p>{station.name.replaceAll("_", " ")}</p>
      </Popup>
    </Marker>
  );
}
