import * as React from "react";
import { LatLngExpression } from "leaflet";
import { Dispatch, SetStateAction, useState } from "react";
import { updateTopics } from "@/lib/constants";
import moment from "moment";
import { useNotificationSubWebSocket } from "@/features/notification/sub";
import { Station } from "@/lib/contracts/station/station";
import i18next from "i18next";

export interface AppContext {
  center: LatLngExpression | null;
  setCenter: Dispatch<SetStateAction<LatLngExpression | null>>;
  roadworksUpdatedAt?: Date;
  sensorsUpdatedAt?: Date;
  stationsUpdatedAt?: Date;
  selectedStation: Station | null;
  setSelectedStation: Dispatch<SetStateAction<Station | null>>;
  language: string;
  setLanguage: Dispatch<SetStateAction<string>>;
}

type Props = {
  children: React.ReactNode;
};

const defaultValue: AppContext = {
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
  const [language, setLanguage] = useState(i18next.language);

  // data updation date time
  const { lastMessage } = useNotificationSubWebSocket();

  const [stationsUpdatedAt, setStationsUpdateAt] = useState<Date>();
  const [roadworksUpdatedAt, setRoadworksUpdateAt] = useState<Date>();
  const [sensorsUpdatedAt, setSensorsUpdateAt] = useState<Date>();

  // selected station
  const [selectedStation, setSelectedStation] = useState<Station | null>(null);

  // map center
  const [center, setCenter] = React.useState<LatLngExpression | null>(null);

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
