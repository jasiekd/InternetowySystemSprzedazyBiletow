import React from "react";
import '../styles/UserProfile.css';
import Header from '../components/Header';
import { GreenInput } from "../components/GreenInput";
import Footer from '../components/Footer';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import { useState, useEffect } from 'react';
import EventsController from '../controllers/Events.js';

function EventForm({onAddEvent}){
    const [title,setTitle] = useState("");
    const [seats,setSeats] = useState(0);
    const [ticketPrice,setTicketPrice] = useState(0);
    const [description,setDescription] = useState("");
    const [date,setDate] = useState("");
    const [adultsOnly,setAdultsOnly] = useState(null);
    const [imgURL,setImgURL] = useState("");

    function addNewEvent(){
        onAddEvent({title,seats,ticketPrice,description,date,adultsOnly,imgURL});
      
    
        setTitle("");
        setSeats(0);
        setTicketPrice(0);
        setDescription("");
        setDate("");
        setAdultsOnly(null);
        setImgURL("");
    }
    return(
        <div className="content-data">
                        <div className="content-data-column">
                            <h1>Dodaj wydarzenie:</h1>
                            <GreenInput label="Tytuł" onChange={(e)=>setTitle(e.target.value)} fullWidth type="text" ></GreenInput>
                            <GreenInput label="Ilość miejsc" onChange={(e)=>setSeats(e.target.value)} fullWidth type="number" ></GreenInput>
                            <GreenInput label="Cena biletu" onChange={(e)=>setTicketPrice(e.target.value)} fullWidth type="number" ></GreenInput>
                            <GreenInput label="Opis" onChange={(e)=>setDescription(e.target.value)} fullWidth type="text" ></GreenInput>
                            <GreenInput label="" onChange={(e)=>setDate(e.target.value)} fullWidth type="date"></GreenInput>
                            <GreenInput label="Link do zdjęcia" onChange={(e)=>setImgURL(e.target.value)} fullWidth type="text"></GreenInput>
                            
                            <FormControlLabel
                            control={<Checkbox />}
                            // onChange={(e)=>setAdultsOnly(e.target.value)}
                            label="18+"
                            labelPlacement="start"
                            />
                            {/* <label htmlFor='imageInput' className="main-btn">
                                <p>Dodaj zdjęcie</p>
                            </label>
                            <input id="imageInput" style={{display:'none'}} type="file" className="main-btn"></input> */}

                            <button className="main-btn" onClick={addNewEvent} type="submit">Dodaj</button>

                        </div>
                    </div>
    );
}
function AddEvent(){
    
    return(
        <div className="App">
            <Header/>
            <main className='content'>
                <EventsController>
                    <EventForm />
                </EventsController>
            </main>
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>
        
    )
}
export default AddEvent;