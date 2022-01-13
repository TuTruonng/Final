import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
import { Status } from 'src/constants/status';
import IError from 'src/interfaces/IError';
import IQueryAssetModel from 'src/interfaces/Asset/IQueryAssetModel';
import {
    createAsset,
    setAsset,
    setStatus,
    cleanUp,
    getAssets,
    setAssets,
    updateAsset,
    disableAsset,
    CreateAction,
    DisableAction,
} from '../reducer';

import {
    createAssetRequest,
    DisableAssetRequest,
    getAssetsRequest,
    UpdateAssetRequest,
} from './requests';
import IAsset from 'src/interfaces/Asset/IAsset';

export function* handleCreateAsset(action: PayloadAction<CreateAction>) {
    const { handleResult, formValues } = action.payload;
    try {
        const { data } = yield call(createAssetRequest, formValues);

        if (data) {
            handleResult(true, data.name);
        }

        yield put(setAsset(data));
    } catch (error: any) {
        const errorModel = error.response.data as IError;

        handleResult(false, errorModel.message);
    }
}

export function* handleGetAssets(action: PayloadAction<IQueryAssetModel>) {
    console.log('getAsset')
    const query = action.payload;
    try {
        const { data } = yield call(getAssetsRequest, query);
        yield put(setAssets(data));
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

export function* handleUpdateAsset(action: PayloadAction<CreateAction>) {
    const { handleResult, formValues } = action.payload;
    try {
        const { data } = yield call(UpdateAssetRequest, formValues);

        if (data) {
            handleResult(true, data.name);
        }
        yield put(setAsset(data));
    } catch (error: any) {
        const errorModel = error.response.data as IError;
        handleResult(false, errorModel.message);
    }
}

export function* handleDisableAsset(action: PayloadAction<DisableAction>) {
    const { handleResult, assetId } = action.payload;

    try {
        const { data } = yield call(DisableAssetRequest, assetId);

        handleResult(data, '');
    } catch (error: any) {
        const errorModel = error.response.data as IError;

        handleResult(false, errorModel.message);
    }
}
