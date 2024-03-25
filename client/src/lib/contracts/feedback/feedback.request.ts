import { z } from "zod";

export const feedbackRequest = z.object({
  title: z.string().min(10),
  description: z.string().min(20),
});

export type FeedbackRequest = z.infer<typeof feedbackRequest>;
