import React from "react";
import Home from './../view/Home';
import {
    createBrowserRouter
} from "react-router-dom";
import Login from "../view/Login";
import Register from "../view/Register";
import SearchList from "../view/SearchList";
import Event from "../view/Event";
import BuyTicket from "../view/BuyTicket";
import UserProfile from "../view/UserProfile";
import AddEvent from "../view/AddEvent";
import EventsApproval from "../view/EventsApproval";
import OrganisersApproval from "../view/OrganisersApproval";
import AddLocalization from "../view/AddLocalization";
import AddCategory from "../view/AddCategory"
import AddAdmin from "../view/AddAdmin";
import EventsController from "../controllers/Events";
import EventPreview from "../view/EventPreview";
import OfflinePayment from "../view/OfflinePayment";
import EditEvent from "../view/EditEvent";
import Redirect from "../view/Redirect";
import ShowUsers from "../view/ShowUsers";
import TicketController from "../controllers/TicketController";
import TransactionController from "../controllers/TransactionController";

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
        element: <TransactionController><TicketController><BuyTicket/></TicketController></TransactionController>
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
        path: "/event-preview",
        element: <EventPreview/>
    },
    {
        path: "/offline-payment",
        element: <OfflinePayment/>
    },
    {
        path:"/edit-event",
        element: <EventsController><EditEvent/></EventsController>
    },
    {
        path:"/buy-ticket/:id",
        element: <Redirect/>
    },
    {
        path:"/show-users",
        element: <ShowUsers />
    }
]);

export default router;