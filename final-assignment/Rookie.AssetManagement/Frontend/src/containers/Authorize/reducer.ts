import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { SetStatusType } from "src/constants/status";
import IAccount from "src/interfaces/IAccount";
import IChangePasswordUser from "src/interfaces/IChangePasswordUser";
import IError from "src/interfaces/IError";
import ILoginModel from "src/interfaces/ILoginModel";
import ISubmitAction from "src/interfaces/ISubmitActions";
import request from "src/services/request";
import { getUsersRequest } from "src/components/services/request";
import { getLocalStorage, removeLocalStorage, setLocalStorage } from "src/utils/localStorage";

type AuthState = {
    loading: boolean;
    isAuth: boolean,
    changePassword: boolean;
    account?: IAccount;
    status?: number;
    error?: IError;
}

const token = getLocalStorage('token');

const initialState: AuthState = {
    isAuth: !!token,
    loading: false,
    changePassword: true,
};

const AuthSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        setAccount: (state: AuthState, action: PayloadAction<IAccount>): AuthState => {
            const account = action.payload;
            if (account?.token) {
                console.log('aaccdscdskj');
                setLocalStorage('token', account.token);
                setLocalStorage('role', account.role);
                // account.role = getLocalStorage('role');
                // localStorage.setItem('token', account.token);
                request.setAuthentication(account.token);

                // getUsersRequest().then;
                // account.changePassword
            }

            return {
                ...state,
                // status: Status.Success,
                changePassword: true,
                account,
                isAuth: true,
                loading: false,
            };
        },
        setStatus: (state: AuthState, action: PayloadAction<SetStatusType>) => {
            const { status, error } = action.payload;

            return {
                ...state,
                status,
                error,
                loading: false,
            }
        },
        me: (state) => {
            if (token) {
                request.setAuthentication(token);
            }
        },
        login: (state: AuthState, action: PayloadAction<ILoginModel>) => ({
            ...state,
            loading: true,
            changePassword: true,
        }),
        changePassword: (state: AuthState, action: PayloadAction<ISubmitAction<IChangePasswordUser>>) => {
            return {
                ...state,
                loading: true,
            }
        },
        logout: (state: AuthState) => {

            removeLocalStorage('token');
            request.setAuthentication('')

            return {
                ...state,
                isAuth: false,
                account: undefined,
                status: undefined,
            };
        },
        cleanUp: (state) => ({
            ...state,
            loading: false,
            status: undefined,
            error: undefined,
        }),
    }
});

export const {
    setAccount, login, setStatus, me, changePassword, logout, cleanUp,
} = AuthSlice.actions;

export default AuthSlice.reducer;
