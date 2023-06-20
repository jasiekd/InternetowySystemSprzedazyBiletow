import React, { useState } from 'react';
import EventsService from '../services/Events';
import Swal from 'sweetalert2';
import { useNavigate } from 'react-router-dom';

export const EventStatus = {
    Pending: "Pending",
    Cancelled: "Cancelled",
    Confirmed: "Confirmed",
    Paid: "Paid",
    Unpaid: "Unpaid",
    AdminOffPaid: "AdminOffPaid"
  };

export default function EventsController({children}){

    const gateway = new EventsService();

    const addEvent = async(eventData) => {

        const response = await gateway.addEvent(eventData);
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

    const getEvent = async(eventId) =>{
      
        const response = await gateway.getEvent(eventId);
        if(response.status === 200)
        {
            return response.data;
        }
        else{
            Swal.fire(
                'Błąd pobierania wydarzenia',
                'Podczas pobierania wydarzenia pojawił się problem',
                'error'
            )
           
        }
    }

    const getPendingEvents = async(pageIndex,pageSize) =>{
        const response = await gateway.getPendingEvents(pageIndex,pageSize);

        if(response.status === 200)
        {
            return response.data.value
        }else{
            
        }
    }

    const acceptEvent = async(id) =>{
        const response = await gateway.acceptEvent(id);

        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Wydarzenie zakceptowane',
                showConfirmButton: false,
                timer: 1500
            })
            return true;
        }
        else{
            Swal.fire(
                'Błąd akceptowania wydarzenia',
                'Podczas akceptowania wydarzenia pojawił się problem',
                'error'
            )
            return false;
        }
    }

    const cancleEvent = async(id) =>{
        const response = await gateway.cancelEvent(id);
       
        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Wydarzenie odrzucone',
                showConfirmButton: false,
                timer: 1500
            })
            return true;
        }
        else{
            Swal.fire(
                'Błąd odrzucania wydarzenia',
                'Podczas odrzucania wydarzenia pojawił się problem',
                'error'
            )
            return false;
        }
    }

    const getOrganisatorEvents = async(pageIndex,pageSize,status) =>{
        const response = await gateway.getOrganisatorEvents(pageIndex,pageSize,status);

        if(response.status === 200)
        {
            return response.data;
        }
    }

    const updateEvent = async(eventID,title,seats,ticketPrice,description,isActive,adultsOnly,typeID,locationID,imgURL)=>{
        const response = await gateway.updateEvent(eventID,title,seats,ticketPrice,description,isActive,adultsOnly,typeID,locationID,imgURL);

        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Zmiany zostały zapisane',
                showConfirmButton: false,
                timer: 1500
            })
        }
        else{
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Błąd zapisu zmian',
                showConfirmButton: true,
            })
        }
    }
    const canBuyTicket = async(eventID) => {
        const response = await gateway.canBuyTicket(eventID);
            let retVal;
        if(response.status === 200)
        {
            retVal = true;
        }else{
            retVal = false;
        }
        return retVal;
    }
    return React.cloneElement(children,{
        onAddEvent:addEvent,
        getHotEvents,
        getHotLocations,
        getTypesOfEvents,
        getEventLocations,
        search,
        getEvent,
        getPendingEvents,
        acceptEvent,
        cancleEvent,
        getOrganisatorEvents,
        updateEvent,
        canBuyTicket
    })
}