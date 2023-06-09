import React from "react";
import '../styles/UserProfile.css';
import Header from '../components/Header';
import { GreenInput } from "../components/GreenInput";
import Footer from '../components/Footer';
import { useState, useEffect } from 'react';
import '../styles/DropDownMenu.css';
import '../styles/AddEvent.css';
import { useNavigate } from "react-router-dom";
import { checkIsLogged } from "../controllers/Login";
import TypesOfEventsController from "../controllers/TypesOfEventsController";
function CategoryForm({addCategory}){
    const [title,setTitle] = useState("");

    const onAddCategory = () =>{
        addCategory(0,title).then(r=>{
            if(r)
                setTitle("");
        })
    }

    return(
        <div className="content-data">
            
            <div className="content-data-column formColumn" >
                <h2>Nazwa kategorii</h2>    
                <GreenInput inputProps={{ "data-testid": "test-category-title" }} value={title} label="Nazwa" onChange={(e)=>setTitle(e.target.value)} fullWidth type="text" ></GreenInput>
                <button data-testid='test-add-category' className="main-btn"  onClick={()=>onAddCategory()}>Dodaj</button>

            </div>    
        </div>
    );
}
function AddCategory(){
    const navigate = useNavigate()
    const [ready,setReady] = useState(false);

    useEffect(()=>{
        if(checkIsLogged()==='1')
            setReady(true);
        else
            navigate("/home");
    })

    return(
        <div className="App">
            <Header/>
            {
                ready?
                <main className='content'>
                    <TypesOfEventsController>
                        <CategoryForm /> 
                    </TypesOfEventsController>
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
export default AddCategory;