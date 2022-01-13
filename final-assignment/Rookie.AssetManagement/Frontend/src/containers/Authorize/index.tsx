import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import { Modal } from 'react-bootstrap';
import { Form, Formik, useFormik } from 'formik';
import * as Yup from 'yup';
import { CHANGEPASSWORD, HOME, LOGIN } from '../../constants/pages';
import Header from '../Layout/Header';
import TextField from 'src/components/FormInputs/TextField';
import ILoginModel from 'src/interfaces/ILoginModel';
import { useAppDispatch, useAppSelector } from 'src/hooks/redux';
import { loginRequest, getUser } from './sagas/requests';
import { cleanUp, login, setAccount, me } from './reducer';
import { NotificationManager } from 'react-notifications';

const initialValues: ILoginModel = {
    userName: '',
    password: '',
};

const Login = () => {
    const history = useHistory();
    const [errorMessage, setErrorMessage] = useState('');

    const dispatch = useAppDispatch();
    const { loading, error } = useAppSelector((state) => state.authReducer);

    const validationSchema = () => {
        return Yup.object().shape({
            userName: Yup.string().required('This field is required!'),
            password: Yup.string()
                .required('This field is required!')
                .min(6, 'Password must be at least 6 characters'),
        });
    };

    const handleShowErrorMessage = (message) => {
        setErrorMessage(message);
    };

    useEffect(() => {
        return () => {
            dispatch(cleanUp());
        };
    }, []);

    return (
        <>
            <div className="container">
                <div>
                    <div className="headText-Login">
                        <p className="text">
                            Welcome To Online Asset Management
                        </p>
                    </div>

                    <div className="form-body">
                        <Formik
                            initialValues={initialValues}
                            validationSchema={validationSchema}
                            onSubmit={(values, actions) => {
                                console.log({ values });
                                dispatch(login(values));
                                actions.setSubmitting(false);

                                loginRequest(values)
                                    .then(
                                        () => {
                                            history.push(HOME);
                                            // window.location.reload();
                                        }
                                    )
                                    .catch((error) => {
                                        if (error.response)
                                        handleShowErrorMessage(error.response.data.message);
                                    });
                            }}
                        >
                            {(formik) => {
                                const {
                                    values,
                                    handleChange,
                                    handleSubmit,
                                    errors,
                                    touched,
                                    handleBlur,
                                    isValid,
                                    dirty,
                                } = formik;
                                const mes = localStorage.getItem('message');
                                return (
                                    <Form className="intro-y">
                                        <TextField
                                            name="userName"
                                            label="Username"
                                            value={values.userName}
                                            onChange={handleChange}
                                            onBlur={handleBlur}
                                            type="text"
                                            isrequired
                                        />

                                        <div>
                                            <TextField
                                                name="password"
                                                label="Password"
                                                value={values.password}
                                                onChange={handleChange}
                                                onBlur={handleBlur}
                                                type="password"
                                                isrequired
                                            />
                                        </div>

                                        {errorMessage && (
                                            <div className="invalid">
                                                <p>{errorMessage} </p>
                                            </div>
                                        )}

                                        {error?.error && (
                                            <div className="invalid">
                                                {error.message}
                                            </div>
                                        )}

                                        <div className="text-center mt-5">
                                            <button
                                                id="btn-login"
                                                className="btn btn-danger"
                                                type="submit"
                                                disabled={!isValid || !dirty}
                                            >
                                                Login
                                                {loading && (
                                                    <img
                                                        src="/oval.svg"
                                                        className="w-4 h-4 ml-2 inline-block"
                                                    />
                                                )}
                                            </button>
                                        </div>
                                    </Form>
                                );
                            }}
                        </Formik>
                    </div>
                </div>
            </div>
        </>
    );
};

export default Login;
