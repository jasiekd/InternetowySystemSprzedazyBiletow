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

function CategoryForm(){
    const [title,setTitle] = useState("");
    return(
        <div className="content-data">
            
            <div className="content-data-column formColumn" >
                <h2>Nazwa kategorii</h2>    
                <GreenInput value={title} label="Nazwa" onChange={(e)=>setTitle(e.target.value)} fullWidth type="text" ></GreenInput>
                <button className="main-btn"  >Dodaj</button>

            </div>    
        </div>
    );
}
function AddCategory(){
    
    return(
        <div className="App">
            <Header/>
            <main className='content'>
                
                    <CategoryForm />
                
            </main>
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>
        
    )
}
export default AddCategory;