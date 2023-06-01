import React from 'react';
import EventsService from '../services/Events';
import Swal from 'sweetalert2';

export default function EventsController({children}){

    const gateway = new EventsService();

    const addEvent = async(eventData) => {
        console.log(eventData);
        const response = await gateway.addEvent(eventData);
        console.log(response);
        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Dodano wydarzenie',
                showConfirmButton: false,
                timer: 1500
            })
            return response.data;
        }
        else{
            Swal.fire(
                'Błąd dodawania wydarzenia',
                'Podczas dodawania wydarzenia pojawił się problem',
                'error'
            )
        }
    }

    const getHotEvents = async() => {
        const response = await gateway.getHotEvents();

        if(response.status === 200)
        {
            return response.data;
        }
        else{

        }
    }

    const getHotLocations = async() => {
        const response = await gateway.getHotLocations();

        if(response.status === 200)
        {
            return response.data;
        }
        else{

        }
    }

    const getTypesOfEvents = async() => {
        const response = await gateway.getTypesOfEvents();

        if(response.status == 200)
        {
            return response.data;
        }else{

        }
    }
    const getEventLocations = async() => {
        const response = await gateway.getEventLocations();

        if(response.status === 200)
        {
            return response.data;
        }else{

        }
    }

    const search = async(searchPhrase,minPrice,maxPrice,startDate,endDate,locationId,typeId,pageIndex,pageSize) =>{
        const response = await gateway.search(searchPhrase,minPrice,maxPrice,startDate,endDate,locationId,typeId,pageIndex,pageSize);

        if(response.status === 200)
        {
            return response.data
        }
        else{

        }
    } 

    return React.cloneElement(children,{
        onAddEvent:addEvent,
        getHotEvents,
        getHotLocations,
        getTypesOfEvents,
        getEventLocations,
        search,
    })
}