"use client";

import { clientEnv } from "@my-stack/config/env/client";

export function ClientConfig() {
  return <div>{clientEnv.NEXT_PUBLIC_API_URL}</div>;
}