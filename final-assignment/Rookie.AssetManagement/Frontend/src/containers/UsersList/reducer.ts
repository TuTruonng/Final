import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { SetStatusType } from 'src/constants/status';
import IError from 'src/interfaces/IError';
import IPagedModel from 'src/interfaces/IPagedModel';
import IQueryUserModel from 'src/interfaces/User/IQueryUserModel';
import IUser from 'src/interfaces/User/IUser';
import IUserForm from 'src/interfaces/User/IUserForm';

type UserState = {
    loading: boolean;
    userResult?: IUser;
    users: IPagedModel<IUser> | null;
    status?: number;
    error?: IError;
    disable: boolean;
};

export type CreateAction = {
    handleResult: Function;
    formValues: IUserForm;
};

export type DisableAction = {
    handleResult: Function;
    userId: number;
};

const initialState: UserState = {
    users: null,
    loading: false,
    disable: false,
};

const userReducerSlice = createSlice({
    name: 'home',
    initialState,
    reducers: {
        getUsers: (
            state,
            action: PayloadAction<IQueryUserModel>
        ): UserState => {
            return {
                ...state,
                loading: true,
            };
        },
        setUsers: (
            state,
            action: PayloadAction<IPagedModel<IUser>>
        ): UserState => {
            const users = action.payload;

            return {
                ...state,
                users,
                loading: false,
            };
        },
        createUser: (state, action: PayloadAction<CreateAction>): UserState => {
            return {
                ...state,
                loading: true,
            };
        },
        updateUser: (state, action: PayloadAction<CreateAction>): UserState => {
            return {
                ...state,
                loading: true,
            };
        },
        disableUser: (
            state,
            action: PayloadAction<DisableAction>
        ): UserState => {
            return {
                ...state,
                loading: true,
            };
        },
        setUser: (state, action: PayloadAction<IUser>): UserState => {
            const userResult = action.payload;

            return {
                ...state,
                userResult,
                loading: false,
            };
        },
        setStatus: (state, action: PayloadAction<SetStatusType>): UserState => {
            const { status, error } = action.payload;

            return {
                ...state,
                status,
                error,
                loading: false,
            };
        },
        cleanUp: (state): UserState => ({
            ...state,
            loading: false,
            userResult: undefined,
            status: undefined,
            error: undefined,
        }),
    },
});

export const {
    createUser,
    setUser,
    setStatus,
    cleanUp,
    getUsers,
    setUsers,
    updateUser,
    disableUser,
} = userReducerSlice.actions;

export default userReducerSlice.reducer;
