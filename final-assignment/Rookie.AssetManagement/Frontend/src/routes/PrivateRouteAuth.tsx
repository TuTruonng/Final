import React, { Suspense } from "react";
import { Redirect, Route } from "react-router-dom";

import { LOGIN } from "src/constants/pages";
import Home from "../containers/Home";
import Login from "../containers/Authorize";
import Layout from "src/containers/Layout";
import { useAppSelector } from "src/hooks/redux";
import InLineLoader from "../components/InlineLoader";

export default function PrivateRoute({ children, ...rest }) {
    const { isAuth } = useAppSelector(state => state.authReducer);

    return (
        <Route
            {...rest}
            render={({ location }) =>
                isAuth ?
                    (
                        <Suspense fallback={<InLineLoader />}>
                            <Layout>
                                <Home />
                            </Layout>
                        </Suspense>
                    )
                    : 
                    <Suspense fallback={<InLineLoader />}>
                        <Layout>
                            <Login />
                        </Layout>
                    </Suspense>
                             
            }
            />
    );
}
