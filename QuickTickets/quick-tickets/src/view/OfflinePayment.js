import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import "../styles/OrganisersApproval.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import "../styles/DropDownMenu.css";
import "../styles/SearchList.css";
import Pagination from '@mui/material/Pagination';
import OrganiserApplicationController from "../controllers/OrganiserApplicationController";
import { useEffect, useState } from "react";

function ShowUnpaidTickets(){
    
    const [unpaidTickets,setUnpaidTickets] = useState([
        {description:'aha'},
        {description:'aha'},

    ]);
    const [pageIndex,setpageIndex] = useState(1);

    //     useEffect(  () => {
    //         getOrganiserApplication(pageIndex,3).then((result)=>{
    //             setUnpaidTickets(result);
    //         });
    //     },[pageIndex]);

    //     const onAcceptOrganiserApplication=(id)=>{
    //         acceptOrganiserApplication(id).then((result)=>{
    //             if(!result)
    //                 return;
    //             getOrganiserApplication(pageIndex,3).then((result)=>{        
    //                 setUnpaidTickets(result);
    //             });
    //         })
    //     }

    //     const onCancelOrganiserApplication=(id)=>{
    //         cancelOrganiserApplication(id).then((result)=>{
    //             if(!result)
    //                 return;
    //                 getOrganiserApplication(pageIndex,3).then((result)=>{
    //                     setUnpaidTickets(result);
    //                 }); 
    //     })
    // }

    return(
        <div className='searchList'>
            <div className='title'>Lista nieopłaconych biletów przez użytkowników</div>
                {
                        unpaidTickets &&
                        unpaidTickets?.map((val,index)=>{
                            return(
                                <div className='organisers-list' key={index}>
                                    <div className='organisers-list-row'>
                                        <h3>Dane:</h3>
                                    </div>
                                    <div className='organisers-list-row'>
                                        Jan Nowak
                                    </div>
                                    <div className='organisers-list-row'>
                                        04.01.1995
                                    </div>
                                    <div className='organisers-list-row'>
                                        jan.nowak@wpp.pl
                                    </div>
                                    <div className='organisers-list-row'>
                                        nazwa biletu
                                    </div>
                                    <div className='organisers-list-row'>
                                        200zł
                                    </div>
                                    <div className='organisers-list-row'>
                                        {/* <button className='main-btn2' onClick={()=>(onAcceptOrganiserApplication(val.id,index))}>Zatwierdź</button>
                                        <button className='cancel-btn' onClick={()=>(onCancelOrganiserApplication(val.id,index))}>Anuluj</button> */}
                                        <button className='main-btn2'>Opłać</button>
                                        <button className='cancel-btn'>Anuluj</button>
                                    </div>
                                </div>
                            )
                        })
                   
                }  
                {
                    unpaidTickets &&
                    <Pagination count={unpaidTickets?.value?.totalPages} size='large' onChange={(e,v)=>(setpageIndex(v))}/>
                }
        </div>
    );

}

export default function OfflinePayment(){
    return(
        <div className="App">
            <Header/>
            <main className='content'>
                <ShowUnpaidTickets />
            </main>
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>

    );
}