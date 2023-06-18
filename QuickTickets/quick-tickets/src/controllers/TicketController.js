import React from "react";
import Swal from "sweetalert2";
import TicketService from "../services/TicketService";
export default function TicketController({children}){

    const gateway = new TicketService();

    const getMyTickets = async(choice, pageIndex,pageSize) =>{
        const response = await gateway.getMyTickets(choice,pageIndex,pageSize);

        if(response.status === 200)
        {
            
            return response.data;
        }
        else
        {
            Swal.fire({
                position: 'error',
                icon: 'success',
                title: 'Błąd pobierania listy',
                showConfirmButton: true,
              })
        }
    }
    const getMyTicket = async(ticketID)=>{
        const response = await gateway.getMyTicket(ticketID);

        if(response.status === 200)
        {
            return response.data;
        }
        else
        {
         
        }
    }

    return React.cloneElement(children,{
        getMyTickets,
        getMyTicket
    })
}