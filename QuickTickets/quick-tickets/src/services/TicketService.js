import axios from "axios";
import { HostName } from "./HostName";
axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;
export default class TicketService{
    async getMyTickets(choice, pageIndex,pageSize){
        try{
            const response = axios.post(HostName+"/api/Ticket/GetMyTickets?choice="+choice,
            {
                pageIndex: pageIndex,
                pageSize: pageSize
            });
            return response;
        }catch(error){
            return error.response
        }
    }
    async getMyTicket(ticketID){
        try{
            const response = axios.post(HostName+"/api/Ticket/GetMyTicket?ticketID="+ticketID);
            return response;
        }catch(error){
            return error.response
        }
    }
    async getMyTicketByID(transactionID){
        try{
            const response = axios.post(HostName+"/api/Ticket/GetMyTicketForTransactionID?transactionID="+transactionID);
            return response;
        }catch(error){
            return error.response
        }
    }
}