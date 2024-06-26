import { useSession } from "@/context/SessionProvider";
import { MenuBase } from "@/shared/components/MenuBase";
import { Button } from "@/shared/components/ui/button";
import { CircleUserRound } from "lucide-react";
import { useTranslation } from "react-i18next";

export function UserProfileMenu() {
  const { t } = useTranslation(["auth"]);
  const { session } = useSession();

  if (!session) {
    return null;
  }

  return (
    <MenuBase
      trigger={
        <Button className="rounded-full" size="icon" variant="outline">
          <CircleUserRound />
        </Button>
      }
      label={session.email}
    >
      <a className="cursor-pointer" href={`/users/admin`}>
        Admin (Test)
      </a>
      <a className="cursor-pointer" href={`/users/default`}>
        User (Test)
      </a>
      <a className="cursor-pointer" href="/auth/sign-out">
        {t("signOutBtn")}
      </a>
    </MenuBase>
  );
}
