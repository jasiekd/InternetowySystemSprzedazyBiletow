import React from "react";
import '../styles/UserProfile.css';
import Header from '../components/Header';
import { GreenInput } from "../components/GreenInput";
import Footer from '../components/Footer';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import { useState, useEffect } from 'react';
import EventsController from '../controllers/Events.js';
import '../styles/DropDownMenu.css';
import { useLocation, useNavigate } from "react-router-dom";
import { checkIsLogged } from "../controllers/Login";
import { EventForm } from "./AddEvent";

export default function EditEvent({ getEvent}){
    const navigate = useNavigate()
    const [ready,setReady] = useState(false);
    const location = useLocation();
    React.useEffect(()=>{
        if(checkIsLogged()==='3')
            setReady(true);
        else
            navigate("/home");

        if(location.state ===null || location.state.event === null)
        {
            navigate("/home")
        }
        else{
           
                console.log(location.state.event);
        
        }
    },[])
    return(
        <div className="App">
            <Header/>
            {
                ready?
                <main className='content'>
                    <EventsController>
                        <EventForm 
                            editData={location.state.event}
                            editMode={true}
                        />
                    </EventsController>
                </main>
                :
                null
            }
            
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>
        
    )
}
