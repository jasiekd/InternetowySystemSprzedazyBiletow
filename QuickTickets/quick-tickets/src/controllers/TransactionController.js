import TransactionService from "../services/TransactionService";
import React from "react";
import { useNavigate } from "react-router-dom";
import Swal from 'sweetalert2';


export default function TransactionController({children}){
    
    const gateway = new TransactionService();
    const navigate = useNavigate();
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
    const GetAllTransactions = async(pageIndex,pageSize)=>{
        const response = await gateway.GetAllTransactions(pageIndex,pageSize);
        if(response.status === 200)
        {
            return response.data;
        }
    }
    const GetStatusTransaction = async(transactionID)=>{
        const response = await gateway.GetStatusTransaction(transactionID);

        if(response.status !== 200 ||response.data.transactionStatus!=="Paid"){
            Swal.fire({
                position: 'center',
                icon: 'error',
                title: 'Błąd płatności',
                text: "Kupowany bilet nie został opłacony, skontaktuj sie z administracją w celu opłacenia go.",
                showConfirmButton: true,
            }).then(()=>{
                navigate("/home");
            })
            return false
        }
        else
        {
            return true;
        }
        
    }

    return React.cloneElement(children,{
        createTransaction,
        GetPendingTransactions,
        AcceptTransaction,
        CancelTransaction,
        GetAllTransactions,
        GetStatusTransaction,
    })
}