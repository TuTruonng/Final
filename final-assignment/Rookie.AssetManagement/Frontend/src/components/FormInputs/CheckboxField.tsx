import React, { InputHTMLAttributes } from 'react';
import { useField } from 'formik';
import { Form } from 'react-bootstrap';
import ISelectOption from 'src/interfaces/ISelectOption';

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
    label: string;
    name: string;
    isrequired?: boolean;
    options: ISelectOption[];
};

const CheckboxField: React.FC<InputFieldProps> = (props) => {
    const [field, { error, touched, value }, { setValue }] = useField(props);

    const { name, options, label, isrequired } = props;

    const handleChange = (e) => {
        setValue(e.target.value)
    };

    return (
        <>
            <div className="mb-3 row">
                <label className="col-4 col-form-label d-flex font-weight-normal text-dark">
                    {label}
                    {isrequired && (
                        <div className="invalid ml-1"></div>
                    )}
                </label>

                <div className="col">  
                    {
                        options.map(({ id, label: optionLabel, value: optionValue }) => (
                            <div className={
                                label == "Gender" ? "form-check form-check-inline" : "form-check"
                            } key={id}>
                                <input className="tu"
                                    id={id.toString()}
                                    type="radio"
                                    name={name}
                                    value={optionValue}
                                    onChange={handleChange}
                                    checked={optionValue === value}
                                />
                                <label className="form-check-label text-dark font-weight-normal" htmlFor={id.toString()}>
                                    {optionLabel}
                                </label>
                            </div>
                        )
                        )
                    }
                </div>
            </div>
        </>
    );
};
export default CheckboxField;
