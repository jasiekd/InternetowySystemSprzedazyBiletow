import axios from "axios";
import { HostName } from "./HostName";
axios.defaults.headers.common['Authorization'] = `Bearer ${localStorage.getItem('accessToken')}`;

export default class TransactionService{
    async createTransaction(cost,desc,eventID,numberOfTickets){
        try{
            const response = axios.post(HostName+"/api/Transaction/CreateTransaction",
            {
                cost:cost,
                desc:desc,
                eventID:eventID,
                numberOfTickets:numberOfTickets
            });
            return response;
        }catch(error){
            return error.response
        }
    }
   
}
