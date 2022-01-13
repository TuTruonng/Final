import React, { InputHTMLAttributes } from 'react';
import { useField, useFormikContext} from 'formik';
import { Form } from 'react-bootstrap';
import ISelectOption from 'src/interfaces/ISelectOption';

type InputFieldProps = InputHTMLAttributes<HTMLInputElement> & {
    label: string;
    name: string;
    isrequired?: boolean;
    notvalidate?: boolean;
    options: ISelectOption[];
};

const SelectField: React.FC<InputFieldProps> = (props) => {
    const [field, { error, touched, value }, { setValue }] = useField(props);
    const { name, options, notvalidate, label, isrequired, disabled } = props;

    const handleChange = (e) => {
        setValue(e.target.value);
    };

    const validateClass = () => {
        if (touched && error) return 'border-danger';
        if (notvalidate) return '';
        if (touched) return 'is-valid';

        return '';
    };

    return (
        <>
            <div className="mb-3 row">
                <label className="col-4 col-form-label d-flex font-weight-normal text-dark">
                    {label}
                    {isrequired && <div className="invalid ml-1"></div>}
                </label>

                <div className="col">
              
                    <select className={`border w-100 text-center ${validateClass()}`} 
                        onChange={handleChange}  
                        disabled={disabled}                   
                    >
                        <option selected hidden>
                         
                        </option>
                        {options.map(                          
                            ({                               
                                id,
                                label: optionLabel,
                                value: optionValue,
                            }) => (
                                <option
                                    {...field}
                                    key={id}
                                    value={optionValue}
                                    selected={optionValue === value}
                                  
                                >
                                    {optionLabel}
                                </option>
                            )
                        )}
                    </select>
                    {error && touched && <div className="invalid">{error}</div>}
                </div>
            </div>
        </>
    );
};
export default SelectField;
