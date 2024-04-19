import { cn } from "@/lib/utils";
import { Button } from "./ui/button";
import { useThemeContext } from "@/context/ThemeProvider";

interface LanguageToggleProps {
  className?: string;
  variant?:
    | "default"
    | "destructive"
    | "outline"
    | "secondary"
    | "ghost"
    | "link"
    | null
    | undefined;
}

export const LanguageToggle = ({ variant, className }: LanguageToggleProps) => {
  const { language, setLanguage } = useThemeContext();

  const handleLanguageChange = () => {
    const newLanguage = language === "en" ? "fi" : "en";
    setLanguage(newLanguage);
  };

  return (
    <Button
      className={cn("font-medium", className)}
      variant={variant || "secondary"}
      size="icon"
      onClick={handleLanguageChange}
    >
      {language?.toUpperCase()}
    </Button>
  );
};
