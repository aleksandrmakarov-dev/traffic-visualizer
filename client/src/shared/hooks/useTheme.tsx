import { Theme, changeTheme } from "@/store/appearence/appearenceSlice";
import { AppDispatch, useAppSelector } from "@/store/store";
import { useCallback, useEffect } from "react";
import { useDispatch } from "react-redux";

export const useTheme = () => {
  const key = "theme";
  const dispatch = useDispatch<AppDispatch>();
  const theme = useAppSelector((state) => state.appearence.theme);

  const setTheme = useCallback(
    (newTheme: Theme) => {
      dispatch(changeTheme(newTheme));
      localStorage.setItem(key, newTheme);

      if (newTheme === "dark") {
        document.body.classList.add("dark");
      } else {
        document.body.classList.remove("dark");
      }
    },
    [dispatch]
  );

  useEffect(() => {
    const savedTheme = localStorage.getItem(key) as Theme | null;

    const systemTheme = window.matchMedia("(prefers-color-scheme: dark)");

    const handleThemeChange = (e: MediaQueryListEvent) => {
      setTheme(e.matches ? "dark" : "light");
    };

    setTheme(savedTheme ?? (systemTheme.matches ? "dark" : "light"));

    systemTheme.addEventListener("change", handleThemeChange);

    return () => {
      systemTheme.removeEventListener("change", handleThemeChange);
    };
  }, []);

  return { theme, setTheme };
};
