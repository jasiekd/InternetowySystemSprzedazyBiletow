
import { useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import React from "react";
import AccountService from "../services/AccountService";
export default function RegisterController({children}){
    const navigate = useNavigate();

    const register = async (registerData,setErrorStatus,setErrorText)=>{

        
        const gateway = new AccountService();
        const response = await gateway.register(registerData);

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