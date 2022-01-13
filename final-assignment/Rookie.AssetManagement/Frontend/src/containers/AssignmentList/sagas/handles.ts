import { PayloadAction } from '@reduxjs/toolkit';
import { call, put } from 'redux-saga/effects';
import { Status } from '../../../constants/status';
import IError from '../../../interfaces/IError';
import IQueryAssignmentModel from '../../../interfaces/Assignment/IQueryAssignmentModel';
import {
    setStatus,
    cleanUp,
    getAssignments,
    setAssignments,
} from '../reducer';

import {
    getAssignmentsRequest,
} from './requests';
import IAssignment from '../../..//interfaces/Assignment/IAssignment';

export function* handleGetAssignments(action: PayloadAction<IQueryAssignmentModel>) {
    console.log('getAssignment')
    const query = action.payload;
    try {
        console.log('abc')
        const { data } = yield call(getAssignmentsRequest, query);
        yield put(setAssignments(data));
    } catch (error: any) {
        const errorModel = error.response.data as IError;

        console.log(errorModel);
        yield put(
            setStatus({
                status: Status.Failed,
                error: errorModel,
            })
        );
    }
}

