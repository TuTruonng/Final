import IAsset from 'src/interfaces/Asset/IAsset';
import IAssetForm from 'src/interfaces/Asset/IAssetForm';
import axios, { AxiosResponse } from 'axios';
import qs from 'qs';
import RequestService from 'src/services/request';
import EndPoints from 'src/constants/endpoints';
import IQueryAssetModel from 'src/interfaces/Asset/IQueryAssetModel';

export function createAssetRequest(
    AssetForm: IAssetForm
): Promise<AxiosResponse<IAsset>> {
    return RequestService.axios
        .post(EndPoints.assets, AssetForm)
        .then((response) => {
            if (response.data) {
                localStorage.setItem('onTopAssetCode', response.data.assetCode);
            }
            return response;
        });
}

export function getAssetsRequest(
    query: IQueryAssetModel
): Promise<AxiosResponse<IAsset>> {
    return RequestService.axios.get(EndPoints.assets, {
        params: query,
        paramsSerializer: (params) => qs.stringify(params),
    });
}

export function UpdateAssetRequest(
    AssetForm: IAssetForm
): Promise<AxiosResponse<IAsset>> {
    const formData = new FormData();
    Object.keys(AssetForm).forEach((key) => {
        if (key === 'installedDate' && AssetForm.installedDate instanceof Date) {
            formData.append(key, (AssetForm[key] as Date).toISOString());
        }
        else {
            formData.append(key, AssetForm[key]);
        }
    });

    return RequestService.axios
        .put(EndPoints.assetsId(AssetForm.assetCode ?? -1), formData)
        .then((response) => {
            if (response.data) {
                localStorage.setItem('onTopAssetCode', response.data.assetCode);
            }
            return response;
        });
}

export function DisableAssetRequest(
    id: number
): Promise<AxiosResponse<Boolean>> {
    return RequestService.axios.put(EndPoints.disableAssetId(id));
}
