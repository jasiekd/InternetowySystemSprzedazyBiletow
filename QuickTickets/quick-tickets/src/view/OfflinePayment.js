import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import "../styles/OrganisersApproval.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import "../styles/DropDownMenu.css";
import "../styles/SearchList.css";
import Pagination from '@mui/material/Pagination';
import TransactionController from "../controllers/TransactionController";
import { useEffect, useState } from "react";
import ReCAPTCHA from "react-google-recaptcha";
import moment from 'moment/moment';
function ShowUnpaidTickets({GetPendingTransactions,AcceptTransaction,CancelTransaction}){
    
    const [unpaidTickets,setUnpaidTickets] = useState([]);
    const [pageIndex,setpageIndex] = useState(1);

        useEffect(  () => {

            GetPendingTransactions(pageIndex,3).then((result)=>{
                setUnpaidTickets(result);
            });
          

        },[pageIndex]);

   
    const [captcha, setCaptcha] = useState({});

    const showCaptcha = (transactionID) => {
        setCaptcha((prevState) => {
            const newCaptcha = {};
      
            // Zchowaj wszystkie otwarte captche
            Object.keys(prevState).forEach((key) => {
                newCaptcha[key] = false;
            });
            
            // Pokaż captche
            if(transactionID)
            newCaptcha[transactionID] = !prevState[transactionID];
      
            return newCaptcha;
          });

    };
    const payOffline = (transactionID,value) => {
        if(!value) return;
        AcceptTransaction(transactionID).then(()=>{
            GetPendingTransactions(pageIndex,3).then((result)=>{
                setUnpaidTickets(result);
                if(pageIndex>result.totalPages){
                    setpageIndex(result.totalPages);
                }
            });
        })

    };
    const cancelUnpaidTicket = (transactionID) => {
        CancelTransaction(transactionID).then(()=>{
            GetPendingTransactions(pageIndex,3).then((result)=>{
                setUnpaidTickets(result);
                if(pageIndex>result.totalPages){
                    setpageIndex(result.totalPages);
                }
            });
        })

    };
    return(
        <div className='searchList'>                                   

            <div className='title'>Lista nieopłaconych biletów przez użytkowników</div>
                {       unpaidTickets!== undefined &&
                        unpaidTickets.transactions?.map((transaction,index)=>(

                                <div className='organisers-list' key={index}>
                                    <div className='organisers-list-row'>
                                        <h3>Dane:</h3>
                                    </div>
                                    <div className='organisers-list-row'>
                                       E-mail: {transaction.user.email}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Login użytkownika: {transaction.user.login}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Imię i nazwisko: {transaction.user.name + " " + transaction.user.surname}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Data urodzenia: { moment(transaction.user.dateOfBirth).format('MMMM Do YYYY, h:mm:ss a')}
                                    </div>
                                    
                                    <div className='organisers-list-row'>
                                        Cena: {transaction.price} zł
                                    </div>
                                    <div className='organisers-list-row'>
                                        Data transakcji: {moment(transaction.transactionDate).format('MMMM Do YYYY, h:mm:ss a')}
                                    </div>
                                    <div className='organisers-list-row'>
                                        <button className='main-btn2' onClick={()=>showCaptcha(transaction.transactionID)}>Opłać</button >
                                        <button className='cancel-btn' onClick={()=>cancelUnpaidTicket(transaction.transactionID)}>Anuluj</button>
                                    </div>
                                    {captcha[transaction.transactionID] && <ReCAPTCHA sitekey='6LcxKagmAAAAAMoALPGOnG9rmQwJRCNVpdhRQvfm' onChange={(value)=>payOffline(transaction.transactionID,value)}/>}
                                </div>
                            
                        ))
                   
                }  
                {
                    unpaidTickets!== undefined && unpaidTickets.totalPages>0 &&
                    <Pagination count={unpaidTickets?.totalPages} size='large' onChange={(e,v)=>{setpageIndex(v);showCaptcha()}}/>
                }
        </div>
    );

}

export default function OfflinePayment(){
    return(
        <div className="App">
            <Header/>
            <main className='content'>
                <TransactionController>
                    <ShowUnpaidTickets />
                </TransactionController>
            </main>
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>

    );
}