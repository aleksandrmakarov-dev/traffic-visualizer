import { useMutation } from "@tanstack/react-query";
import { AxiosError } from "axios";
import axios from "@/lib/axios";
import { authKeys } from "@/entities/auth/api";
import { ErrorResponse } from "@/lib/contracts/common/error.response";
import { SignUpRequest } from "@/lib/contracts/auth/sign-up.request";
import { MessageResponse } from "@/lib/contracts/common/message.response";

async function signUpLocal(request: SignUpRequest) {
  const response = await axios.post<MessageResponse>("/auth/sign-up", request);
  return response.data;
}

export const useSignUpLocal = () => {
  return useMutation<
    MessageResponse,
    AxiosError<ErrorResponse>,
    SignUpRequest
  >({
    mutationKey: authKeys.mutations.signUpLocal(),
    mutationFn: async (data) => {
      return await signUpLocal(data);
    },
  });
};
