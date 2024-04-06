import {
  Dispatch,
  ReactNode,
  SetStateAction,
  createContext,
  useContext,
  useState,
} from "react";

export type MapMode = "default" | "compare";

interface MapContextState {
  mode: MapMode;
  setMode: Dispatch<SetStateAction<MapMode>>;
  selectedId?: string | null;
  setSelectedId: Dispatch<SetStateAction<string | null>>;
  compareWithId?: string | null;
  setCompareWithId: Dispatch<SetStateAction<string | null>>;
}

const initialState: MapContextState = {
  mode: "default",
  setMode: () => {},
  selectedId: null,
  setSelectedId: () => {},
  compareWithId: null,
  setCompareWithId: () => {},
};

const MapContext = createContext<MapContextState>(initialState);

export default function MapProvider({ children }: { children: ReactNode }) {
  const [mode, setMode] = useState<MapMode>("default");
  const [selectedId, setSelectedId] = useState<string | null>(null);
  const [compareWithId, setCompareWithId] = useState<string | null>(null);

  return (
    <MapContext.Provider
      value={{
        mode: mode,
        setMode: setMode,
        selectedId: selectedId,
        setSelectedId: setSelectedId,
        compareWithId: compareWithId,
        setCompareWithId: setCompareWithId,
      }}
    >
      {children}
    </MapContext.Provider>
  );
}

export const useMapContext = () => {
  return useContext<MapContextState>(MapContext);
};
