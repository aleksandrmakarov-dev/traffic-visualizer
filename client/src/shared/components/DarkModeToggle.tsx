import { Button } from "./ui/button";
import { Moon, Sun } from "lucide-react";
import { useTheme } from "@/shared/hooks/useTheme";

interface DarkModeToggleProps {
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

export const DarkModeToggle = ({ variant, className }: DarkModeToggleProps) => {
  const { theme, setTheme } = useTheme();

  const toggleTheme = () => {
    setTheme(theme === "dark" ? "light" : "dark");
  };

  return (
    <Button
      className={className}
      size="icon"
      variant={variant ?? "secondary"}
      onClick={toggleTheme}
    >
      {theme === "dark" ? <Moon /> : <Sun />}
    </Button>
  );
};
