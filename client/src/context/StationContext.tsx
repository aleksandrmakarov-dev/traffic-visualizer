import { LatLngExpression } from "leaflet";
import { Dispatch, SetStateAction, useEffect, useState } from "react";
import { Station } from "@/lib/contracts/station/station";
import i18next from "i18next";
import { useSensors } from "@/entities/sensor";
import React from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";

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
  const [language, setLanguage] = useState(i18next.language);

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
  const [center, setCenter] = useState<LatLngExpression | null>(null);
  const [zoom, setZoom] = useState<number>(12);

  //connection

  useEffect(() => {
    const conn = new HubConnectionBuilder()
      .withUrl(`${import.meta.env.VITE_BACKEND_BASE_URL}/notifications`)
      .build();

    conn.on("ConnectionOpen", (msg) => {
      console.log("Connection:", msg);
    });

    conn.on("ClientJoined", (msg) => {
      console.log("ClientJoin:", msg);
    });

    conn.on("ClientDisconnected", (msg) => {
      console.log("ClientDisconnect", msg);
    });

    conn.on("Notification", (msg) => {
      console.log("Notification", msg);
    });

    conn.start().then(() => console.log("start"));

    return () => {
      conn.stop();
    };
  }, []);

  useEffect(() => {
    if (selectedStation) {
      const stationWithSensors: Station = {
        ...selectedStation,
        sensors: selectedStationSensors,
      };

      setSelectedStation(stationWithSensors);
    }
  }, [selectedStationSensors]);

  // useEffect(() => {
  //   if (!lastMessage) return;

  //   const { topic, payload } = lastMessage.data;

  //   if (topic === updateTopics.stationsUpdate) {
  //     setStationsUpdateAt(Number(payload));

  //     queryClient.invalidateQueries();
  //   } else if (topic === updateTopics.sensorsUpdate) {
  //     setSensorsUpdateAt(Number(payload));
  //   } else if (topic === updateTopics.roadworksUpdate) {
  //     setRoadworksUpdateAt(Number(payload));
  //   }

  //   console.log(lastMessage);
  // }, [lastMessage]);

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
