import React from "react";
import TypesOfEventsService from "../services/TypesOfEventsService";
import Swal from "sweetalert2";
export default function TypesOfEventsController({children}){
    const gateway = new TypesOfEventsService();

    const addCategory = async(typeID,description)=>{
        const response = await gateway.addCategory(typeID,description);
        console.log(response);
        if(response.status === 201)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Dodano kategorie',
                showConfirmButton: false,
                timer: 1500
            })
            return true;
        }
        else{
            Swal.fire(
                'Błąd dodawania kategorii',
                'Podczas dodawania katrgorii pojawił się problem',
                'error'
            )
            return false;
        }
    }

    return React.cloneElement(children,{
        addCategory
    })
}