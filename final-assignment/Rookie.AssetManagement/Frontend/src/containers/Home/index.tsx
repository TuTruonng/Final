import React, { useEffect, useState } from 'react';
import Users from 'src/containers/UsersList/index';

const Home = () => {
    return (
        <>
            <div className="primaryColor text-title intro-x">
                My Home
                <Users />
            </div>
        </>
    );
};

export default Home;
