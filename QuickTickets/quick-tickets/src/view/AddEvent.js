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
import EventComponent from "../components/EventComponent";
import LocationComponent from "../components/LocationComponent";
import dayjs from 'dayjs';
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import { useNavigate } from "react-router-dom";
import { checkIsLogged } from "../controllers/Login";
export function EventForm({editData,editMode,onAddEvent,getTypesOfEvents,getEventLocations,updateEvent}){
    const [title,setTitle] = useState("");
    const [seats,setSeats] = useState(0);
    const [ticketPrice,setTicketPrice] = useState(0);
    const [description,setDescription] = useState("");
    const [date, setDate] = React.useState(dayjs('2022-04-17T15:30'));
    const [adultsOnly,setAdultsOnly] = useState(true);
    const [imgURL,setImgURL] = useState("");
    const navigate = useNavigate();
    const addNewEvent=()=>{
        // if(isNaN(date)) return;
        // let newdate=date.$d.toISOString();
        // console.log(newdate)

        onAddEvent({title,seats,ticketPrice,description,date,adultsOnly,imgURL,typeID,locationID}).then((result)=>{
            setTitle("");
            setSeats(0);
            setTicketPrice(0);
            setDescription("");
            setDate("");
            setAdultsOnly(null);
            setImgURL("");
            setSelectedCategory("Kategoria");
            setSelectedLocation("Miejsce");
        });
      
       
    
        
    }

    const clickUpdateEvent = () =>{
        updateEvent(editData.eventID,title,seats,ticketPrice,description,date,adultsOnly,typeID,locationID,imgURL).then(r=>{
            navigate("/user-profile")
        })
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

    useEffect(()=>{
        if(editMode)
        {
            setTitle(editData.title);
            setSeats(editData.seats);
            setTicketPrice(editData.ticketPrice);
            setDescription(editData.description);
            setDate("");
            setAdultsOnly(editData.adultsOnly);
            setImgURL(editData.imgURL);
            setLocationID(editData.location.locationID)
            setSelectedLocation(editData.location.name);
            setSelectedCategory(editData.type.description);
            setTypeID(editData.type.typeID)
            setDate(dayjs(editData.date))
        }
    },[])

    return(
        <div className="addEventContent">
        <div className="eventForm">
        <div className="content-data">
            
            <div className="content-data-column formColumn" >
                <h2>Dane wydarzenia</h2>    
                <GreenInput value={title} label="Tytuł" onChange={(e)=>setTitle(e.target.value)} fullWidth type="text" inputProps={{ "data-testid": "test-event-title" }}></GreenInput>
                <div className='addEventOption'>
                {openPlace ? (
                            <div className="menu">
                                {
                                locationsList.map((val,key)=>{
                                    return(
                                        <button className='drop-down-btn' key={key} onClick={()=>selectLocation(val.name,val.locationID)} value={val.name}>{val.name}</button>
                                    )
                                })
                            }
                            </div>
                        ) : null}
                    <button data-testid="buttonn" className='filteringButton addEvent-filtering' onClick={handleOpenPlace} ><img className='leftIcon' src={place} /><div className='filteringTitle'>{selecredLocation}</div><img className='rightIcon' src={arrow}/></button>
                    
                       
                </div>
                <div className='addEventOption'>
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
                    <button data-testid="buttonn2" className='filteringButton addEvent-filtering' onClick={handleOpenCategory}><img className='leftIcon' src={category}/><div className='filteringTitle'>{selectedCategory}</div><img className='rightIcon' src={arrow}/></button>
                    
                        
                </div>
                <GreenInput value={seats} label="Ilość miejsc" onChange={(e)=>setSeats(e.target.value)} fullWidth type="number" inputProps={{ "data-testid": "test-event-seats" }} ></GreenInput>
                <GreenInput value={ticketPrice} label="Cena biletu" onChange={(e)=>setTicketPrice(e.target.value)} fullWidth type="number" inputProps={{ "data-testid": "test-event-price" }} ></GreenInput>
                <GreenInput value={description} label="Opis" onChange={(e)=>setDescription(e.target.value)} fullWidth type="text" multiline rows={4} maxRows={40} inputProps={{ "data-testid": "test-event-description" }} ></GreenInput>
                {/* <GreenInput value={date} label="" onChange={(e)=>setDate(e.target.value)} fullWidth type="date" inputProps={{ "data-testid": "test-event-date" }}></GreenInput> */}
                <LocalizationProvider dateAdapter={AdapterDayjs} sx={{width:"100%"}}>
                <DemoContainer components={['DateTimePicker']} sx={{width:"100%"}}> 
                    <DateTimePicker sx={{width:"100%"}}
                    label="Data"
                    value={date}
                    onChange={(newValue) => {setDate(newValue)}}

                    />
                </DemoContainer>
                </LocalizationProvider>
                <GreenInput value={imgURL} label="Link do zdjęcia" onChange={(e)=>setImgURL(e.target.value)} fullWidth type="text" inputProps={{ "data-testid": "test-event-img" }}></GreenInput>
                
                <FormControlLabel
                    control={<Checkbox inputProps={{ "data-testid": "test-event-adultsOnly" }}/>}
                    onClick={()=>setAdultsOnly(!adultsOnly)}
                    checked={adultsOnly}
                    label="18+"
                    labelPlacement="start"
                    
                />
                {/* <label htmlFor='imageInput' className="main-btn">
                    <p>Dodaj zdjęcie</p>
                </label>
                <input id="imageInput" style={{display:'none'}} type="file" className="main-btn"></input> */}
                {
                    editMode?
                    <button className="main-btn"  onClick={()=>clickUpdateEvent()}>Zapisz</button>
                    :
                    <button className="main-btn"  onClick={()=>addNewEvent()} >Dodaj</button>
                }
                

            </div>
            
</div>
        </div>
            

        <div className="evntPreview">
                            <EventComponent 
                                eventTitle={title} 
                                eventText={description}
                                eventImg={imgURL}
                                eventDate={date?.$d}
                                eventPlace={selecredLocation}
                                disableBuy={true}
                                displayPreview={true}
                                occupiedSeats={0}
                                seats={seats}
                            />
                            
                        </div>
        </div>

    );
}
function AddEvent(){
    const navigate = useNavigate()
    const [ready,setReady] = useState(false);

    React.useEffect(()=>{
        if(checkIsLogged()==='3')
            setReady(true);
        else
            navigate("/home");
    },[])
    return(
        <div className="App">
            <Header/>
            {
                ready?
                <main className='content'>
                    <EventsController>
                        <EventForm />
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
export default AddEvent;