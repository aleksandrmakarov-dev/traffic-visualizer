import { z } from "zod";

export const updateMessage = z.object({
  topic:z.string(),
  payload:z.date()
})

export type UpdateMessage = z.infer<typeof updateMessage>;