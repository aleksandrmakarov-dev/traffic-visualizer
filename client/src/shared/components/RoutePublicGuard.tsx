import { useSession } from "@/context/SessionProvider";
import { Navigate, Outlet } from "react-router-dom";

export function RoutePublicGuard() {
  const { session, isLoading } = useSession();

  if (isLoading) {
    return null;
  }

  return session ? <Navigate to="/" /> : <Outlet />;
}
