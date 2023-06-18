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
    async GetPendingTransactions(pageIndex,pageSize){
        try{
            const response = await axios.post(HostName+"/api/Transaction/GetPendingTransactions",{
                pageIndex: pageIndex,
                pageSize: pageSize
            })
            return response;
        }catch(error){
            return error.response;
        }
    }
    async AcceptTransaction(transactionID){
        try{
            const response = await axios.put(HostName+"/api/Transaction/AcceptTransaction?transactionID=" + transactionID,{})
            return response;
        }catch(error){
            return error.response;
        }
    }
    async CancelTransaction(transactionID){
        try{
            const response = await axios.put(HostName+"/api/Transaction/CancelTransaction?transactionID=" + transactionID,{})
            return response;
        }catch(error){
            return error.response;
        }
    }

}
