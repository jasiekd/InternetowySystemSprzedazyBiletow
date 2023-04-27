import * as React from 'react';
import "../styles/MainStyle.css";
import { useNavigate } from "react-router-dom";
import logo from "../images/logo.png";
import '../styles/Header.css';
import TextField from '@mui/material/TextField';
import  SearchInput  from './SearchInput';
export default function Header() {
    const navigate = useNavigate();
    return (
        <header className="App-header">
            <div className='header-content'>
                <img className='main-logo' src={logo}/>
                
                <div style={{width:"40rem"}}>
                    <SearchInput />  
                </div>
                <button className='main-btn login-nav-login' onClick={()=>navigate("/login")}>Zaloguj</button>
            </div>
        </header>
    )
}

