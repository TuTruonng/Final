import React, { useEffect, useState } from 'react';

import Users from 'src/containers/UsersList';

const UserManager = () => {
    return (
        <>
            <div className="primaryColor text-title intro-x">
                <Users />
            </div>
        </>
    );
};

export default UserManager;
