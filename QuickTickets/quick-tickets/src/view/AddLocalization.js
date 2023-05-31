import React from "react";
import '../styles/UserProfile.css';
import Header from '../components/Header';
import { GreenInput } from "../components/GreenInput";
import Footer from '../components/Footer';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';
import { useState, useEffect } from 'react';
import LocationsController from '../controllers/Locations.js';
import '../styles/DropDownMenu.css';
import place from "../images/place.png";
import arrow from "../images/arrow.png"
import category from "../images/category.png";
import '../styles/AddEvent.css';
import EventComponent from "../components/EventComponent";
import LocationComponent from "../components/LocationComponent";

function LocationForm(){
    const [title,setTitle] = useState("");
    const [description,setDescription] = useState("");
    const [imgURL,setImgURL] = useState("");

   

    


   
    
   
    return(
        <div className="addEventContent">
        <div className="eventForm">
        <div className="content-data">
            
            <div className="content-data-column formColumn" >
                <h2>Dane lokalizacji</h2>    
                <GreenInput value={title} label="Tytuł" onChange={(e)=>setTitle(e.target.value)} fullWidth type="text" ></GreenInput>
                <GreenInput value={description} label="Opis" onChange={(e)=>setDescription(e.target.value)} fullWidth type="text" multiline rows={4} maxRows={40}></GreenInput>
                <GreenInput value={imgURL} label="Link do zdjęcia" onChange={(e)=>setImgURL(e.target.value)} fullWidth type="text"></GreenInput>
                <button className="main-btn"  >Dodaj</button>

            </div>
            
</div>
        </div>
            

        <div className="evntPreview">
                            <LocationComponent 
                               localText={description}
                               localTitle={title}
                               localImg={imgURL}
                                // eventTitle={title} 
                                // eventText={description}
                                // eventImg={imgURL}
                                // eventDate={date}
                                // eventPlace={selecredLocation}
                                // disableBuy={true}
                            />
                            
                        </div>
        </div>

    );
}
function AddLocalization(){
    
    return(
        <div className="App">
            <Header/>
            <main className='content'>
                
                    <LocationForm />
                
            </main>
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>
        
    )
}
export default AddLocalization;