import { VerifyEmailForm } from "@/entities/auth";
import { useVerifyEmail } from "@/features/auth/verify-email";
import { VerifyEmailRequest } from "@/lib/contracts/auth/verify-email.request";
import { CardContainer } from "@/shared/components/CardContainer";
import { FormAlert } from "@/shared/components/FormAlert";
import { useTranslation } from "react-i18next";

interface VerifyEmailCardProps {
  data?: VerifyEmailRequest;
}

export function VerifyEmailCard({ data }: VerifyEmailCardProps) {
  const { t } = useTranslation(["auth"]);
  const { mutate, isError, error, isSuccess, isPending } = useVerifyEmail();

  const onSubmit = (data: VerifyEmailRequest) => {
    mutate(data);
  };

  return (
    <CardContainer className="w-full max-w-md p-8">
      <h1 className="text-center mb-10 text-3xl font-semibold">
        {t("verifyEmailTitle")}
      </h1>
      <FormAlert
        className="mb-3"
        isSuccess={isSuccess}
        success={{
          title: t("verifyEmailSuccessTitle"),
          message: (
            <>
              {t("verifyEmailSuccessSubtitle")}{" "}
              <a
                className="underline font-semibold underline-offset-2"
                href="/auth/sign-in"
              >
                {t("signInBtn")}
              </a>
              .
            </>
          ),
        }}
        isError={isError}
        error={error?.response?.data}
      />
      <VerifyEmailForm data={data} isLoading={isPending} onSubmit={onSubmit} />
    </CardContainer>
  );
}
