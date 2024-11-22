import React, { useState } from 'react';
import UserEventHistoryService from '../services/UserEventHistoryService';
import Swal from 'sweetalert2';

export default function UserEventHistoryController({children}){

    const gateway = new UserEventHistoryService();

    const getPredictedEvents = async(eventID,label) => {
        const response = await gateway.getPredictedEvents(eventID, label);

        if(response.status === 200) return response.data;
        
        
    }
    return React.cloneElement(children,{
        getPredictedEvents
    })
}