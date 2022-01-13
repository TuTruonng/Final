import React, { lazy } from 'react';
import { Edit } from 'react-feather';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import {
    HOME,
    ASSETMANAGER,
    CREATEASSET,
    EDIT_ASSET,
} from '../../constants/pages';

const ListAssets = lazy(() => import('./TableFunctions/List'));
const CreateAsset = lazy(() => import('./TableFunctions/Create'));
const EditAsset = lazy(() => import('./TableFunctions/Update'));

const Assets = () => {
    return (
        <>
            <Route exact path={ASSETMANAGER} component={ListAssets} />
            <Route exact path={CREATEASSET} component={CreateAsset} />
            <Route path={EDIT_ASSET} component={EditAsset} />
        </>
    );
};

export default Assets;
