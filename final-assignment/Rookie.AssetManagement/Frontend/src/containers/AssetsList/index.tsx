import React, { useEffect, useState } from 'react';

import Assets from 'src/containers/AssetsList/AssetManager';

const AssetManager = () => {
    return (
        <>
            <div className="primaryColor text-title intro-x">
                <Assets />
            </div>
        </>
    );
};

export default AssetManager;
