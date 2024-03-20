import AuthLayout from "@/pages/auth/layout";
import NewEmailVerificationPage from "@/pages/auth/new-email-verification/page";
import SignInPage from "@/pages/auth/sign-in/page";
import SignOutPage from "@/pages/auth/sign-out/page";
import SignUpPage from "@/pages/auth/sign-up/page";
import VerifyEmailPage from "@/pages/auth/verify-email/page";
import FullPageWrapper from "@/shared/components/FullPageWrapper";
import { RoutePublicGuard } from "@/shared/components/RoutePublicGuard";
import { RouteRoleGuard } from "@/shared/components/RouteRoleGuard";
import { Suspense } from "react";
import {
  createBrowserRouter,
  createRoutesFromElements,
  RouterProvider as ReactDOMRouterProvider,
  Route,
} from "react-router-dom";
import { ErrorBoundaryView } from "@/shared/components/ErrorBoundaryView";
import AdminPage from "@/pages/user/admin/page";
import { Role } from "@/lib/contracts/common/roles";
import MainLayout from "@/pages/(main)/layout";
import HomePage from "@/pages/(main)/home/page";
import AccessDeniedPage from "@/pages/access-denied/page";
import UserPage from "@/pages/user/default/page";

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route errorElement={<ErrorBoundaryView />}>
      <Route path="/" element={<MainLayout />}>
        <Route index element={<HomePage />} />
      </Route>
      <Route path="/users">
        <Route path="admin" element={<RouteRoleGuard roles={[Role.Admin]} />}>
          <Route index element={<AdminPage />} />
        </Route>
        <Route path="default" element={<RouteRoleGuard />}>
          <Route index element={<UserPage />} />
        </Route>
      </Route>
      <Route path="/auth" element={<AuthLayout />}>
        <Route element={<RoutePublicGuard />}>
          <Route path="sign-in" element={<SignInPage />} />
          <Route path="sign-up" element={<SignUpPage />} />
        </Route>
        <Route path="verify-email" element={<VerifyEmailPage />} />
        <Route
          path="new-email-verification"
          element={<NewEmailVerificationPage />}
        />
        <Route path="sign-out" element={<SignOutPage />} />
      </Route>
      <Route path="access-denied" element={<AccessDeniedPage />} />
    </Route>
  )
);

export default function RouterProvider() {
  return (
    <Suspense
      fallback={
        <FullPageWrapper className="flex justify-center items-center">
          <div>
            <h5 className="font-semibold text-lg">Please wait for a while</h5>
            <p className="text-gray-700">Loading page...</p>
          </div>
        </FullPageWrapper>
      }
    >
      <ReactDOMRouterProvider router={router} />
    </Suspense>
  );
}
