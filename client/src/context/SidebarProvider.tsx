import {
  Dispatch,
  ReactNode,
  SetStateAction,
  createContext,
  useContext,
  useState,
} from "react";

interface SidebarContextState {
  key: string | null;
  setKey: Dispatch<SetStateAction<string | null>>;
}

const initialState: SidebarContextState = {
  key: null,
  setKey: () => {},
};

const SidebarContext = createContext<SidebarContextState>(initialState);

export default function SidebarProvider({ children }: { children: ReactNode }) {
  const [value, setValue] = useState<string | null>(null);

  return (
    <SidebarContext.Provider
      value={{
        key: value,
        setKey: setValue,
      }}
    >
      {children}
    </SidebarContext.Provider>
  );
}

export const useSidebarContext = () => {
  return useContext<SidebarContextState>(SidebarContext);
};
