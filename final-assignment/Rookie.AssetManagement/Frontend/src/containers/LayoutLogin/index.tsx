import React from "react";
import { NotificationContainer } from 'react-notifications';

import Header from "./Header";

const LayoutLogin = ({ children }) => {
  return (
    <>
      <Header />

      <div className="container-lg-min container-fluid">
        <div className="row mt-5">
            <div className="col-lg-4 col-md-2 ms-4">
                <p></p>
            </div>
            <div className="col-lg-4 col-md-8 ms-4">
                {children}
            </div>
            <div className="col-lg-4 col-md-2 ms-4">
                <p></p>
            </div>
        </div>
      </div>
    </>
  );
};

export default LayoutLogin;
