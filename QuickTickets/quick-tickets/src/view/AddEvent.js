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
import place from "../images/place.png";
import arrow from "../images/arrow.png"
import category from "../images/category.png";
import '../styles/AddEvent.css';

function EventForm({onAddEvent,getTypesOfEvents,getEventLocations}){
    const [title,setTitle] = useState("");
    const [seats,setSeats] = useState(0);
    const [ticketPrice,setTicketPrice] = useState(0);
    const [description,setDescription] = useState("");
    const [date,setDate] = useState("");
    const [adultsOnly,setAdultsOnly] = useState(null);
    const [imgURL,setImgURL] = useState("");

    const addNewEvent=()=>{
        onAddEvent({title,seats,ticketPrice,description,date,adultsOnly,imgURL,typeID,locationID}).then((result)=>{
            setTitle("");
            setSeats(0);
            setTicketPrice(0);
            setDescription("");
            setDate("");
            setAdultsOnly(null);
            setImgURL("");
        });
      
    
        
    }

    const handleOpenPlace = () => {
        setOpenPlace(!openPlace);
    };
    const handleOpenCategory = () => {
        setOpenCategory(!openCategory);
    };
    const [openPlace, setOpenPlace] = React.useState(false);
    const [openCategory, setOpenCategory] = React.useState(false);
    const [locationsList,setLocationsList] = React.useState([]);
    const [categoryList,setCategoryList] = React.useState([]);

    useEffect(()=>{
        getEventLocations().then((result)=>{
            setLocationsList(result);
        })
        getTypesOfEvents().then((result)=>{
            setCategoryList(result);
        })
    },[])
    const [selectedCategory,setSelectedCategory] = React.useState("Kategoria");
    const [selecredLocation,setSelectedLocation] = React.useState("Miejsce");
    const [typeID,setTypeID] = useState(0);
    const [locationID,setLocationID] = useState(0);
    const selectLocation = (name,id) =>{
        setSelectedLocation(name);
        setLocationID(id);
        handleOpenPlace();
    }
    const selectCategory = (name,id) =>{
        setSelectedCategory(name);
        setTypeID(id);
        handleOpenCategory();
    }
    return(
        <div className="content-data">
                        <div className="content-data-column">
                            <h1>Dodaj wydarzenie:</h1>
                            <GreenInput value={title} label="Tytuł" onChange={(e)=>setTitle(e.target.value)} fullWidth type="text" ></GreenInput>
                            <GreenInput value={seats} label="Ilość miejsc" onChange={(e)=>setSeats(e.target.value)} fullWidth type="number" ></GreenInput>
                            <GreenInput value={ticketPrice} label="Cena biletu" onChange={(e)=>setTicketPrice(e.target.value)} fullWidth type="number" ></GreenInput>
                            <GreenInput value={description} label="Opis" onChange={(e)=>setDescription(e.target.value)} fullWidth type="text" ></GreenInput>
                            <GreenInput value={date} label="" onChange={(e)=>setDate(e.target.value)} fullWidth type="date"></GreenInput>
                            <div className='addEventOption'>
                                <button className='filteringButton addEvent-filtering' onClick={handleOpenPlace}><img className='leftIcon' src={place}/><div className='filteringTitle'>{selecredLocation}</div><img className='rightIcon' src={arrow}/></button>
                                
                                    {openPlace ? (
                                        <div className="menu">
                                            {
                                            locationsList.map((val,key)=>{
                                                return(
                                                    <button className='drop-down-btn' key={key} onClick={()=>selectLocation(val.name,val.locationID)}>{val.name}</button>
                                                )
                                            })
                                        }
                                        </div>
                                    ) : null}
                            </div>
                            <div className='addEventOption'>
                                <button className='filteringButton addEvent-filtering' onClick={handleOpenCategory}><img className='leftIcon' src={category}/><div className='filteringTitle'>{selectedCategory}</div><img className='rightIcon' src={arrow}/></button>
                                
                                    {openCategory ? (
                                        <div className="menu">
                                            {
                                            categoryList.map((val,key)=>{
                                                return(
                                                    <button className='drop-down-btn' key={key} onClick={()=>selectCategory(val.description,val.typeID)}>{val.description}</button>
                                                )
                                            })
                                        }
                                        </div>
                                    ) : null}
                            </div>
                            <GreenInput value={imgURL} label="Link do zdjęcia" onChange={(e)=>setImgURL(e.target.value)} fullWidth type="text"></GreenInput>
                            
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

                            <button className="main-btn"  onClick={()=>addNewEvent()}>Dodaj</button>

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