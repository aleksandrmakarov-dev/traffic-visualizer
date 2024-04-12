import MapProvider from "@/context/MapProvider";
import { useSession } from "@/context/SessionProvider";
import SidebarProvider from "@/context/SidebarProvider";
import StationProvider from "@/context/StationProvider";
import FullPageWrapper from "@/shared/components/FullPageWrapper";
import { LanguageToggle } from "@/shared/components/LanguageToggle";
import { LogoContainer } from "@/shared/components/LogoContainer";
import {
  Sidebar,
  SidebarContainer,
  SidebarContent,
  SidebarHeader,
  SidebarNavigation,
  SidebarNavigationItem,
} from "@/shared/components/Sidebar";
import { ThemeToggle } from "@/shared/components/ThemeToggle";
import { Button } from "@/shared/components/ui/button";
import { FeedbackDialog } from "@/widgets/feedback";
import { LocationSearch, LocationSearchList } from "@/widgets/location";
import { FavoriteStationList, StationDetails } from "@/widgets/station";
import { UserProfileMenu } from "@/widgets/user";
import { MessageSquareText, Star } from "lucide-react";
import { useTranslation } from "react-i18next";
import { Outlet } from "react-router-dom";

export default function MainLayout() {
  const { session, isLoading } = useSession();
  useTranslation(["tooltip", "roadworks", "sensors", "units", "modal"]);

  return (
    <SidebarProvider>
      <StationProvider>
        <MapProvider>
          <FullPageWrapper>
            {/* <div className="absolute z-10 h-screen bg-white shadow-md dark:bg-gray-950 w-16 border-r border-border flex items-center flex-col py-2.5 gap-y-2.5">
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
            <LocationSearch className="absolute z-20 top-0 left-16 w-full flex max-w-sm" />
            <div className="absolute z-10 top-0 left-16 w-full max-w-sm shadow-md">
              <StationDetails />
            </div> */}
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
            <Sidebar className="absolute z-10">
              <SidebarNavigation>
                <ThemeToggle />
                <FeedbackDialog
                  trigger={
                    <Button size="icon" variant="secondary">
                      <MessageSquareText />
                    </Button>
                  }
                />
                <LanguageToggle />
                {session && (
                  <SidebarNavigationItem value="favorite">
                    <Star />
                  </SidebarNavigationItem>
                )}
              </SidebarNavigation>
              <SidebarContainer>
                <SidebarHeader>
                  <LocationSearch />
                </SidebarHeader>
                <SidebarContent value="favorite">
                  <FavoriteStationList />
                </SidebarContent>
                <SidebarContent value="select">
                  <StationDetails />
                </SidebarContent>
                <SidebarContent value="search">
                  <LocationSearchList />
                </SidebarContent>
              </SidebarContainer>
            </Sidebar>
            <LogoContainer className="absolute z-10 bottom-8 right-14" />
            <Outlet />
          </FullPageWrapper>
        </MapProvider>
      </StationProvider>
    </SidebarProvider>
  );
}
