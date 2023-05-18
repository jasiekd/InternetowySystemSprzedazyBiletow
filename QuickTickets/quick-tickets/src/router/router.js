import React from "react";
import Home from './../view/Home';
import {
    createBrowserRouter,
} from "react-router-dom";
import Login from "../view/Login";
import Register from "../view/Register";
import SearchList from "../view/SearchList";
import Event from "../view/Event";
import BuyTicket from "../view/BuyTicket";
import UserProfile from "../view/UserProfile";
import AddEvent from "../view/AddEvent";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Home />,
    },
    {
        path: "/home",
        element: <Home />,
    },
    {
        path: "/login",
        element: <Login/>
    },
    {
        path: "/register",
        element: <Register/>
    },
    {
        path: "/search-list",
        element: <SearchList/>
    },
    {
        path: "/event",
        element: <Event/>
    },
    {
        path: "/buy-ticket",
        element: <BuyTicket/>
    },
    {
        path: "/user-profile",
        element: <UserProfile/>
    },
    {
        path: "/add-event",
        element: <AddEvent/>
    },
]);

export default router;