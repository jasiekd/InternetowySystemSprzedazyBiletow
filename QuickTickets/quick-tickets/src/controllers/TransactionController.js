import TransactionService from "../services/TransactionService";
import React from "react";

export default function TransactionController({children}){
    
    const gateway = new TransactionService();

    const createTransaction = async(cost,desc,eventID,numberOfTickets)=>{
        const response = await gateway.createTransaction(cost,desc,eventID,numberOfTickets);

        if(response.status ===200)
        {
            return response.data;
        }

    }

    return React.cloneElement(children,{
        createTransaction
    })
}