import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import NProgress from "nprogress";
import { Provider } from "react-redux";
import { library } from '@fortawesome/fontawesome-svg-core';
import { faEye, faEyeSlash, faCalendar } from '@fortawesome/free-solid-svg-icons';
import store from "src/redux/store";
import Routes from "./routes";
import "bootstrap/dist/css/bootstrap.min.css";
import "nprogress/nprogress.css";
import "react-notifications/lib/notifications.css";
import "react-datepicker/dist/react-datepicker.css";
import "./styles/App.scss";

library.add(faEyeSlash, faEye, faCalendar)
NProgress.configure({ minimum: 1 });

function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Routes />
      </BrowserRouter>
    </Provider>
  );
}

const ROOT = document.getElementById("root");
ReactDOM.render(<App />, ROOT);
