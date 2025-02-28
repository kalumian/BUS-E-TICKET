import { apiRespone } from 'src/Interfaces/apiInterfaces';
import api from 'src/Services/apiService';

export const getDashboardStats = async (token : string): Promise<apiRespone> => {
  try {
    const response = await api.get<apiRespone>('/Dashboard/stats', {headers:{Authorization: `Bearer ${token}`,}});
    return response.data;
  } catch (error : any) {
    return Promise.reject({ message: error.response?.data?.error || 'An unexpected error occurred.', statusCode: error.response?.status || 500 });
  }
};