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

export type Theme = "dark" | "light";
export type Language = "en" | "fi";

interface ThemeContextState {
  theme: Theme;
  setTheme: (newTheme: Theme) => void;
  language: Language;
  setLanguage: Dispatch<SetStateAction<Language>>;
}

const initialState: ThemeContextState = {
  theme: "light",
  setTheme: () => {},
  language: "en",
  setLanguage: () => {},
};

const ThemeContext = createContext<ThemeContextState>(initialState);

export default function ThemeProvider({ children }: { children: ReactNode }) {
  const key = "theme";

  const [theme, changeTheme] = useState<Theme>("light");
  const [language, setLanguage] = useState<Language>("en");

  const setTheme = useCallback(
    (newTheme: Theme) => {
      changeTheme(newTheme);
      localStorage.setItem(key, newTheme);

      if (newTheme === "dark") {
        document.body.classList.add("dark");
      } else {
        document.body.classList.remove("dark");
      }
    },
    [changeTheme]
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

  return (
    <ThemeContext.Provider
      value={{
        theme: theme,
        setTheme: setTheme,
        language: language,
        setLanguage: setLanguage,
      }}
    >
      {children}
    </ThemeContext.Provider>
  );
}

export const useThemeContext = () => {
  return useContext<ThemeContextState>(ThemeContext);
};
