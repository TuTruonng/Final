import ISelectOption from 'src/interfaces/ISelectOption';

export const UserTypeOptions: ISelectOption[] = [
    { id: 1, label: 'All', value: 'All' },
    { id: 2, label: 'Admin', value: 'ADMIN' },
    { id: 3, label: 'Staff', value: 'STAFF' },
];

export const UserGenderOptions: ISelectOption[] = [
    { id: 1, label: 'Female', value: 'Female' },
    { id: 2, label: 'Male', value: 'Male' },
];

export const AssetStateOptions: ISelectOption[] = [
    { id: 1, label: 'All', value: 'All' },
    { id: 2, label: 'Assigned', value: 'ASSIGNED' },
    { id: 3, label: 'Available', value: 'AVAILABLE' },
    { id: 4, label: 'Not available', value: 'NOT AVAILABLE' },
    { id: 5, label: 'Waiting for recycling', value: 'WAITING FOR RECYCLING' },
    { id: 6, label: 'Recycled', value: 'RECYCLED' },
];

export const AssetStateAssignmentOptions: ISelectOption[] = [
    { id: 1, label: 'All', value: 'All' },
    { id: 2, label: 'Accepted', value: 'Accepted' },
    { id: 3, label: 'Waiting for acceptance', value: 'Waiting for acceptance' },
];

export const AssetCategoriesOptions: ISelectOption[] = [
    { id: 1, label: 'All', value: 'All' },
    { id: 2, label: 'Laptop', value: 'LAPTOP' },
    { id: 3, label: 'Monitor', value: 'MONITOR' },
    { id: 4, label: 'Personal Computer', value: 'PERSONAL COMPUTER' },
    // { id: 5, label: 'Iphone', value: 'IPHONE' },
    // { id: 6, label: 'Bluetooth Mouse', value: 'BLUETOOTH MOUSE' },
    // { id: 7, label: 'Mobile', value: 'MOBILE' },
    // { id: 8, label: 'Headset', value: 'HEADSET' },
    // { id: 9, label: 'Ipad', value: 'IPAD' },
    // { id: 10, label: 'Tablet', value: 'TABLET' },
];

export const UserOptions: ISelectOption[] = [
    { id: 2, label: 'Admin', value: 'ADMIN' },
    { id: 3, label: 'Staff', value: 'STAFF' },
];

export const AssetOptions: ISelectOption[] = [
    { id: 1, label: 'Laptop', value: 'Laptop'},
    { id: 2, label: 'Monitor', value: 'Monitor'},
    { id: 3, label: 'Personal Computer', value: 'Personal Computer' },
];

export const AssetStateCreateOptions: ISelectOption[] = [
    { id: 1, label: 'Available', value: 'Available' },
    { id: 2, label: 'Not available', value:  'Not available' },
];

export const AssetStateEditOptions: ISelectOption[] = [
    { id: 1, label: 'Available', value: 'Available' },
    { id: 2, label: 'Not available', value:  'Not available' },
    { id: 3, label: 'Waiting for recycling', value:  'Waiting for recycling' },
    { id: 4, label: 'Recycled', value:  'Recycled' },
];