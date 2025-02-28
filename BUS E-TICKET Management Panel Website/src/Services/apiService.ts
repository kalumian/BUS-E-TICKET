import axios from "axios";
import Cookies from "js-cookie";
import { Dispatch, SetStateAction } from "react";
import { apiRespone } from "src/Interfaces/apiInterfaces";

export const API_BASE_URL = "https://localhost:7148/api";

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: { "Content-Type": "application/json" },
});

api.interceptors.request.use((config) => {
  const token = Cookies.get("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const apiRequest = async (
  method: "GET" | "POST" | "PUT" | "DELETE",
  url: string,
  token?: string,
  data?: any
): Promise<apiRespone> => {
  try {
    const headers = token ? { Authorization: `Bearer ${token}` } : {};
    const response = await api({ method, url, data, headers });

    return response.data;
  } catch (error: any) {
    return Promise.reject({
      message: error.response?.data?.error || "An unexpected error occurred.",
      statusCode: error.response?.status || 500,
    });
  }
};

export const fetchData = async (
  fetchFunction: () => Promise<any>,
  setData?: Dispatch<SetStateAction<any>>,
  setError?: Dispatch<SetStateAction<string>>,
  setLoading?: Dispatch<SetStateAction<boolean>>,
  { errorMessage, token }: { errorMessage?: string; token?: string } = {}
) => {
  try {
    setLoading?.(true);
    
    if (token !== undefined && !token) {
      throw new Error("401");
    }

    const data = await fetchFunction();
    setData?.(data?.data); 
    setError?.(''); 
  } catch (err: any) {
    setError?.(errorMessage || err.message || "An unexpected error occurred"); 
  } finally {
    setLoading?.(false);
  }
};
export default api;


  