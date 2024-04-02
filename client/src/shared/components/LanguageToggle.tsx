import i18next from "i18next";
import { Button } from "./ui/button";
import { useStationContext } from "@/context/StationContext";

export const LanguageToggle = () => {
  const { language, setLanguage } = useStationContext();

  const handleLanguageChange = () => {
    const newLanguage = language === "en" ? "fi" : "en";
    setLanguage(newLanguage);
    i18next.changeLanguage(newLanguage);
  };

  return (
    <Button variant="secondary" size="icon" onClick={handleLanguageChange}>
      {language?.toUpperCase()}
    </Button>
  );
};
