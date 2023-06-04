import React from "react";
import OrganiserApplicationService from "../services/OrganiserApplicationService";
import Swal from "sweetalert2";

export default function OrganiserApplicationController({children}){
    const gateway = new OrganiserApplicationService();

     const addOrganiserApplication = async(description) =>{

        const response = await gateway.addOrganiserApplication(localStorage.uId,description)

        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Wysłano aplikacje o zostanie organizatorem',
                showConfirmButton: false,
                timer: 1500
            })
            return true;
        }
        else{
            Swal.fire(
                'Błąd podczas wysyłania aplikacji o zostanie organizatorem',
                'error'
            )
            return false;
        }
    }
    
    const getOrganiserApplication = async(pageIndex,pageSize) =>{
        const response = await gateway.getOrganiserApplication(pageIndex,pageSize);

        if(response.status === 200)
        {
            return response.data;
        }

    }

    const acceptOrganiserApplication = async(id) =>{
        const response = await gateway.acceptOrganiserApplication(id);

        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Zaakceptowano nowego organizatora',
                showConfirmButton: false,
                timer: 1500
            })
            return true;
        }else{
            Swal.fire(
                'Nie udało się zaakceptować organizatora',
                'error'
            )
            return false;
        }

    }
    const cancelOrganiserApplication = async(id) =>{
        const response = await gateway.cancelOrganiserApplication(id);

        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Anulowano prośbę o zostanie organizatorem',
                showConfirmButton: false,
                timer: 1500
            })
            return true;
        }else{
            Swal.fire(
                'Nie udało się anulować prośby o zostanie organizatorem',
                'error'
            )
            return false;
        }

    }

    return React.cloneElement(children,{
        addOrganiserApplication,
        getOrganiserApplication,
        acceptOrganiserApplication,
        cancelOrganiserApplication,
    })

}