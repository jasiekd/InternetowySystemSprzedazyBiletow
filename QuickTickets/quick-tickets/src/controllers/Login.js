import * as React from 'react';
import { useNavigate } from "react-router-dom";
import Swal from 'sweetalert2';
import axios from 'axios';
import AccountService from '../services/AccountService';
export function checkIsLogged(){
    if(localStorage.getItem("roleId")!=null) 
    {
        return localStorage.roleId;
    }
    return 0;
}
export function logOut(){
    localStorage.clear();
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
        const gateway = new AccountService();
        const response = await gateway.login(loginData);
        if(response.status === 404)
        {
            console.log(response)
            Swal.fire(
                'Błąd logowania',
                'Niepoprawne dane logowania',
                'error'
            )
            setErrorStatus(true);
            setErrorText("Niepoprawne dane logowania");
        }
        else if(response.status === 200){
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
        const gateway = new AccountService();
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
        else
        {
            Swal.fire(
                'Błąd logowania',
                'Problem z zalogowaniem przez Google',
                'error'
            )
        }
    }

    const getUser = async()=>{
        const gateway = new AccountService();
        const response = await gateway.getUser();


        if(response.status === 200)
        {
            return response.data;
        }
        else 
        {
           
        }
    }
    const updateAccount = async(data,formRegex) =>{
        const gateway = new AccountService();
        if(data.name !== ""&&data.surname !== "" && data.email !== ""&&
            data.login!==""&&data.password!==""&& data.dateOfBirth!==""&&
            formRegex.emailAlert === false && formRegex.loginAlert === false && formRegex.passwordAlert === false)
        {
            const response = await gateway.updateAccount(data);


            if(response.status === 200)
            {
                Swal.fire({
                    icon: 'success',
                    title: 'Zapisano zmiany',
                    showConfirmButton: false,
                    timer: 1500
                })
                navigate("/home");
            }
            else 
            {
                Swal.fire({
                    icon: 'error',
                    title: 'Błąd zapisu zmian',
                    showConfirmButton: true,
                })
            }
        }
        else{
            Swal.fire({
                icon: 'error',
                title: 'Błąd zapisu zmian',
                text: "Nie wszystkie pola zawierają poprawną wartość lub są puste",
                showConfirmButton: true,
            })
        }
       
    }
    const GetListOfUsers = async(pageIndex,pageSize) =>{
        const gateway = new AccountService();
        
            const response = await gateway.GetListOfUsers(pageIndex,pageSize);
            console.log('wtf',response);

            if(response.status === 200)
            {
                
                return response.data;
            }
            else 
            {
                Swal.fire(
                    'Błąd pobrania użytkowników',
                    'error'
                )
            }
        
       
       
    }
    return React.cloneElement(children,{
        onLogin: login,
        onLoginWithGoogle: loginWithGoogle,
        checkIsLogged: checkIsLogged,
        getUser,
        updateAccount,
        GetListOfUsers
    })

}
