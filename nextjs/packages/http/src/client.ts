import { HttpError } from "./error";
import type {
    HttpClientOptions,
    HttpRequestOptions,
} from "./types";

export function createHttpClient(
  options: HttpClientOptions = {},
) {
  const {
    baseUrl = "",
    headers: defaultHeaders,
  } = options;

  async function request<T>(
    path: string,
    requestOptions: HttpRequestOptions = {},
  ): Promise<T> {
    const {
      body,
      headers,
      method = "GET",
      ...fetchOptions
    } = requestOptions;

    const response = await fetch(`${baseUrl}${path}`, {
      ...fetchOptions,
      method,
      headers: {
        ...defaultHeaders,
        ...headers,
        ...(body !== undefined
          ? { "Content-Type": "application/json" }
          : {}),
      },
      body:
        body === undefined
          ? undefined
          : JSON.stringify(body),
    });

    const data = await parseResponse(response);

    if (!response.ok) {
      throw new HttpError(
        response.status,
        response.statusText,
        data,
      );
    }

    return data as T;
  }

  return {
    request,

    get<T>(
      path: string,
      options?: Omit<HttpRequestOptions, "method" | "body">,
    ) {
      return request<T>(path, {
        ...options,
        method: "GET",
      });
    },

    post<T>(
      path: string,
      body?: unknown,
      options?: Omit<HttpRequestOptions, "method" | "body">,
    ) {
      return request<T>(path, {
        ...options,
        method: "POST",
        body,
      });
    },

    put<T>(
      path: string,
      body?: unknown,
      options?: Omit<HttpRequestOptions, "method" | "body">,
    ) {
      return request<T>(path, {
        ...options,
        method: "PUT",
        body,
      });
    },

    patch<T>(
      path: string,
      body?: unknown,
      options?: Omit<HttpRequestOptions, "method" | "body">,
    ) {
      return request<T>(path, {
        ...options,
        method: "PATCH",
        body,
      });
    },

    delete<T>(
      path: string,
      options?: Omit<HttpRequestOptions, "method" | "body">,
    ) {
      return request<T>(path, {
        ...options,
        method: "DELETE",
      });
    },
  };
}

async function parseResponse(
  response: Response,
): Promise<unknown> {
  if (response.status === 204) {
    return undefined;
  }

  const contentType =
    response.headers.get("content-type");

  if (contentType?.includes("application/json")) {
    return response.json();
  }

  return response.text();
}