export type HttpMethod =
  | "GET"
  | "POST"
  | "PUT"
  | "PATCH"
  | "DELETE";

export interface HttpRequestOptions
  extends Omit<RequestInit, "method" | "body"> {
  method?: HttpMethod;
  body?: unknown;
}

export interface HttpClientOptions {
  baseUrl?: string;
  headers?: HeadersInit;
}