import React, { useState } from "react";
import { Dropdown } from "react-bootstrap";
import { useHistory, useLocation } from "react-router-dom";
import ConfirmModal from "src/components/ConfirmModal";
import { HOME } from "src/constants/pages";

import { useAppDispatch, useAppSelector } from "src/hooks/redux";
import { logout } from "../Authorize/reducer";

// eslint-disable-next-line react/display-name
const CustomToggle = React.forwardRef<any, any>(({ children, onClick }, ref): any => (
  <a
    className="btn btn-link dropdownButton"
    ref={ref as any}
    onClick={(e) => {
      e.preventDefault();
      onClick(e);
    }}
  >
    {children} <span>&#x25bc;</span>
  </a>
));

const HeaderLogin = () => {
  const history = useHistory();
  const { pathname } = useLocation();
  const { account } = useAppSelector(state => state.authReducer);
  const dispatch = useAppDispatch();

  const [showModalChangePasswod, setShowModalChangePasswod] = useState(false);
  const [showConfirmLogout, setShowConfirmLogout] = useState(false);

  const headerName = () => {
    const pathnameSplit = pathname.split('/');
    pathnameSplit.shift();
    return pathnameSplit.join(' > ').toString() || 'Home';
  }

  const openModal = () => {
    setShowModalChangePasswod(true);
  };

  const handleHide = () => {
    setShowModalChangePasswod(false);
  }

  const handleLogout = () => {
    setShowConfirmLogout(true);
  };

  const handleCancleLogout = () => {
    setShowConfirmLogout(false);
  };

  const handleConfirmedLogout = () => {
    history.push(HOME);
    dispatch(logout());
  };

  return (
    <>
      <div className='header align-items-center font-weight-bold'>
        <div className="container-lg-min container-fluid d-flex pt-2">
          <img className="img-logo" src='/images/Logo_lk.png' alt='logo' />
            <p className="headText">Online Asset Management</p>
        </div>
      </div>
    </>
  );
};

export default HeaderLogin;
