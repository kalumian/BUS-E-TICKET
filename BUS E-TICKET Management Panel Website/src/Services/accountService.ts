import exp from 'constants';
import { RegisterManagerAccountDTO } from 'src/Interfaces/accountInterfaces';
import { RegisterationApplication } from 'src/Interfaces/applicationInterface';
import { apiRequest } from 'src/Services/apiService';

export const GetManagers = async (token: string) => 
  apiRequest("GET", "/Manager", token);

export const GetManagerByID = async (token: string, id: string) => 
  apiRequest("GET", `/Manager/${id}`, token);

export const AddManager = async (token: string, RegisterManager : RegisterManagerAccountDTO) => 
  apiRequest("POST", `/Manager/Register`, token, RegisterManager);


export const UpdateManager = async (token: string, editAccount: RegisterManagerAccountDTO) =>
  apiRequest("PUT", `/Manager/Update/${editAccount.account.accountId}`, token, editAccount);

export const DeleteManager = async (token: string, Id: string) => 
  apiRequest("DELETE", `/Manager/${Id}`, token);



export const GetServiceProviders = async (token: string) => 
  apiRequest("GET", "/serviceprovider", token);

export const GetServiceProviderByID = async (token: string, id: string) => 
  apiRequest("GET", `/serviceprovider/${id}`, token);

export const CreateNewRegisterationApplication = async (application : RegisterationApplication) => 
  apiRequest("POST", `/serviceprovider/registration/applicate`,"",application);



export const GetAllCustomers = async (token: string) => 
  apiRequest("GET", `/customer`, token);

export const GetCustomerById = async (token: string, id : string) => 
  apiRequest("GET", `/customer/${id}`, token);


