import { LatLngExpression } from "leaflet";
import {
  Dispatch,
  ReactNode,
  SetStateAction,
  createContext,
  useContext,
  useState,
} from "react";

interface MapContextState {
  center: LatLngExpression;
  setCenter: Dispatch<SetStateAction<LatLngExpression>>;
  zoom: number;
  setZoom: Dispatch<SetStateAction<number>>;
}

const initialState: MapContextState = {
  center: [60.16, 24.93],
  setCenter: () => {},
  zoom: 12,
  setZoom: () => {},
};

const MapContext = createContext<MapContextState>(initialState);

export default function MapProvider({ children }: { children: ReactNode }) {
  const [center, setCenter] = useState<LatLngExpression>([60.16, 24.93]);
  const [zoom, setZoom] = useState<number>(12);

  return (
    <MapContext.Provider
      value={{
        center: center,
        setCenter: setCenter,
        zoom: zoom,
        setZoom: setZoom,
      }}
    >
      {children}
    </MapContext.Provider>
  );
}

export const useMapContext = () => {
  return useContext<MapContextState>(MapContext);
};
