import { Button } from "./ui/button";
import { useThemeContext } from "@/context/ThemeProvider";

export const LanguageToggle = () => {
  const { language, setLanguage } = useThemeContext();

  const handleLanguageChange = () => {
    const newLanguage = language === "en" ? "fi" : "en";
    setLanguage(newLanguage);
  };

  return (
    <Button variant="secondary" size="icon" onClick={handleLanguageChange}>
      {language?.toUpperCase()}
    </Button>
  );
};
