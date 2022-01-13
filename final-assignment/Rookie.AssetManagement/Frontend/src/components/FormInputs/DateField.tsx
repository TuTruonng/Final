import React, { InputHTMLAttributes, useEffect } from 'react';
import { useField, useFormikContext } from 'formik';
import { CalendarDateFill } from 'react-bootstrap-icons';
import DatePicker from 'react-datepicker';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import moment from 'moment';

type DateFieldProps = InputHTMLAttributes<HTMLInputElement> & {
    label: string;
    placeholder?: string;
    name: string;
    isrequired?: boolean;
    notvalidate?: boolean;
    maxDate?: Date;
    minDate?: Date;
    filterDate?: (date: Date) => boolean;
};

const DateField: React.FC<DateFieldProps> = (props) => {
    const { setFieldTouched, setFieldValue } = useFormikContext();
    const [field, { error, touched }, { setValue }] = useField(props);
    const {
        label,
        isrequired,
        notvalidate,
        maxDate,
        minDate,
        filterDate,
        disabled,
    } = props;

    const validateClass = () => {
        if (touched && error) return 'is-invalid border-danger';
        if (notvalidate) return '';
        if (touched) return 'is-valid';

        return '';
    };

    const handleChangeAssignedDate = (assignDate: Date) => {
        setValue(assignDate);
    };
    
    useEffect(() => {
        const temp = new Date(moment(field.value, "YYYY-MM-DD").format())
        if(disabled) setValue(new Date (temp))
    }, []);
    return (
        <>
            <div className="mb-3 row">
                <label className="col-4 col-form-label d-flex font-weight-normal text-dark">
                    {label}
                    {isrequired && <div className="invalid ml-1"></div>}
                </label>
                <div className="col">
                    <div className="d-flex align-items-center" id="date-field">
                        <DatePicker
                            {...field}
                            className={`border w-100 text-center ${validateClass()}`}
                            selected={
                               field.value
                            }
                            dateFormat='dd/MM/yyyy'
                            onChangeRaw={(e) => {
                                setFieldTouched(field.name, true, true);
                            }}
                            onChange={(date) =>
                                //handleChangeAssignedDate(date as Date)
                                setFieldValue(field.name, date)
                            }
                            showDisabledMonthNavigation
                            maxDate={maxDate}
                            minDate={minDate}
                            filterDate={filterDate}
                        />

                        <FontAwesomeIcon
                            className="icon-calendar"
                            icon="calendar"
                            //  onClick={filterDate}
                        />
                    </div>
                    {error && touched && <div className="invalid">{error}</div>}
                </div>
            </div>
        </>
    );
};
export default DateField;
