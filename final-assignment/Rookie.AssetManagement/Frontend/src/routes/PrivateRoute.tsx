import React, { Suspense } from "react";
import { Redirect, Route } from "react-router-dom";

import { HOME, CHANGEPASSWORD, LOGIN } from "src/constants/pages";
import ChangePassword from "../containers/ChangePassword";
import Home from "../containers/Home";
import Login from "../containers/Authorize";
import UserManager from "../containers/Home";
import Layout from "../containers/Layout";
import LayoutLogin from "../containers/LayoutLogin";
import { useAppSelector } from "src/hooks/redux";
import InLineLoader from "../components/InlineLoader";
import { boolean } from "yup/lib/locale";

export default function PrivateRoute({ children, ...rest }) {
    const { changePassword } = useAppSelector(state => state.authReducer);
    // const { isAuth } = useAppSelector(state => state.authReducer);
    const isAuth = localStorage.getItem('token');
    const value = localStorage.getItem('status');

    return (
        <Route
            {...rest}
            render={() =>
                isAuth ?
                    (
                        value === 'true' ?
                            (
                                <Suspense fallback={<InLineLoader />}>
                                    <Layout>
                                        {children}
                                    </Layout>
                                </Suspense>
                            )
                            :

                            <Suspense fallback={<InLineLoader />}>
                                <Layout>
                                    <ChangePassword />
                                </Layout>
                            </Suspense>

                    )

                    :
                    //<Redirect to={LOGIN} />
                    <Suspense fallback={<InLineLoader />}>
                        <LayoutLogin>
                            <Redirect to={LOGIN} />
                            <Login />
                        </LayoutLogin>
                    </Suspense>
            }
        />
    );
}