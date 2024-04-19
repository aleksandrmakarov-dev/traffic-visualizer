import { useSession } from "@/context/SessionProvider";
import { SignInForm } from "@/entities/auth";
import { useSignInLocal } from "@/features/auth/sign-in";
import { SignInRequest } from "@/lib/contracts/auth/sign-in.request";
import { CardContainer } from "@/shared/components/CardContainer";
import { FormAlert } from "@/shared/components/FormAlert";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

export function SignInCard() {
  const { t } = useTranslation(["auth"]);
  const { mutate, isError, error, isSuccess, isPending } = useSignInLocal();
  const { setSession } = useSession();

  const navigate = useNavigate();

  const onSubmit = (data: SignInRequest) => {
    mutate(data, {
      onSuccess: (response) => {
        setSession(response);

        navigate("/", { replace: true });
      },
    });
  };

  return (
    <CardContainer className="w-full max-w-md mx-auto p-8">
      <h1 className="text-center mb-10 text-3xl font-semibold">
        {t("signInTitle")}
      </h1>
      <FormAlert
        className="mb-3"
        isSuccess={isSuccess}
        isError={isError}
        error={{
          title: error?.response?.data.error,
          message:
            error?.response?.data.statusCode === 422 ? (
              <>
                {error?.response?.data.message}{" "}
                <a
                  className="font-medium underline underline-offset-2"
                  href="/auth/new-email-verification"
                >
                  {t("resendBtn")}
                </a>
              </>
            ) : (
              error?.response?.data.message
            ),
        }}
      />
      <SignInForm isLoading={isPending} onSubmit={onSubmit} />
      <div className="text-center mt-5">
        <p>
          {t("signInSuggestion")}{" "}
          <a
            href="/auth/sign-up"
            className="font-semibold underline underline-offset-2"
          >
            {t("signUpBtn")}
          </a>
        </p>
      </div>
    </CardContainer>
  );
}
