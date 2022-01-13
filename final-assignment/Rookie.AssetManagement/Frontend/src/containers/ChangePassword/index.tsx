import React, { useEffect, useState } from 'react';
import { useHistory } from 'react-router-dom';
import { Modal } from 'react-bootstrap';
import { Form, Formik, useFormik } from 'formik';
import * as Yup from 'yup';
import { HOME, LOGIN } from '../../constants/pages';
import Header from '../Layout/Header';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import TextField from 'src/components/FormInputs/TextField';
import IChangePassword from 'src/interfaces/IChangePassword';
import { useAppDispatch, useAppSelector } from 'src/hooks/redux';
import { putChangePassword } from '../Authorize/sagas/requests';
import { cleanUp, login, setAccount, me } from '../Authorize/reducer';
import { handleChangePassword } from '../Authorize/sagas/handles';
import ILoginModel from 'src/interfaces/ILoginModel';

const initialValues: IChangePassword = {
    userName: '',
    newPassword: '',
};

const ChangePassword = () => {
    const [isShow, setShow] = useState(true);
    const history = useHistory();
    const dispatch = useAppDispatch();
    const { loading, error } = useAppSelector((state) => state.authReducer);

    const validationSchema = () => {
        return Yup.object().shape({
            newPassword: Yup.string()
                .required('This field is required!')
                .min(6, 'Password must be at least 6 characters'),
        });
    };

    return (
        <>
            <div className="container">
                <Modal
                    show={isShow}
                    dialogClassName="modal-90w"
                    aria-labelledby="login-modal"
                >
                    <Modal.Header closeButton>
                        <Modal.Title id="login-modal">
                            Change password
                        </Modal.Title>
                    </Modal.Header>

                    <Modal.Body>
                        <Formik
                            initialValues={initialValues}
                            validationSchema={validationSchema}
                            onSubmit={(values, actions) => {
                                const value = localStorage.getItem('user');
                                console.log(value);
                                actions.setSubmitting(false);
                                values.userName = value?.toString();
                                putChangePassword(values).then(
                                    () => {
                                        history.push(HOME);
                                    },
                                );
                            }}

                        >
                            {(formik) => {
                                const {
                                    values,
                                    handleChange,
                                    isValid,
                                    dirty
                                } = formik;
                                return (
                                    <Form className='intro-y'>
                                        <p>This is the first time you logged in.</p>
                                        <p>You have to change your password to continute.</p>
                                        <TextField
                                            name="newPassword"
                                            label="New Password"
                                            value={values.newPassword}
                                            onChange={handleChange}
                                            //type={showPassword ? "text" : "password"}
                                            isrequired
                                        />

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
                                                Save
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
                    </Modal.Body>
                </Modal>
            </div>
        </>
    );
};

export default ChangePassword;
