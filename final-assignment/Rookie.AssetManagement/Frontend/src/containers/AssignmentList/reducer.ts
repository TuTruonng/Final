import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { SetStatusType } from '../../constants/status';
import IError from '../../interfaces/IError';
import IPagedModel from '../../interfaces/IPagedModel';
import IQueryAssignmentModel from '../../interfaces/Assignment/IQueryAssignmentModel';
import IAssignment from '../../interfaces/Assignment/IAssignment';
import IAssignmentForm from '../../interfaces/Assignment/IAssignmentForm';

type AssignmentState = {
    loading: boolean;
    assignmentResult?: IAssignment;
    assignments: IPagedModel<IAssignment> | null;
    status?: number;
    error?: IError;
    disable: boolean;
};

export type CreateAction = {
    handleResult: Function;
    formValues: IAssignmentForm;
};

export type DisableAction = {
    handleResult: Function;
    assetId: number;
};

const initialState: AssignmentState = {
    assignments: null,
    loading: false,
    disable: false,
};

const assignmentReducerSlice = createSlice({
    name: 'home',
    initialState,
    reducers: {
        getAssignments: (
            state,
            action: PayloadAction<IQueryAssignmentModel>
        ): AssignmentState => {
            return {
                ...state,
                loading: true,
            };
        },
        setAssignments: (
            state,
            action: PayloadAction<IPagedModel<IAssignment>>
        ): AssignmentState => {
            const assignments = action.payload;

            return {
                ...state,
                assignments,
                loading: false,
            };
        },
        setAssignment: (state, action: PayloadAction<IAssignment>): AssignmentState => {
            const assignmentResult = action.payload;

            return {
                ...state,
                assignmentResult,
                loading: false,
            };
        },
        setStatus: (
            state,
            action: PayloadAction<SetStatusType>
        ): AssignmentState => {
            const { status, error } = action.payload;

            return {
                ...state,
                status,
                error,
                loading: false,
            };
        },
        cleanUp: (state): AssignmentState => ({
            ...state,
            loading: false,
            assignmentResult: undefined,
            status: undefined,
            error: undefined,
        }),
    },
});

export const {
    setStatus,
    cleanUp,
    getAssignments,
    setAssignments,
} = assignmentReducerSlice.actions;

export default assignmentReducerSlice.reducer;
