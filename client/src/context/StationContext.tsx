import * as React from "react";
import { Station } from "../interfaces/Interfaces";
import { LatLngExpression, Marker } from "leaflet";
import { Dispatch, SetStateAction } from "react";

export interface AppContext {
  station: Station | null;
  updateStation: (station: Station | null) => void;
  marker: Marker | null;
  updateMarker: (marker: Marker | null) => void;
  stationError: boolean | null;
  updateError: (err: boolean | null) => void;
  center: LatLngExpression | null;
  setCenter: Dispatch<SetStateAction<LatLngExpression | null>>;
}

type Props = {
  children: React.ReactNode;
};

export const StationContext = React.createContext<AppContext>({
  marker: null,
  station: null,
  updateStation: function (_station: Station | null): void {
    throw new Error("Function not implemented.");
  },
  updateMarker: function (_marker: Marker<any> | null): void {
    throw new Error("Function not implemented.");
  },
  stationError: null,
  updateError: function (_err: boolean | null): void {
    throw new Error("Function not implemented.");
  },
  center: null,
  setCenter: function (
    _value: React.SetStateAction<LatLngExpression | null>
  ): void {
    throw new Error("Function not implemented.");
  },
});

const Provider: React.FC<Props> = ({
  children,
}: {
  children: React.ReactNode;
}): JSX.Element => {
  const [station, setStation] = React.useState<Station | null>(null);
  const [marker, setMarker] = React.useState<Marker | null>(null);
  const [stationError, setError] = React.useState<boolean | null>(null);
  const [center, setCenter] = React.useState<LatLngExpression | null>(null);

  const updateStation = (station: Station | null) => setStation(station);
  const updateMarker = (marker: Marker | null) => setMarker(marker);
  const updateError = (err: boolean | null) => setError(err);

  return (
    <StationContext.Provider
      value={{
        station,
        updateStation,
        marker,
        updateMarker,
        stationError,
        updateError,
        center,
        setCenter,
      }}
    >
      {children}
    </StationContext.Provider>
  );
};

export const useStationContext = () => {
  return React.useContext<AppContext>(StationContext);
};

export default Provider;
