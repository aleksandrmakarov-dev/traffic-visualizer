import { z } from "zod";

export const locationRequest = z.object({
  query: z.string().min(1),
});

export type LocationRequest = z.infer<typeof locationRequest>;
