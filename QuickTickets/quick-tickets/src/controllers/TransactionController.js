import TransactionService from "../services/TransactionService";
import Swal from 'sweetalert2';
import * as React from 'react';


export default function TransactionController({children}){
    
    const gateway = new TransactionService();

    const createTransaction = async(cost,desc,eventID,numberOfTickets)=>{
        const response = await gateway.createTransaction(cost,desc,eventID,numberOfTickets);

        if(response.status === 200)
        {
            return response.data;
        }

    }
    const GetPendingTransactions = async(pageIndex,pageSize)=>{
        const response = await gateway.GetPendingTransactions(pageIndex,pageSize);
        if(response.status === 200)
        {
            return response.data;
        }

    }
    const AcceptTransaction = async(transactionID)=>{
        const response = await gateway.AcceptTransaction(transactionID);

        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Opłacono',
                showConfirmButton: false,
                timer: 1500
            })
            return response;
        }else{
            Swal.fire(
                'Błąd - nie udało się opłacić',
                'Podczas opłacania pojawił się problem',
                'error'
            )
        }

    }
    const CancelTransaction = async(transactionID)=>{
        const response = await gateway.CancelTransaction(transactionID);

        if(response.status === 200)
        {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Anulowano pomyślnie',
                showConfirmButton: false,
                timer: 1500
            })
            return response;
        }else{
            Swal.fire(
                'Błąd - nie udało się anulować',
                'Podczas anulowania pojawił się problem',
                'error'
            )
        }

    }

    return React.cloneElement(children,{
        createTransaction,
        GetPendingTransactions,
        AcceptTransaction,
        CancelTransaction,
    })
}