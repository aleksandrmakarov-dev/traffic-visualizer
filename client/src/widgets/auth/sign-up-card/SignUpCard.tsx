import { SignUpForm } from "@/entities/auth";
import { useSignUpLocal } from "@/features/auth/sign-up";
import { SignUpRequest } from "@/lib/contracts/auth/sign-up.request";
import { CardContainer } from "@/shared/components/CardContainer";
import { FormAlert } from "@/shared/components/FormAlert";
import { useTranslation } from "react-i18next";

export function SignUpCard() {
  const { t } = useTranslation(["auth"]);
  const { mutate, isError, error, isSuccess, isPending } = useSignUpLocal();

  const onSubmit = (data: SignUpRequest) => {
    mutate(data);
  };

  return (
    <CardContainer className="w-full max-w-md mx-auto p-8">
      <h1 className="text-center mb-10 text-3xl font-semibold">
        {t("signUpTitle")}
      </h1>
      <FormAlert
        className="mb-3"
        isSuccess={isSuccess}
        success={{
          title: "User created",
          message: (
            <>
              You have been sent verification link to your email. You can enter
              token manually at{" "}
              <a
                className="underline font-semibold underline-offset-2"
                href="/auth/verify-email"
              >
                Verify email
              </a>
              .
            </>
          ),
        }}
        isError={isError}
        error={{
          title: error?.response?.data.error,
          message: error?.response?.data.message,
        }}
      />
      <SignUpForm isLoading={isPending} onSubmit={onSubmit} />
      <div className="text-center mt-5">
        <p>
          {t("signUpSuggestion")}{" "}
          <a
            href="/auth/sign-in"
            className="font-semibold underline underline-offset-2"
          >
            {t("signInBtn")}
          </a>
        </p>
      </div>
    </CardContainer>
  );
}
