import React, { useState } from 'react';
import { Dropdown, Modal } from 'react-bootstrap';
import { useHistory, useLocation } from 'react-router-dom';
import ConfirmModal from 'src/components/ConfirmModal';
import { HOME } from 'src/constants/pages';
import { Form, Formik, useFormik } from 'formik';
import TextField from '../../components/FormInputs/TextField';
import IChangePasswordUser from '../../interfaces/IChangePasswordUser';
import { putChangePasswordUser } from '../Authorize/sagas/requests';
import * as Yup from 'yup';
import { useAppDispatch, useAppSelector } from 'src/hooks/redux';
import { logout } from '../Authorize/reducer';

const initialValues: IChangePasswordUser = {
    userName: '',
    currentPassword: '',
    newPassword: '',
};
// eslint-disable-next-line react/display-name
const CustomToggle = React.forwardRef<any, any>(
    ({ children, onClick }, ref): any => (
        <a
            className="btn btn-link dropdownButton"
            ref={ref as any}
            onClick={(e) => {
                e.preventDefault();
                onClick(e);
            }}
        >
            {children} <span>&#x25bc;</span>
        </a>
    )
);

const Header = () => {
    const history = useHistory();
    const { pathname } = useLocation();
    const { account, loading, error } = useAppSelector(
        (state) => state.authReducer
    );
    const dispatch = useAppDispatch();
    const [errorMessage, setErrorMessage] = useState('');
    //const [isShow, setShow] = useState(true);
    //const [showPassword, setShowPassword] = useState(false);
    const [showModalChangePasswod, setShowModalChangePasswod] = useState(false);
    const [showCompleteChangePasswod, setShowCompleteChangePasswod] =
        useState(false);
    const [showConfirmLogout, setShowConfirmLogout] = useState(false);

    const validationSchema = () => {
        return Yup.object().shape({
            newPassword: Yup.string()
                .required('This field is required!')
                .min(6, 'Password must be at least 6 characters'),
            currentPassword: Yup.string()
                .required('This field is required!')
                .min(6, 'Password must be at least 6 characters'),
        });
    };

    const headerName = () => {
        const pathnameSplit = pathname.split('/');
        pathnameSplit.shift();
        pathnameSplit.length > 2 ? pathnameSplit.pop() : pathnameSplit;
        return pathnameSplit.join(' > ').toString() || 'Home';
    };

    const handleChangePassword = () => {
        setShowModalChangePasswod(true);
    };

    const handleCancleChangePassword = () => {
        setShowModalChangePasswod(false);
    };

    const handleChangePasswordComplete = () => {
        setShowCompleteChangePasswod(true);
    };

    const handleClose = () => {
        setShowCompleteChangePasswod(false);
        setShowModalChangePasswod(false);
        window.location.reload();
        // history.push(pathname);
    };

    const handleLogout = () => {
        setShowConfirmLogout(true);
    };

    const handleCancleLogout = () => {
        setShowConfirmLogout(false);
    };

    const handleConfirmedLogout = () => {
        history.push(HOME);
        dispatch(logout());
    };

    const handleShowErrorMessage = (message) => {
        setErrorMessage(message);
    };

    return (
        <>
            <div className="header align-items-center font-weight-bold">
                <div className="container-lg-min container-fluid d-flex pt-2">
                    <p className="headText">{`${headerName()}`}</p>

                    <div className="ml-auto text-white">
                        <Dropdown>
                            <Dropdown.Toggle as={CustomToggle}>
                                {account?.userName}
                            </Dropdown.Toggle>

                            <Dropdown.Menu>
                                <Dropdown.Item onClick={handleChangePassword}>
                                    Change Password
                                </Dropdown.Item>
                                <Dropdown.Item onClick={handleLogout}>
                                    Logout
                                </Dropdown.Item>
                            </Dropdown.Menu>
                        </Dropdown>
                    </div>
                </div>
            </div>

            {/* Modal ChangePasswordUser */}
            <Modal
                show={showModalChangePasswod}
                onHide={handleCancleChangePassword}
                dialogClassName="modal-90w"
                aria-labelledby="login-modal"
            >
                <Modal.Header closeButton>
                    <Modal.Title id="login-modal">Change password</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    {showCompleteChangePasswod ? (
                        <>
                            <p>Your password has been changed successfully</p>
                            <div className="text-right mt-3">
                                <button
                                    className="btn btn-outline-secondary"
                                    onClick={handleClose}
                                    type="button"
                                >
                                    Close
                                </button>
                            </div>
                        </>
                    ) : (
                        <>
                            <Formik
                                initialValues={initialValues}
                                validationSchema={validationSchema}
                                onSubmit={(values, actions) => {
                                    const value = localStorage.getItem('user');
                                    console.log(value);
                                    values.userName = value?.toString();
                                    console.log({ values });
                                    actions.setSubmitting(false);
                                    putChangePasswordUser(values)
                                        .then(() => {
                                            setShowCompleteChangePasswod(true);
                                        })
                                        .catch((error) => {
                                            if (error.response)
                                                console.log(
                                                    error.response.data.message
                                                );
                                            handleShowErrorMessage(
                                                error.response.data.message
                                            );
                                        });
                                }}
                            >
                                {(formik) => {
                                    const {
                                        values,
                                        handleChange,
                                        isValid,
                                        dirty,
                                    } = formik;
                                    return (
                                        <Form className="intro-y">
                                            <TextField
                                                name="currentPassword"
                                                label="Old Password"
                                                value={values.currentPassword}
                                                onChange={handleChange}
                                                //type={showPassword ? "text" : "password"}
                                                isrequired
                                            />
                                            {errorMessage && (
                                                <div className="passwordInvalid">
                                                    <p>{errorMessage} </p>
                                                </div>
                                            )}

                                            <TextField
                                                name="newPassword"
                                                label="New Password"
                                                value={values.newPassword}
                                                onChange={handleChange}
                                                //type={showPassword ? "text" : "password"}
                                                isrequired
                                            />

                                            <div className="text-right mt-3">
                                                <button
                                                    id="btn-change"
                                                    className="btn btn-danger"
                                                    type="submit"
                                                    disabled={
                                                        !dirty || !isValid
                                                    }
                                                >
                                                    Save
                                                    {loading && (
                                                        <img
                                                            src="/oval.svg"
                                                            className="w-4 h-4 ml-2 inline-block"
                                                        />
                                                    )}
                                                </button>
                                                <button
                                                    className="btn btn-outline-secondary"
                                                    onClick={
                                                        handleCancleChangePassword
                                                    }
                                                    type="button"
                                                >
                                                    Cancel
                                                </button>
                                            </div>
                                        </Form>
                                    );
                                }}
                            </Formik>
                        </>
                    )}
                </Modal.Body>
            </Modal>

            {/* Modal ChangePassword Complete */}
            <Modal
                show={showCompleteChangePasswod}
                onHide={handleClose}
                dialogClassName="modal-90w"
                aria-labelledby="login-modal"
            >
                <Modal.Header closeButton>
                    <Modal.Title id="login-modal">Change password</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <p>Your password has been changed successfully</p>
                    <div className="text-right mt-3">
                        <button
                            className="btn btn-outline-secondary"
                            onClick={handleClose}
                            type="button"
                        >
                            Close
                        </button>
                    </div>
                </Modal.Body>
            </Modal>

            {/* Modal Logout*/}
            <ConfirmModal
                title="Are you sure"
                isShow={showConfirmLogout}
                onHide={handleCancleLogout}
            >
                <div>
                    <div className="text-center">Do you want to log out?</div>
                    <div className="text-center mt-3">
                        <button
                            className="btn btn-danger mr-3"
                            onClick={handleConfirmedLogout}
                            type="button"
                        >
                            Log out
                        </button>
                        <button
                            className="btn btn-outline-secondary"
                            onClick={handleCancleLogout}
                            type="button"
                        >
                            Cancel
                        </button>
                    </div>
                </div>
            </ConfirmModal>
        </>
    );
};

export default Header;
