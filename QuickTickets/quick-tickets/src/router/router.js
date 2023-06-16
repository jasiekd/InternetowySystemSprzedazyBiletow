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
import MyEvents from "../view/MyEvents";
import EventsApproval from "../view/EventsApproval";
import OrganisersApproval from "../view/OrganisersApproval";
import AddLocalization from "../view/AddLocalization";
import AddCategory from "../view/AddCategory"
import AddAdmin from "../view/AddAdmin";
import EventsController from "../controllers/Events";
import OfflinePayment from "../view/OfflinePayment";

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
        element: <EventsController><Event/></EventsController>
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
    {
        path: "/my-events",
        element: <EventsController><MyEvents/></EventsController> 
    },
    {
        path: "/events-approval",
        element: <EventsController><EventsApproval/></EventsController>
    },
    {
        path: "/organisers-approval",
        element: <OrganisersApproval/>
    },
    {
        path: "/add-localization",
        element: <AddLocalization/>
    },
    {
        path: "/add-category",
        element: <AddCategory/>
    },
    {
        path: "/add-admin",
        element: <AddAdmin/>
    },
    {
        path: "/offline-payment",
        element: <OfflinePayment/>
    }
]);

export default router;