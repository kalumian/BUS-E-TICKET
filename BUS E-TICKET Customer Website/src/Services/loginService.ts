import api, { apiRequest } from './apiService'
import {apiRespone} from '../Interfaces/apiInterfaces'
import { AppDispatch, login, logout } from 'src/store';


export const loginUser = async (username: string, password: string) : Promise<apiRespone> => 
  apiRequest("POST", "account/login", "", {username, password})

export const logoutUser = (dispatch: AppDispatch) => {
  try {
    dispatch(logout())
  }catch(error: any){
    console.log(error)
  }
}

