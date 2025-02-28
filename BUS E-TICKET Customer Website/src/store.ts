import { configureStore, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { AuthState } from "./Interfaces/authInterfaces";
import { UIState } from "./Interfaces/uiSetting";
import Cookies from "js-cookie";
import { decodeTokenIntoUser, saveToken } from "./Services/tokenService";



const token = Cookies.get("token") || null;
const user = token ? decodeTokenIntoUser(token) : null;

const initialUIState: UIState = {
  asideShow: false,
  sidebarShow: true,
  theme: "light",
  sidebarUnfoldable: false,
};

const uiSlice = createSlice({
  name: "ui",
  initialState: initialUIState,
  reducers: {
    setUIState: (state, action: PayloadAction<Partial<UIState>>) => {
      return { ...state, ...action.payload };
    },
  },
});

const initialAuthState: AuthState = {
  token,
  user,
};

const authSlice = createSlice({
  name: "auth",
  initialState: initialAuthState,
  reducers: {
    login: (state, action: PayloadAction<{ token: string }>) => {
      const token = action.payload.token;
      const decodedUser = saveToken(token);
      if (!decodedUser) {
        return; 
      }
      state.token = token;
      state.user = decodedUser;
    },
    logout: (state) => {
      state.token = null;
      state.user = null;
      Cookies.remove("token");
    },
  },
});

const store = configureStore({
  reducer: {
    ui: uiSlice.reducer,
    auth: authSlice.reducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const { setUIState } = uiSlice.actions;
export const { login, logout } = authSlice.actions;
export default store;
