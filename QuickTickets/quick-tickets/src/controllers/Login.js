import * as React from 'react';
import { useNavigate } from "react-router-dom";
import Swal from 'sweetalert2';
import LoginService from '../services/Login';
import axios from 'axios';
export function checkIsLogged(){
    if(localStorage.getItem("accessToken")!=null) 
    {
        return true;
    }
    return false;
}
export function logOut(){
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
    axios.defaults.headers.common['Authorization'] = `Bearer ${null}`;
    Swal.fire({
        position: 'center',
        icon: 'success',
        title: 'Wylogowano',
        showConfirmButton: false,
        timer: 1500
    })
}

export default function LoginController({children}){
    const navigate = useNavigate();



    const login = async (loginData,setErrorStatus,setErrorText)=>{
        const gateway = new LoginService();
        const response = await gateway.login(loginData);

        if(response.status === 401)
        {
            Swal.fire(
                'Błąd logowania',
                'Niepoprawne dane logowania',
                'error'
            )
            setErrorStatus(true);
            setErrorText("Niepoprawne dane logowania");
        }
        else if(200){
            Swal.fire({
                icon: 'success',
                title: 'Zalogowane poprawnie',
                showConfirmButton: false,
                timer: 1500
            })
            navigate("/home");
        }
        
       
    }
    const loginWithGoogle = async(credentialResponse)=>{
        const gateway = new LoginService();
        const response = await gateway.loginWithGoogle(credentialResponse);

        if(response.status === 200)
        {
            Swal.fire({
                icon: 'success',
                title: 'Zalogowane poprawnie',
                showConfirmButton: false,
                timer: 1500
            })
            navigate("/home");
        }
        else if(response.status === 401)
        {
            Swal.fire(
                'Błąd logowania',
                'Problem z zalogowaniem przez Google',
                'error'
            )
        }
    }
    return React.cloneElement(children,{
        onLogin: login,
        onLoginWithGoogle: loginWithGoogle,
        checkIsLogged: checkIsLogged
    })

}
