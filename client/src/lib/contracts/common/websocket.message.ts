import { z } from "zod";

export const webSocketMessage = z.object({
  message_type: z.string(),
  data: z.object({
    topic:z.string(),
    payload:z.string()
  })
});

export type WebSocketMessage = z.infer<typeof webSocketMessage>;

