import { ThemeToggle } from "@/shared/components/ThemeToggle";
import FullPageWrapper from "@/shared/components/FullPageWrapper";
import { Outlet } from "react-router-dom";
import { LanguageToggle } from "@/shared/components/LanguageToggle";

export default function AuthLayout() {
  return (
    <FullPageWrapper className="flex items-center justify-center py-14 px-5 bg-gray-50 dark:bg-gray-950">
      <LanguageToggle variant="ghost" className="absolute top-5 left-5" />
      <ThemeToggle variant="ghost" className="absolute top-5 right-5" />
      <Outlet />
    </FullPageWrapper>
  );
}
