import { User } from "./userInterfaces";

export interface AuthState {
    token: string | null;
    user: User | null;
  }
