import * as React from 'react';
import "../styles/MainStyle.css";
import regBack from "../images/regBack.jpg";
import "../styles/LogReg.css";
import logo from "../images/logo.png";
import { GreenInput } from '../components/GreenInput';
import { useNavigate } from "react-router-dom";
import RegisterController from '../controllers/Register';
import dayjs from 'dayjs';

function RegisterFormView({onRegister})
{
    const navigate = useNavigate();

    const [loginRegVal,setLoginRegVal] = React.useState("");
    const [passwordRegVal,setPasswordRegVal] = React.useState("");
    const [emailRegVal,setEmailRegVal] = React.useState("");
    const [nameRegVal, setNameRegVal] = React.useState("");
    const [surnameRegVal,setSurnameRegVal] = React.useState("");
    const [dateOfBirthRegVal,setDateOfBirdthRegVal] = React.useState(dayjs('2022-04-17'));

    const [errorStatus, setErrorStatus] = React.useState(false);
    const [errorText, setErrorText] = React.useState("");

    const onChangeLoginVal = (val) => {
        const regex = /^[a-zA-Z0-9._-]{5,}$/;
        setLoginRegVal(val);
        setErrorStatus(false);
        setErrorText("");
        if(!regex.test(val))
        {
            setErrorStatus(true);
            setErrorText("Login musi mieć minimum 5 znaków");
        }
    }
    const onChangePasswordVal = (val) => {
        const regex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/;
        setPasswordRegVal(val);
        setErrorStatus(false);
        setErrorText("");
        if(!regex.test(val))
        {
            setErrorStatus(true);
            setErrorText("Hasło musi zawieraż conajmniej: jedną dużą i małą litere, liczbe oraz 8 znaków ");
        }
    }
    const onChangeEmailVal = (val) => {
        const regex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
        setEmailRegVal(val);
        setErrorStatus(false);
        setErrorText("");
        if(!regex.test(val))
        {
            setErrorStatus(true);
            setErrorText("Nie poprawny format adresu email");
        }
    }
    const onChangeNameVal = (val) => {
        setNameRegVal(val);
        setErrorStatus(false);
        setErrorText("");
    }
    const onChangeSurnameVal = (val) => {
        setSurnameRegVal(val);
        setErrorStatus(false);
        setErrorText("");
    }
    const onChangeDateOfBirthVal = (val) => {
        setDateOfBirdthRegVal(val);
        setErrorStatus(false);
        setErrorText("");
    }

    const onClickRegister = () =>{

            onRegister({loginRegVal,passwordRegVal,emailRegVal,nameRegVal,surnameRegVal,dateOfBirthRegVal},setErrorStatus,setErrorText,errorStatus);
        
    } 
    return(
        <div>
             <div className='accountFormInputs'>
                    <GreenInput label="Login" error={errorStatus} value={loginRegVal} helperText={errorText} onChange={(e)=>onChangeLoginVal(e.target.value)}/>
                    <GreenInput label="Hasło" error={errorStatus} value={passwordRegVal} helperText={errorText} onChange={(e)=>onChangePasswordVal(e.target.value)} type='password'/>
                    <GreenInput label="Email" error={errorStatus} value={emailRegVal} helperText={errorText} onChange={(e)=>onChangeEmailVal(e.target.value)}/>
                    <GreenInput label="Imię"  error={errorStatus} value={nameRegVal} helperText={errorText} onChange={(e)=>onChangeNameVal(e.target.value)}/>
                    <GreenInput label="Nazwisko" error={errorStatus} value={surnameRegVal} helperText={errorText} onChange={(e)=>onChangeSurnameVal(e.target.value)}/>
                    <GreenInput type="date" label="Data urodzenia" error={errorStatus} helperText={errorText} value={dateOfBirthRegVal} onChange={(e)=>onChangeDateOfBirthVal(e.target.value)} fullWidth></GreenInput>

                </div>
                <div className='buttonsAccountMenu'>
                    <div className='accountFormButtons'>
                        <button className='main-btn accountFormButton' onClick={()=>onClickRegister()}>Zarejestruj</button>
                        <button className='main-btn accountFormButton main-btn-negative' onClick={()=>navigate("/login")}>Logowanie</button>
                    </div>
                </div>
        </div>
    )
}

export default function Register() {
    const navigate = useNavigate();
    return (
        <div className='logRegContent' style={{backgroundImage: 'url('+regBack+')'}}>
            <div className='accountFormContent'>
                <div className='accountFormHeader'>
                    <img className='logo' src={logo} onClick={()=>navigate("/")}/>
                </div>
                <RegisterController>
                    <RegisterFormView/>
                </RegisterController>
            </div>
        </div>
       
    )
}