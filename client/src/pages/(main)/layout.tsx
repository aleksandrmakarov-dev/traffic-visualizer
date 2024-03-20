import { useSession } from "@/context/SessionProvider";
import FullPageWrapper from "@/shared/components/FullPageWrapper";
import { Button } from "@/shared/components/ui/button";
import { LocationSearch } from "@/widgets/location";
import { UserProfileMenu } from "@/widgets/user";
import { Outlet } from "react-router-dom";

export default function MainLayout() {
  const { session, isLoading } = useSession();

  return (
    <FullPageWrapper className="bg-gray-50">
      <LocationSearch className="absolute z-10 top-0 left-0" />
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
      <Outlet />
    </FullPageWrapper>
  );
}
