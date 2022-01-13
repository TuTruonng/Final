import React, { useEffect, useState } from 'react';
import { Redirect, useParams } from 'react-router';
import { useHistory, useLocation } from 'react-router-dom';
import moment, { invalid } from 'moment';
import { NOTFOUND } from 'src/constants/pages';
import { useAppSelector } from 'src/hooks/redux';
import IAssetForm from 'src/interfaces/Asset/IAssetForm';
import { string } from 'yup/lib/locale';
import Users from '../AssetManager';
import UserFormContainer from '../AssetForm';
import format from "date-fns/format";
import AssetForm from '../AssetForm';

const UpdateAssetContainer = () => {
    const { users } = useAppSelector((state) => state.userReducer);
    const [asset, setAsset] = useState(undefined as IAssetForm | undefined);
    const { assetCode } = useParams<{ assetCode: string }>();
    const { state } = useLocation<{ existAsset: IAssetForm }>();
    const { existAsset } = state; // Read values passed on state

    useEffect(() => {
        if (existAsset) {
            //console.log(existAsset?.assetCode);
            setAsset(
                {
                    assetName: existAsset.assetName,
                    category: existAsset.category,
                    specification: existAsset.specification,
                    installedDate: existAsset.installedDate,
                    state: existAsset.state,
                    location: existAsset.location,
                    assetCode: existAsset.assetCode,
                }
            );
           
        }

    }, [existAsset]);
    console.log(asset?.assetCode);

    return (
        <div className="ml-5">
            <div className="primaryColor text-title intro-x">Edit Asset</div>

            <div className="row">
                {/* <UserFormContainer /> */}
                {asset && <AssetForm initialAssetForm={asset} />}
            </div>
        </div>
    );
};

export default UpdateAssetContainer;
