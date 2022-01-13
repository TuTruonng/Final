import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
import { Status } from 'src/constants/status';
import IError from 'src/interfaces/IError';
import IQueryUserModel from 'src/interfaces/User/IQueryUserModel';
import {
    createUser,
    setUser,
    setStatus,
    cleanUp,
    getUsers,
    setUsers,
    updateUser,
    disableUser,
    CreateAction,
    DisableAction,
} from '../reducer';

import {
    createUserRequest,
    DisableUserRequest,
    getUsersRequest,
    UpdateUserRequest,
} from './requests';
import IUser from 'src/interfaces/User/IUser';

export function* handleCreateUser(action: PayloadAction<CreateAction>) {
    const { handleResult, formValues } = action.payload;
    try {
        const { data } = yield call(createUserRequest, formValues);

        if (data) {
            handleResult(true, data.name);
        }

        yield put(setUser(data));
    } catch (error: any) {
        const errorModel = error.response.data as IError;

        handleResult(false, errorModel.message);
    }
}

export function* handleGetUsers(action: PayloadAction<IQueryUserModel>) {
    const query = action.payload;
    try {
        const { data } = yield call(getUsersRequest, query);
        yield put(setUsers(data));
    } catch (error: any) {
        const errorModel = error.response.data as IError;

        console.log(errorModel);
        yield put(
            setStatus({
                status: Status.Failed,
                error: errorModel,
            })
        );
    }
}

export function* handleUpdateUser(action: PayloadAction<CreateAction>) {
    const { handleResult, formValues } = action.payload;
    try {
        const { data } = yield call(UpdateUserRequest, formValues);
      
        if (data) {
            handleResult(true, data.name);
        }
        yield put(setUser(data));
    } catch (error: any) {
        const errorModel = error.response.data as IError;
        handleResult(false, errorModel.message);
    }
}

export function* handleDisableUser(action: PayloadAction<DisableAction>) {
    const { handleResult, userId } = action.payload;

    try {
        const { data } = yield call(DisableUserRequest, userId);

        handleResult(data, '');
    } catch (error: any) {
        const errorModel = error.response.data as IError;

        handleResult(false, errorModel.message);
    }
}
