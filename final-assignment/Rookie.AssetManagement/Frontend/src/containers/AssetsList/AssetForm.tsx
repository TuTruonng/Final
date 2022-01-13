import React, { useEffect, useState } from 'react';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import moment, { invalid } from 'moment';
import { Link, useHistory } from 'react-router-dom';
import { NotificationManager } from 'react-notifications';
import differenceInYears from 'date-fns/differenceInYears';
import TextAreaField from 'src/components/FormInputs/TextAreaField';
import TextField from 'src/components/FormInputs/TextField';
import DateField from 'src/components/FormInputs/DateField';
import CheckboxField from 'src/components/FormInputs/CheckboxField';
import SelectField from 'src/components/FormInputs/SelectField';
import { ASSETMANAGER } from 'src/constants/pages';
import { useAppDispatch, useAppSelector } from 'src/hooks/redux';
import { createAsset, updateAsset } from './reducer';
import IAssetForm from 'src/interfaces/Asset/IAssetForm';
import { Status } from 'src/constants/status';
import { AssetOptions, AssetStateOptions, AssetStateCreateOptions, AssetStateEditOptions } from 'src/constants/selectOptions';

const _location = localStorage.getItem('location');

const initialFormValues: IAssetForm = {
    assetName: '',
    category: '',
    state: 'Available',
    specification: '',
    installedDate: undefined,
    location: `${_location}`,
};

const validationSchema = Yup.object().shape({
    assetName: Yup.string().required('Required'),
    category: Yup.string().required('Required'),
    specification: Yup.string()
        .nullable()
        .required('Required'),
    installedDate: Yup.string()
        .nullable()
        .required('Required'),
    state: Yup.string().required('Required'),

});

type Props = {
    initialAssetForm?: IAssetForm;
};

const AssetFormContainer: React.FC<Props> = ({
    initialAssetForm = {
        ...initialFormValues,
    },
}) => {
    const [loading, setLoading] = useState(false);

    const dispatch = useAppDispatch();

    const isUpdate = initialAssetForm.assetCode ? true : false;

    const history = useHistory();

    const handleResult = (result: boolean, message: string) => {
        if (result) {
            NotificationManager.success(
                `${isUpdate ? 'Updated' : 'Created'
                } Successful Asset ${message}`,
                `${isUpdate ? 'Update' : 'Create'} Successful`,
                2000
            );

            setTimeout(() => {
                history.push(ASSETMANAGER);
                window.location.reload();
            }, 1000);
        } else {
            NotificationManager.error(message, 'Create failed', 2000);
        }
    };

    return (
        <Formik
            initialValues={initialAssetForm}
            enableReinitialize
            validationSchema={validationSchema}
            onSubmit={(values) => {
                setLoading(true);
                setTimeout(() => {
                    if (isUpdate) {
                        const offset = values.installedDate?.getTimezoneOffset()
                        values.installedDate = new Date(moment.utc(values.installedDate).subtract(offset, 'minutes').format())
                        dispatch(
                            updateAsset({ handleResult, formValues: values })
                        );
                    } else {
                        const offset = values.installedDate?.getTimezoneOffset()
                        values.installedDate = new Date(moment.utc(values.installedDate).subtract(offset, 'minutes').format())
                        dispatch(
                            createAsset({ handleResult, formValues: values })
                        );
                    }
                    setLoading(false);
                }, 1000);
            }}
        >
            {(formik) => {
                const { isValid, dirty } = formik;
                return (
                    <Form className="intro-y col-lg-6 col-12">
                        <TextField
                            name="assetName"
                            label="Name"
                            isrequired
                            disabled={false}
                        />
                        <SelectField
                            name="category"
                            label="Category"
                            options={AssetOptions}
                            isrequired
                            disabled={isUpdate ? true : false}
                        />
                        <TextAreaField
                            name="specification"
                            label="Specification"
                            isrequired
                            disabled={false}
                        />
                        <DateField
                            name="installedDate"
                            label="Installed Date"
                            isrequired
                            disabled={isUpdate ? true : false}
                        />
                        <CheckboxField
                            name="state"
                            label="State"
                            options={isUpdate ? AssetStateEditOptions : AssetStateCreateOptions}
                            isrequired
                            disabled={isUpdate ? true : false}
                        />

                        <div className="d-flex">
                            <div className="ml-auto">
                                <button
                                    className="btn btn-danger"
                                    type="submit"
                                    disabled={!dirty || !isValid}
                                >
                                    Save{' '}
                                    {loading && (
                                        <img
                                            src="/oval.svg"
                                            className="w-4 h-4 ml-2 inline-block"
                                        />
                                    )}
                                </button>

                                <Link
                                    to={ASSETMANAGER}
                                    className="btn btn-outline-secondary ml-2"
                                >
                                    Cancel
                                </Link>
                            </div>
                        </div>
                    </Form>
                );
            }}
        </Formik>
    );
};

export default AssetFormContainer;
