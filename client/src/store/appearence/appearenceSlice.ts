import { PayloadAction, createSlice } from "@reduxjs/toolkit";

export type Theme = "light" | "dark";
export type Language = "en" | "fi";

interface AppearenceState {
  theme: Theme;
  language: Language;
}

const initialState: AppearenceState = {
  theme: "light",
  language: "en",
};

const appearenceSlice = createSlice({
  name: "appearence",
  initialState: initialState,
  reducers: {
    changeTheme: (state, action: PayloadAction<Theme>) => {
      state.theme = action.payload;
    },
    changeLanguage: (state, action: PayloadAction<Language>) => {
      state.language = action.payload;
    },
  },
});

export const { changeLanguage, changeTheme } = appearenceSlice.actions;

export default appearenceSlice.reducer;
