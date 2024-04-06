import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { LatLngExpression } from "leaflet";

interface MapState {
  center: LatLngExpression;
  zoom: number;
}

const initialState: MapState = {
  center: [0, 0],
  zoom: 12,
};

const mapSlice = createSlice({
  name: "map",
  initialState: initialState,
  reducers: {
    setCenter: (state, action: PayloadAction<LatLngExpression>) => {
      state.center = action.payload;
    },
    setZoom: (state, action: PayloadAction<number>) => {
      state.zoom = action.payload;
    },
  },
});

export const { setCenter, setZoom } = mapSlice.actions;

export default mapSlice.reducer;
