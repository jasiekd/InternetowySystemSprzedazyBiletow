import React from "react";
import '../styles/UserProfile.css';
import Header from '../components/Header';
import { GreenInput } from "../components/GreenInput";
import Footer from '../components/Footer';
import { useState, useEffect } from 'react';
import '../styles/DropDownMenu.css';
import '../styles/AddEvent.css';
import LocationComponent from "../components/LocationComponent";
import { checkIsLogged } from "../controllers/Login";
import { useNavigate } from "react-router-dom";
import LocationsController from "../controllers/LocationsController";

function LocationForm({addLocations}){
    const [title,setTitle] = useState("");
    const [description,setDescription] = useState("");
    const [imgURL,setImgURL] = useState("");

    const onAddLocation = () =>{
        addLocations(0,title,description,imgURL).then(r=>{
            if(r)
            {
                setTitle("")
                setDescription("")
                setImgURL("")
            }
        })
    }

    return(
        <div className="addEventContent">
            <div className="eventForm">
                <div className="content-data">
                    <div className="content-data-column formColumn" >
                        <h2>Dane lokalizacji</h2>    
                        <GreenInput value={title} label="Tytuł" onChange={(e)=>setTitle(e.target.value)} fullWidth type="text" ></GreenInput>
                        <GreenInput value={description} label="Opis" onChange={(e)=>setDescription(e.target.value)} fullWidth type="text" multiline rows={4} maxRows={40}></GreenInput>
                        <GreenInput value={imgURL} label="Link do zdjęcia" onChange={(e)=>setImgURL(e.target.value)} fullWidth type="text"></GreenInput>
                        <button className="main-btn" onClick={()=>onAddLocation()} >Dodaj</button>
                    </div>           
                </div>
            </div>
            <div className="evntPreview">
                <LocationComponent 
                    localText={description}
                    localTitle={title}
                    localImg={imgURL}
                    disableSearch={true}
                />                  
            </div>
        </div>

    );
}
function AddLocalization(){
    const navigate = useNavigate()
    const [ready,setReady] = useState(false);

    useEffect(()=>{
        if(checkIsLogged()==='1')
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
                    <LocationsController>
                        <LocationForm />
                    </LocationsController>
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
export default AddLocalization;