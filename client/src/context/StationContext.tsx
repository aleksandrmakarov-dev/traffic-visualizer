import * as React from "react";
import { LatLngExpression, Marker } from "leaflet";
import { Dispatch, SetStateAction, useState } from "react";
import { updateTopics } from "@/lib/constants";
import moment from "moment";
import { useNotificationSubWebSocket } from "@/features/notification/sub";
import { Station } from "@/lib/contracts/station/station";

export interface AppContext {
  station: Station | null;
  updateStation: (station: Station | null) => void;
  marker: Marker | null;
  updateMarker: (marker: Marker | null) => void;
  stationError: boolean | null;
  updateError: (err: boolean | null) => void;
  center: LatLngExpression | null;
  setCenter: Dispatch<SetStateAction<LatLngExpression | null>>;
  roadworksUpdatedAt?: Date;
  sensorsUpdatedAt?: Date;
  stationsUpdatedAt?: Date;
}

type Props = {
  children: React.ReactNode;
};

const defaultValue = {
  marker: null,
  station: null,
  updateStation: () => {},
  updateMarker: () => {},
  stationError: null,
  updateError: () => {},
  center: null,
  setCenter: () => {},
};

export const StationContext = React.createContext<AppContext>(defaultValue);

const Provider: React.FC<Props> = ({
  children,
}: {
  children: React.ReactNode;
}): JSX.Element => {
  const { lastMessage } = useNotificationSubWebSocket();

  const [stationsUpdatedAt, setStationsUpdateAt] = useState<Date>();
  const [roadworksUpdatedAt, setRoadworksUpdateAt] = useState<Date>();
  const [sensorsUpdatedAt, setSensorsUpdateAt] = useState<Date>();

  const [station, setStation] = React.useState<Station | null>(null);
  const [marker, setMarker] = React.useState<Marker | null>(null);
  const [stationError, setError] = React.useState<boolean | null>(null);
  const [center, setCenter] = React.useState<LatLngExpression | null>(null);

  const updateStation = (station: Station | null) => setStation(station);
  const updateMarker = (marker: Marker | null) => setMarker(marker);
  const updateError = (err: boolean | null) => setError(err);

  React.useEffect(() => {
    if (!lastMessage) return;

    const { topic, payload } = lastMessage.data;

    if (topic === updateTopics.stationsUpdate) {
      setStationsUpdateAt(moment(payload).toDate());
    } else if (topic === updateTopics.sensorsUpdate) {
      setSensorsUpdateAt(moment(payload).toDate());
    } else if (topic === updateTopics.roadworksUpdate) {
      setRoadworksUpdateAt(moment(payload).toDate());
    }

    console.log(lastMessage);
  }, [lastMessage]);

  return (
    <StationContext.Provider
      value={{
        center,
        marker,
        setCenter,
        station,
        stationError,
        updateError,
        updateMarker,
        updateStation,
        roadworksUpdatedAt,
        sensorsUpdatedAt,
        stationsUpdatedAt,
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
