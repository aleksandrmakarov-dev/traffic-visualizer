import { configureStore } from "@reduxjs/toolkit";
import appearenceReducer from "@/store/appearence/appearenceSlice";
import mapReducer from "@/store/map/mapSlice";
import { TypedUseSelectorHook, useSelector } from "react-redux";

export const store = configureStore({
  reducer: {
    appearence: appearenceReducer,
    map: mapReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
