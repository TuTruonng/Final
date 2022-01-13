import React, { Suspense } from "react";
import { Redirect, Route } from "react-router-dom";
import { UNAUTHORIZE } from "../constants/pages";
import Layout from "../containers/Layout";
import { useAppSelector } from "../hooks/redux";
import InLineLoader from "../components/InlineLoader";
import { checkUserIsAdmin } from "../utils/checkRole";

export default function PrivateRoute({ children, ...rest }) {
    const {account} = useAppSelector(state => state.authReducer);
    return (
        <Route
            {...rest}
            render={({ location }) =>
            checkUserIsAdmin(account) ?
                    (
                        <Suspense fallback={<InLineLoader />}>
                            <Layout>
                                {children}
                            </Layout>
                        </Suspense>
                    )
                    : <Redirect to={UNAUTHORIZE} />}
        />
    );
}