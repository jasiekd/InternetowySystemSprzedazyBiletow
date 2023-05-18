import React from 'react';
import EventsService from '../services/Events';

export default function EventsController({children}){

    const gateway = new EventsService();

    const addEvent = async(eventData) => {
        const response = await gateway.addEvent(eventData);

        if(response.status === 200)
        {
            return response.data;
        }
        else{

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

    return React.cloneElement(children,{
        onAddEvent:addEvent,
        getHotEvents,
        getHotLocations
    })
}