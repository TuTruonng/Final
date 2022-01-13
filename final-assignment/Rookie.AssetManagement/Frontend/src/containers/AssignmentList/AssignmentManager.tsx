import React, { lazy } from 'react';
import { Edit } from 'react-feather';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import {
    HOME,
    ASSIGNMENTMANAGER,
} from '../../constants/pages';

const ListAssignments = lazy(() => import('./TableFunctions/List'));

const Assignments = () => {
    return (
        <>
            <Route exact path={ASSIGNMENTMANAGER} component={ListAssignments} />
        </>
    );
};

export default Assignments;
