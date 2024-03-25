import { useState, useEffect } from "react";
import { useMediaQuery } from "react-responsive";
import { Button } from "./ui/button";
import { Moon, Sun } from "lucide-react";

export const DarkModeToggle = () => {
  const [isDark, setIsDark] = useState(false);

  // Adds/removes css class "dark" from body element when isDark updates
  useEffect(() => {
    if (isDark) {
      document.body.classList.add("dark");
    } else {
      document.body.classList.remove("dark");
    }
  }, [isDark]);

  // Checks users system color scheme preference
  useMediaQuery(
    {
      query: "(prefers-color-scheme: dark)",
    },
    undefined,
    (isSystemDark) => setIsDark(isSystemDark)
  );

  return (
    <Button size="icon" variant="secondary" onClick={() => setIsDark(!isDark)}>
      {isDark ? <Moon /> : <Sun />}
    </Button>
  );
};
