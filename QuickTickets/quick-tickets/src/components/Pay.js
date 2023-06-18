import React from "react";
import { GreenInput } from "./GreenInput";
import EventInfo from "./EventInfo";
import '../styles/ChooseTicket.css';
import '../styles/Pay.css';
import { useNavigate } from "react-router-dom";
export default function Pay({counter,eventData,createTransaction}){
    const navigate = useNavigate()
    const[price,setPrice] = React.useState(80);
    //cost,desc,eventID,numberOfTickets
    const onClickPay =() =>{
        createTransaction((counter+1)*eventData.ticketPrice,"opis",eventData.eventID,(counter+1)).then(r=>{
            console.log(r);
            window.location.href = r;
        })
       // console.log(eventData)
    }
    return(
        <div className="choose-ticket">
        <div className="buy-form">
            {
                eventData?
                <>
                    <div className="buy-title">Informacje o płatności</div>
                        <table class="pay-summart-table">
                            <thead>
                                <tr>
                                <th>Ilość bilrtów</th>
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
                        <div className="pay-status">Status płatności: Nieopłacone</div>
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