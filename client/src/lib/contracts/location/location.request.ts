import { z } from "zod";

export const locationRequest = z.object({
    query:z.string().optional()
})

export type LocationRequest = z.infer<typeof locationRequest>;