import { ThemeToggle } from "@/shared/components/ThemeToggle";
import FullPageWrapper from "@/shared/components/FullPageWrapper";
import { Outlet } from "react-router-dom";

export default function AuthLayout() {
  return (
    <FullPageWrapper className="flex items-center justify-center py-14 px-5 bg-gray-50 dark:bg-gray-950">
      <ThemeToggle variant="ghost" className="absolute top-5 right-5" />
      <Outlet />
    </FullPageWrapper>
  );
}
