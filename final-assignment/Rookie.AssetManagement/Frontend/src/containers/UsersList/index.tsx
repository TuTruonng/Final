import React, { lazy } from 'react';
import { Edit } from 'react-feather';
import { BrowserRouter, Route, Switch } from 'react-router-dom';

import {
    HOME,
    USERMANAGER,
    CREATEUSER,
    EDIT_USER,
} from '../../constants/pages';

const ListUsers = lazy(() => import('./TableFunctions/List'));
const CreateUser = lazy(() => import('./TableFunctions/Create'));
const EditUser = lazy(() => import('./TableFunctions/Update'));

const Users = () => {
    return (
        <>
            <Route exact path={USERMANAGER} component={ListUsers} />
            <Route exact path={CREATEUSER} component={CreateUser} />
            <Route path={EDIT_USER} component={EditUser} />
        </>
    );
};

export default Users;
