import { z } from "zod";

export const locationRequest = z.object({
  query: z.string(),
});

export type LocationRequest = z.infer<typeof locationRequest>;
