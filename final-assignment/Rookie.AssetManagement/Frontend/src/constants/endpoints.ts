const Endpoints = {
    authorize: '/api/Authen',
    me: '/api/Authen/me',
    users: '/api/users',
    changePassword: '/api/users/password',
    disable: '/api/users/disable',
    disableId: (id) => `/api/users/disable/${id}`,
    usersId: (staffCode) => `/api/users/${staffCode}`,
    assets: '/api/Assets',
    assignments: '/api/Assignments',
    disableAsset: '/api/assets/disable',
    disableAssetId: (id) => `/api/assets/disable/${id}`,
    assetsId: (staffCode) => `/api/assets/${staffCode}`,
    authentication: '/api/Authen',
};

export default Endpoints;
