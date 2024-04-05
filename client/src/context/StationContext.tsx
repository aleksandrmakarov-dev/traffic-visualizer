import { LatLngExpression } from "leaflet";
import { Dispatch, SetStateAction, useState } from "react";
import { updateTopics } from "@/lib/constants";
import { useNotificationSubWebSocket } from "@/features/notification/sub";
import { Station } from "@/lib/contracts/station/station";
import i18next from "i18next";
import { useSensors } from "@/entities/sensor";
import React from "react";
import { useQueryClient } from "@tanstack/react-query";

export interface AppContext {
  zoom: number;
  setZoom: Dispatch<SetStateAction<number>>;
  center: LatLngExpression | null;
  setCenter: Dispatch<SetStateAction<LatLngExpression | null>>;
  roadworksUpdatedAt?: number;
  sensorsUpdatedAt?: number;
  stationsUpdatedAt?: number;
  selectedStation: Station | null;
  setSelectedStation: Dispatch<SetStateAction<Station | null>>;
  language: string;
  setLanguage: Dispatch<SetStateAction<string>>;
}

type Props = {
  children: React.ReactNode;
};

const defaultValue: AppContext = {
  zoom: 12,
  setZoom: () => {},
  center: null,
  setCenter: () => {},
  selectedStation: null,
  setSelectedStation: () => {},
  language: "en",
  setLanguage: () => {},
};

export const StationContext = React.createContext<AppContext>(defaultValue);

const Provider: React.FC<Props> = ({
  children,
}: {
  children: React.ReactNode;
}): JSX.Element => {
  // application language

  const queryClient = useQueryClient();

  const [language, setLanguage] = useState(i18next.language);

  // data updation date time
  const { lastMessage } = useNotificationSubWebSocket();
  const [stationsUpdatedAt, setStationsUpdateAt] = useState<number>();
  const [roadworksUpdatedAt, setRoadworksUpdateAt] = useState<number>();
  const [sensorsUpdatedAt, setSensorsUpdateAt] = useState<number>();

  // selected station
  const [selectedStation, setSelectedStation] = useState<Station | null>(null);

  const { data: selectedStationSensors } = useSensors(
    { stationId: selectedStation?.id },
    sensorsUpdatedAt,
    {
      enabled: !!selectedStation?.id,
    }
  );

  // map center
  const [center, setCenter] = React.useState<LatLngExpression | null>(null);
  const [zoom, setZoom] = React.useState<number>(12);

  React.useEffect(() => {
    if (selectedStation) {
      const stationWithSensors: Station = {
        ...selectedStation,
        sensors: selectedStationSensors,
      };

      setSelectedStation(stationWithSensors);
    }
  }, [selectedStationSensors]);

  React.useEffect(() => {
    if (!lastMessage) return;

    const { topic, payload } = lastMessage.data;

    if (topic === updateTopics.stationsUpdate) {
      setStationsUpdateAt(Number(payload));

      queryClient.invalidateQueries();
    } else if (topic === updateTopics.sensorsUpdate) {
      setSensorsUpdateAt(Number(payload));
    } else if (topic === updateTopics.roadworksUpdate) {
      setRoadworksUpdateAt(Number(payload));
    }

    console.log(lastMessage);
  }, [lastMessage]);

  return (
    <StationContext.Provider
      value={{
        zoom,
        setZoom,
        center,
        setCenter,
        roadworksUpdatedAt,
        sensorsUpdatedAt,
        stationsUpdatedAt,
        selectedStation,
        setSelectedStation,
        language,
        setLanguage,
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
