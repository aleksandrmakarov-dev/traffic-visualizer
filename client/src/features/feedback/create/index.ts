import { feedbackKeys } from "@/entities/feedback";
import { ErrorResponse } from "@/lib/contracts/common/error.response";
import { MessageResponse } from "@/lib/contracts/common/message.response";
import { FeedbackRequest } from "@/lib/contracts/feedback/feedback.request";
import { useMutation } from "@tanstack/react-query";
import { AxiosError } from "axios";
import axios from "@/lib/axios";

async function createFeedback(request: FeedbackRequest) {
  const response = await axios.post<MessageResponse>("/feedback", request);

  return response.data;
}

export const useCreateFeedback = () => {
  return useMutation<
    MessageResponse,
    AxiosError<ErrorResponse>,
    FeedbackRequest
  >({
    mutationKey: feedbackKeys.mutations.create(),
    mutationFn: async (data) => await createFeedback(data),
  });
};
