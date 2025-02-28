import { FetchMethod } from "src/types/apiTypes";

export interface apiRespone{
    statusCode: number,
    message?: string,
    data?: any,
    error?:string
}

export interface FetchOptions {
    method?: FetchMethod;
    body?: any;
    headers?: HeadersInit;
  }
  