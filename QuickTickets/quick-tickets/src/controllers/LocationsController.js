import React from "react";
import LocationsService from "../services/LocationsService";
import Swal from "sweetalert2";
export default function LocationsController({children}){
    const gateway = new LocationsService();

    const addLocations = async(locationID,name,description,imgUrl) =>{
        const response = await gateway.addLocation(locationID,name,description,imgUrl)
        
        if(response.status === 200||response.status === 201)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Dodano lokalizacje',
                showConfirmButton: false,
                timer: 1500
            })
            return true;
        }
        else{
            Swal.fire(
                'Błąd dodawania lokalizacji',
                'Podczas dodawania lokalizacji pojawił się problem',
                'error'
            )
            return false;
        }
    }
    
    const getLocation = async(id) =>{
        const response = await gateway.getLocation(id);

        if(response.status === 200)
        {
            return response.data;
        }

    }

    return React.cloneElement(children,{
        addLocations,
        getLocation
    })

}