import { useSession } from "@/context/SessionProvider";
import { DarkModeToggle } from "@/shared/components/DarkModeToggle";
import FullPageWrapper from "@/shared/components/FullPageWrapper";
import { LanguageToggle } from "@/shared/components/LanguageToggle";
import { LogoContainer } from "@/shared/components/LogoContainer";
import { Button } from "@/shared/components/ui/button";
import { FeedbackDialog } from "@/widgets/feedback";
import { LocationSearch } from "@/widgets/location";
import { UserProfileMenu } from "@/widgets/user";
import { MessageSquareText } from "lucide-react";
import { Outlet } from "react-router-dom";

export default function MainLayout() {
  const { session, isLoading } = useSession();

  return (
    <FullPageWrapper>
      <div className="absolute z-10 h-screen bg-white dark:bg-gray-900 w-16 border-r border-border flex items-center flex-col py-2.5 gap-y-2.5">
        <DarkModeToggle />
        <FeedbackDialog
          trigger={
            <Button size="icon" variant="secondary">
              <MessageSquareText />
            </Button>
          }
        />
        <LanguageToggle />
      </div>
      <LocationSearch className="absolute z-10 top-0 left-16 w-full flex max-w-sm" />
      <div className="absolute z-10 top-0 right-0 p-2.5">
        {isLoading ? (
          <p>Loading...</p>
        ) : session ? (
          <UserProfileMenu />
        ) : (
          <div className="space-x-2">
            <Button size="sm" asChild>
              <a href="/auth/sign-in">Sign in</a>
            </Button>
          </div>
        )}
      </div>
      <LogoContainer className="absolute z-10 bottom-8 right-14" />
      <Outlet />
    </FullPageWrapper>
  );
}
