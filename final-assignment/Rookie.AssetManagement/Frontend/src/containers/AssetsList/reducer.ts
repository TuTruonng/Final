import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { SetStatusType } from 'src/constants/status';

import IError from 'src/interfaces/IError';
import IPagedModel from 'src/interfaces/IPagedModel';
import IQueryAssetModel from 'src/interfaces/Asset/IQueryAssetModel';
import IAsset from 'src/interfaces/Asset/IAsset';
import IAssetForm from 'src/interfaces/Asset/IAssetForm';

type AssetState = {
    loading: boolean;
    assetResult?: IAsset;
    assets: IPagedModel<IAsset> | null;
    status?: number;
    error?: IError;
    disable: boolean;
};

export type CreateAction = {
    handleResult: Function;
    formValues: IAssetForm;
};

export type DisableAction = {
    handleResult: Function;
    assetId: number;
};

const initialState: AssetState = {
    assets: null,
    loading: false,
    disable: false,
};

const assetReducerSlice = createSlice({
    name: 'home',
    initialState,
    reducers: {
        getAssets: (
            state,
            action: PayloadAction<IQueryAssetModel>
        ): AssetState => {
            return {
                ...state,
                loading: true,
            };
        },
        setAssets: (
            state,
            action: PayloadAction<IPagedModel<IAsset>>
        ): AssetState => {
            const assets = action.payload;

            return {
                ...state,
                assets,
                loading: false,
            };
        },
        createAsset: (
            state,
            action: PayloadAction<CreateAction>
        ): AssetState => {
            return {
                ...state,
                loading: true,
            };
        },
        updateAsset: (
            state,
            action: PayloadAction<CreateAction>
        ): AssetState => {
            return {
                ...state,
                loading: true,
            };
        },
        disableAsset: (
            state,
            action: PayloadAction<DisableAction>
        ): AssetState => {
            return {
                ...state,
                loading: true,
            };
        },
        setAsset: (state, action: PayloadAction<IAsset>): AssetState => {
            const assetResult = action.payload;

            return {
                ...state,
                assetResult,
                loading: false,
            };
        },
        setStatus: (
            state,
            action: PayloadAction<SetStatusType>
        ): AssetState => {
            const { status, error } = action.payload;

            return {
                ...state,
                status,
                error,
                loading: false,
            };
        },
        cleanUp: (state): AssetState => ({
            ...state,
            loading: false,
            assetResult: undefined,
            status: undefined,
            error: undefined,
        }),
    },
});

export const {
    createAsset,
    setAsset,
    setStatus,
    cleanUp,
    getAssets,
    setAssets,
    updateAsset,
    disableAsset,
} = assetReducerSlice.actions;

export default assetReducerSlice.reducer;
