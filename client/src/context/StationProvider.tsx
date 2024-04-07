import { Station } from "@/lib/contracts/station/station";
import {
  Dispatch,
  ReactNode,
  SetStateAction,
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";
import { HubConnectionBuilder } from "@microsoft/signalr";
import { useFavoriteStations, useStations } from "@/entities/station";
import { useRoadworks } from "@/entities/roadwork";
import { useSensors } from "@/entities/sensor";
import _ from "lodash";
import { useSession } from "./SessionProvider";

interface StationContextState {
  stations: Station[];
  favoriteStations: string[] | null;
  selected: Station | null;
  setSelected: Dispatch<SetStateAction<Station | null>>;
  lastUpdate: Date | null;
}

const initialState: StationContextState = {
  stations: [],
  favoriteStations: null,
  selected: null,
  setSelected: () => {},
  lastUpdate: null,
};

const methods = {
  open: "ConnectionOpen",
  close: "ConnectionClose",
  msg: "Message",
};

const StationContext = createContext<StationContextState>(initialState);

export default function StationProvider({ children }: { children: ReactNode }) {
  const { session } = useSession();
  const {
    data: roadworkData,
    isLoading: isRoadworksLoading,
    refetch: refetchRoadworks,
  } = useRoadworks({
    enabled: false,
  });
  const {
    data: sensorData,
    isLoading: isSensorsLoading,
    refetch: refetchSensors,
  } = useSensors(
    {
      ids: ["5158", "5161"],
    },
    {
      enabled: false,
    }
  );
  const {
    data: stationData,
    isLoading: isStationsLoading,
    refetch: refetchStations,
  } = useStations({
    enabled: false,
  });

  const { data: favoriteStations } = useFavoriteStations({
    enabled: !!session,
  });

  const [stations, setStations] = useState<Station[]>([]);

  const [selected, setSelected] = useState<Station | null>(null);

  const [lastUpdate, setLastUpdate] = useState<Date | null>(null);

  const refetchData = useCallback(() => {
    refetchRoadworks();
    refetchSensors();
    refetchStations();
  }, [refetchRoadworks, refetchSensors, refetchStations]);

  useEffect(() => {
    const conn = new HubConnectionBuilder()
      .withUrl(`${import.meta.env.VITE_BACKEND_BASE_URL}/notifications`)
      .build();

    conn.on(methods.open, (msg) => {
      console.log(msg);
      refetchData();
    });

    conn.on(methods.msg, (msg) => {
      setLastUpdate(new Date(msg));
      console.log("refetching:", msg);
      refetchData();
    });

    conn.on(methods.close, (msg) => {
      console.log(msg);
    });

    conn.start().then(() => console.log("start"));

    return () => {
      conn.stop();
    };
  }, []);

  useEffect(() => {
    if (!isRoadworksLoading && !isSensorsLoading && !isStationsLoading) {
      const mappedStations: Station[] = _.map(
        stationData,
        (st): Station => ({
          ...st,
          sensors: _.filter(sensorData, (sensor) => sensor.stationId === st.id),
          roadworks: _.filter(roadworkData, (rw) => {
            return (
              (rw.primaryPointRoadNumber === st.roadNumber &&
                rw.primaryPointRoadSection === st.roadSection) ||
              (rw.secondaryPointRoadNumber === st.roadNumber &&
                rw.secondaryPointRoadSection === st.roadSection)
            );
          }),
        })
      );

      setStations(mappedStations);
    }
  }, [roadworkData, sensorData, stationData]);

  return (
    <StationContext.Provider
      value={{
        stations: stations,
        favoriteStations: favoriteStations || null,
        selected: selected,
        setSelected: setSelected,
        lastUpdate: lastUpdate,
      }}
    >
      {children}
    </StationContext.Provider>
  );
}

export const useStationContext = () => {
  return useContext<StationContextState>(StationContext);
};
