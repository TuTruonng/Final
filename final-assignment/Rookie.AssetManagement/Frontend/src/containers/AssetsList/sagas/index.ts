import { takeLatest } from 'redux-saga/effects';

import { createAsset, getAssets, updateAsset, disableAsset } from '../reducer';
import {
    handleCreateAsset,
    handleGetAssets,
    handleUpdateAsset,
    handleDisableAsset,
} from './handles';

export default function* AssetSagas() {
    yield takeLatest(createAsset.type, handleCreateAsset);
    yield takeLatest(getAssets.type, handleGetAssets);
    yield takeLatest(updateAsset.type, handleUpdateAsset);
    yield takeLatest(disableAsset.type, handleDisableAsset);
}
