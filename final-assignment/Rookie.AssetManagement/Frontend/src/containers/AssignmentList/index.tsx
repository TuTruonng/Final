import React, { useEffect, useState } from 'react';

import Assignments from './AssignmentManager';

const AssignmentManager = () => {
    return (
        <>
            <div className="primaryColor text-title intro-x">
                <Assignments />
            </div>
        </>
    );
};

export default AssignmentManager;
