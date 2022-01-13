export const LOGIN = '/';
export const HOME = '/Home';
export const NOTFOUND = '/notfound';
export const UNAUTHORIZE = '/unauthorization';
export const CHANGEPASSWORD = '/ChangePassword';
// Users
export const USERMANAGER = '/Manager User';
export const CREATEUSER = '/Manager User/Create New User';
export const EDIT_USER = '/Manager User/Edit User/:staffCode';
export const EDIT_USER_ID = (staffCode: string | number) =>
    `/Manager User/Edit User/${staffCode}`;
// Assets
export const ASSETMANAGER = '/Manager Asset';
export const CREATEASSET = '/Manager Asset/Create New Asset';
export const EDIT_ASSET = '/Manager Asset/Edit Asset/:assetCode';
export const EDIT_ASSET_ID = (assetCode: string | number) =>
    `/Manager Asset/Edit Asset/${assetCode}`;
// Not ready to use
export const ASSIGNMENTMANAGER = '/Manage Assignment';
export const REQUEST = '/Request';
export const REPORT = '/Report';
