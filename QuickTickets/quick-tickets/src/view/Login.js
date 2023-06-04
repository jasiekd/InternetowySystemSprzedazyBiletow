import * as React from 'react';
import "../styles/MainStyle.css";
import logBack from "../images/logBack.jpg";
import "../styles/LogReg.css";
import logo from "../images/logo.png";
import { GreenInput } from '../components/GreenInput';
import { useNavigate } from "react-router-dom";
import { GoogleLogin } from '@react-oauth/google';
import LoginController from '../controllers/Login';
import Swal from 'sweetalert2';

function LoginFormView({onLogin,onLoginWithGoogle}){
    
    const [loginVal,setLoginVal] = React.useState("");
    const [passwordVal, setPasswordVal] = React.useState("");
    
    const [errorStatus, setErrorStatus] = React.useState(false);
    const [errorText, setErrorText] = React.useState("");

    const navigate = useNavigate();

    const onClickLogin = () =>{
        onLogin({loginVal,passwordVal},setErrorStatus,setErrorText)
    }
    const onClickGoogle = (credentialResponse) =>{
        onLoginWithGoogle(credentialResponse);
    }
    const onChangeLoginVal = (val) =>{
        setLoginVal(val);
        setErrorStatus(false);
        setErrorText("");
    }
    const onChangePasswordVal = (val) =>{
        setPasswordVal(val);
        setErrorStatus(false);
        setErrorText("");
    }

    return(
        <div>
                <div className='accountFormInputs'>
                    <GreenInput label="Login" error={errorStatus} helperText={errorText} value={loginVal} onChange={(e)=>onChangeLoginVal(e.target.value)}/>
                    <GreenInput label="Hasło" error={errorStatus} type="password" helperText={errorText} value={passwordVal} onChange={(e)=>onChangePasswordVal(e.target.value)}/>
                </div>
                <div className='buttonsAccountMenu'>
                    <GoogleLogin
                        width= "1000px"
                   
                        onSuccess={credentialResponse => {
                            onClickGoogle(credentialResponse);
                            
                        }}
                    
                        onError={() => {
                            Swal.fire(
                                'Błąd logowania',
                                'Logowanie nieudane',
                                'error'
                            )
                        }}
                    />
                    <div className='accountFormButtons'>
                        <button className='main-btn accountFormButton' onClick={()=>onClickLogin()}>Zaloguj</button>
                        <button className='main-btn accountFormButton main-btn-negative' onClick={()=>navigate("/register")}>Rejestracja</button>
                    </div>
                </div>
        </div>
    )
}
export default function Login(){
    const navigate = useNavigate();

    return (
        <div className='logRegContent' style={{backgroundImage: 'url('+logBack+')'}}>
            <div className='accountFormContent'>
                <div className='accountFormHeader'>
                    <img className='logo' src={logo} onClick={()=>navigate("/")}/>
                </div>
                <LoginController>
                    <LoginFormView/>
                </LoginController>
            </div>
        </div>
    )
}