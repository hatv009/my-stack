import { z } from "zod";

const serverEnvSchema = z.object({
  API_URL: z.url(),
});

export const serverEnv = serverEnvSchema.parse({
  API_URL: process.env.API_URL,
});