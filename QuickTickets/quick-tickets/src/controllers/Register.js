import RegisterService from "../services/Register";
import { useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import React from "react";
export default function RegisterController({children}){
    const navigate = useNavigate();

    const register = async (registerData,setErrorStatus,setErrorText)=>{
        console.log(registerData);
        
        const gateway = new RegisterService();
        const response = await gateway.register(registerData);
        console.log(response);
        if(response.status === 201)
        {
            Swal.fire({
                icon: 'success',
                title: 'Zarejestrowano poprawnie',
                showConfirmButton: false,
                timer: 1500
            })
            navigate("/login");
        }
        else if(response.status === 500)
        {
            Swal.fire(
                'Błąd rejestracji',
                'Konto o podanym adresie email lub loginie juz istnieje',
                'error'
            )
            setErrorStatus(true);
            setErrorText("Email lub login jest już zjęty")
        }
        else if(response.status === 400)
        {
            Swal.fire(
                'Błąd rejestracji',
                'Niepoprawne dane rejestracji',
                'error'
            )
        }
    }
    return React.cloneElement(children,{
        onRegister: register
    })
}