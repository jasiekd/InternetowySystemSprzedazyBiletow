import React from "react";
import { GreenInput } from "./GreenInput";
import EventInfo from "./EventInfo";
import '../styles/ChooseTicket.css';
import '../styles/Pay.css';

export default function Pay({counter,eventData,createTransaction}){
    const onClickPay =() =>{
        createTransaction(counter*eventData.ticketPrice,"opis",eventData.eventID,counter).then(r=>{
            window.location.href = r;
        })
    }
    return(
        <div className="choose-ticket">
        <div className="buy-form">
            {
                eventData?
                <>
                    <div className="buy-title">Informacje o płatności</div>
                        <table className="pay-summart-table">
                            <thead>
                                <tr>
                                <th>Ilość biletów</th>
                                <th>Cena</th>
                                <th>Suma</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                <td>{counter}</td>
                                <td>{eventData.ticketPrice}</td>
                                <td>{counter*eventData.ticketPrice}</td>
                                </tr>
                            </tbody>
                        </table>
                        <br/>
                        <br/>
                        <br/>
                        <button className="main-btn" onClick={()=>onClickPay()}>Zapłać online</button>
                </>
                :
                null
            }
             
        </div>
        <EventInfo eventData={eventData}/>
     </div>
    )
}