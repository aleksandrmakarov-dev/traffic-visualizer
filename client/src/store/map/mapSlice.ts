import { PayloadAction, createSlice } from "@reduxjs/toolkit";
import { LatLngExpression } from "leaflet";

interface MapState {
  center: LatLngExpression;
  zoom: number;
}

const initialState: MapState = {
  center: [60.2, 24.9],
  zoom: 12,
};

const mapSlice = createSlice({
  name: "map",
  initialState: initialState,
  reducers: {
    changeCenter: (state, action: PayloadAction<LatLngExpression>) => {
      state.center = action.payload;
    },
    changeZoom: (state, action: PayloadAction<number>) => {
      state.zoom = action.payload;
    },
  },
});

export const { changeCenter, changeZoom } = mapSlice.actions;

export default mapSlice.reducer;
