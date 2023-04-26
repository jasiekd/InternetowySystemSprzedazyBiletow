import * as React from 'react';
import "../styles/MainStyle.css";
import regBack from "../images/regBack.jpg";
import "../styles/LogReg.css";
import logo from "../images/logo.png";
import { GreenInput } from '../components/GreenInput';
import { useNavigate } from "react-router-dom";
import { GoogleLogin } from '@react-oauth/google';

export default function Register() {
    const navigate = useNavigate();
    return (
        <div className='logRegContent' style={{backgroundImage: 'url('+regBack+')'}}>
            <div className='accountFormContent'>
                <div className='accountFormHeader'>
                    <img className='logo' src={logo} onClick={()=>navigate("/")}/>
                </div>
                <div className='accountFormInputs'>
                    <GreenInput label="Login"/>
                    <GreenInput label="Hasło"/>
                    <GreenInput label="Email"/>
                </div>
                <div className='buttonsAccountMenu'>
                    <GoogleLogin
                        width= "1000px"
                   
                        onSuccess={credentialResponse => {
                        console.log(credentialResponse);
                        }}
                    
                        onError={() => {
                        console.log('Login Failed');
                        }}
                    />
                    <div className='accountFormButtons'>
                        <button className='main-btn accountFormButton'>Zarejestruj</button>
                        <button className='main-btn accountFormButton main-btn-negative' onClick={()=>navigate("/login")}>Logowanie</button>
                    </div>
                </div>
            </div>
        </div>
       
    )
}