import { authKeys } from "@/entities/auth/api";
import { useMutation } from "@tanstack/react-query";
import { AxiosError } from "axios";
import axios from "@/lib/axios";
import { ErrorResponse } from "@/lib/contracts/common/error.response";
import { VerifyEmailRequest } from "@/lib/contracts/auth/verify-email.request";

async function verifyEmail(request: VerifyEmailRequest) {
  await axios.post("/auth/verify-email", request);
}

export const useVerifyEmail = () => {
  return useMutation<unknown, AxiosError<ErrorResponse>, VerifyEmailRequest>({
    mutationKey: authKeys.mutations.verifyEmail(),
    mutationFn: async (data) => {
      await verifyEmail(data);
    },
  });
};
