import { User } from "src/Interfaces/userInterfaces";
import {jwtDecode} from "jwt-decode"
import Cookies from "js-cookie"
import EnUserRole from "src/Enums/EnUserRole";
export const decodeTokenIntoUser = (token: string): User | null => {
    try {
        const decoded: any = jwtDecode(token);
        return {
        UserID: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
        Username: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
        Role: EnUserRole[decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] as keyof typeof EnUserRole],
        };
    } catch (error) {
        console.error("Invalid token:", error);
        return null;
    }
};

export const saveToken = (token: string) : User | null => {
    try {
        const decoded: any = jwtDecode(token);
        Cookies.set("token", token, { expires: ExtractTokenAge(decoded), secure: true, sameSite: "strict", path: "/" });
        return decodeTokenIntoUser(token);
    } catch (error) {
        console.error("Failed to save token:", error);
        return null;
    }
};
  const ExtractTokenAge = (decodedToken : any) : number | Date | undefined => {
    const expTime = decodedToken.exp * 1000;
    const expiresInDays = (expTime - Date.now()) / (1000 * 60 * 60 * 24);
    return expiresInDays;
  }