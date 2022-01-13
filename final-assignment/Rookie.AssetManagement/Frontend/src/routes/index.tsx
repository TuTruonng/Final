import React, { lazy, Suspense, useEffect } from 'react';
import { Route, Switch } from 'react-router-dom';
import {
    CHANGEPASSWORD,
    HOME,
    LOGIN,
    UNAUTHORIZE,
    NOTFOUND,
    USERMANAGER,
    CREATEUSER,
    EDIT_USER,
    ASSETMANAGER,
    CREATEASSET,
    EDIT_ASSET,
    ASSIGNMENTMANAGER
} from '../constants/pages';
import InLineLoader from '../components/InlineLoader';
import { useAppDispatch, useAppSelector } from '../hooks/redux';
import LayoutRoute from './LayoutRoute';
import LayoutRouteHome from './LayoutRouteHome';
import PrivateRoute from './PrivateRoute';
import PrivateRouteAuth from './PrivateRouteAuth';
import AdminRoute from './AdminRoute';
import Roles from 'src/constants/roles';
import { me } from 'src/containers/Authorize/reducer';
import CreateUserContainer from 'src/containers/UsersList/TableFunctions/Create';

const Home = lazy(() => import('../containers/Home'));
const Login = lazy(() => import('../containers/Authorize'));
const ChangePassword = lazy(() => import('../containers/ChangePassword'));
const NotFound = lazy(() => import('../containers/NotFound'));
const UnAuthorization = lazy(() => import('../containers/UnAuthorization'));
const UserManager = lazy(() => import('../containers/UserManager'));
const CreateUser = lazy(
    () => import('../containers/UsersList/TableFunctions/Create')
);
const EditUser = lazy(
    () => import('../containers/UsersList/TableFunctions/Update')
);

const CreateAsset = lazy(
    () => import('../containers/AssetsList/TableFunctions/Create')
);
const EditAsset = lazy(
    () => import('../containers/AssetsList/TableFunctions/Update')
);

const AssetManager = lazy(() => import('../containers/AssetsList/index'));
const AssignmentManager = lazy(() => import('../containers/AssignmentList/index'));
const SusspenseLoading = ({ children }) => (
    <Suspense fallback={<InLineLoader />}>{children}</Suspense>
);


const Routes = () => {
    const { isAuth, account } = useAppSelector((state) => state.authReducer);
    const dispatch = useAppDispatch();

    useEffect(() => {
        dispatch(me());
    }, []);

    return (
        <SusspenseLoading>
            <Switch>
                {/*Use AdminRoute if only admin is allow to access that route*/}
                <LayoutRouteHome exact path={LOGIN}>
                    <Login />
                </LayoutRouteHome>
                <PrivateRoute exact path={HOME}>
                    <Home />
                </PrivateRoute>
                <LayoutRoute exact path={UNAUTHORIZE}>
                    <UnAuthorization />
                </LayoutRoute>
                <LayoutRoute exact path={NOTFOUND}>
                    <NotFound />
                </LayoutRoute>
                <PrivateRoute exact path={USERMANAGER}>
                    <UserManager />
                </PrivateRoute>
                <PrivateRoute exact path={CREATEUSER}>
                    <CreateUser />
                </PrivateRoute>
                <PrivateRoute exact path={EDIT_USER}>
                    <EditUser />
                </PrivateRoute>
                <PrivateRoute exact path={ASSETMANAGER}>
                    <AssetManager />
                </PrivateRoute>
                <PrivateRoute exact path={CREATEASSET}>
                    <CreateAsset />
                </PrivateRoute>
                <PrivateRoute exact path={EDIT_ASSET}>
                    <EditAsset />
                </PrivateRoute>
                <PrivateRoute exact path={ASSIGNMENTMANAGER}>
                    <AssignmentManager />
                </PrivateRoute>
                {/* <PrivateRoute exact path={CHANGEPASSWORD}>
                      <ChangePassword />
                    </PrivateRoute> */}
            </Switch>
        </SusspenseLoading>
    );
};

export default Routes;
