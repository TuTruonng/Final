import React, { InputHTMLAttributes, useState } from 'react';
import { useField } from 'formik';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
    label: string;
    placeholder?: string;
    name: string;
    isrequired?: boolean;
    notvalidate?: boolean;
};

const TextField: React.FC<InputFieldProps> = (props) => {
    const [field, { error, touched }, meta] = useField(props);
    const { label, isrequired, notvalidate, type } = props;

    const [showPassword, setShowPassword] = useState(false);

    const handleClickShowPassword = () => {
        setShowPassword(!showPassword);
    };

    const validateClass = () => {
        if (touched && error) return 'is-invalid';
        if (notvalidate) return '';
        if (touched) return 'is-valid';

        return '';
    };

    return (
        <>
            <div className="mb-3 row" id="textbox">
                <label className="col-4 col-form-label d-flex font-weight-normal text-dark">
                    {label}
                    {isrequired && <div className="invalid ml-1"></div>}
                </label>
                <div className="col">
                    <input
                        className={`form-control ${validateClass()}`}
                        {...field}
                        {...props}
                        type={
                            label == 'Password' || label == 'New Password' || label == 'Old Password'
                                ? showPassword
                                    ? 'text'
                                    : 'password'
                                : 'text'
                        }
                    />
                    {label == 'Password' && (
                        <FontAwesomeIcon
                            className="icon-eye"
                            icon={!showPassword ? 'eye' : 'eye-slash'}
                            onClick={handleClickShowPassword}
                        />
                    )}
                    {label == 'New Password' && (
                        <FontAwesomeIcon
                            className="icon-eye"
                            icon={!showPassword ? 'eye' : 'eye-slash'}
                            onClick={handleClickShowPassword}
                        />
                    )}
                     {label == 'Old Password' && (
                        <FontAwesomeIcon
                            className="icon-eye"
                            icon={!showPassword ? 'eye' : 'eye-slash'}
                            onClick={handleClickShowPassword}
                        />
                    )}
                    {error && touched && <div className="invalid">{error}</div>}
                </div>
            </div>
        </>
    );
};
export default TextField;
