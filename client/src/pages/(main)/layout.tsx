import { useSession } from "@/context/SessionProvider";
import FullPageWrapper from "@/shared/components/FullPageWrapper";
import { Button } from "@/shared/components/ui/button";
import { FeedbackDialog } from "@/widgets/feedback";
import { LocationSearch } from "@/widgets/location";
import { UserProfileMenu } from "@/widgets/user";
import { Languages, MessageSquareText, Moon } from "lucide-react";
import { Outlet } from "react-router-dom";

export default function MainLayout() {
  const { session, isLoading } = useSession();

  return (
    <FullPageWrapper className="bg-gray-50">
      <div className="absolute z-10 h-screen bg-white w-16 border-r border-border flex items-center flex-col py-2.5 gap-y-2.5">
        <Button size="icon" variant="secondary">
          <Moon />
        </Button>
        <FeedbackDialog
          trigger={
            <Button size="icon" variant="secondary">
              <MessageSquareText />
            </Button>
          }
        />
        <Button size="icon" variant="secondary">
          <Languages />
        </Button>
      </div>
      <LocationSearch className="absolute z-[4] top-0 left-16 w-full flex max-w-sm" />
      <div className="absolute z-[4] top-0 right-0 p-2.5">
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
      <Outlet />
    </FullPageWrapper>
  );
}
