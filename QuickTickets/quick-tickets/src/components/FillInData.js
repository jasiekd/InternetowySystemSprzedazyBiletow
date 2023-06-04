import React from "react";
import '../styles/ChooseTicket.css';
import '../styles/FillInData.css';
import { GreenInput } from "./GreenInput";
import exampleEvent from "../images/example-event.png";
import EventInfo from "./EventInfo";
export default function FillInData({eventData}){
    return(
        <div className="choose-ticket">
        <div className="buy-form">
             <div className="buy-title">Dane użytkownika</div>
             <div className="buy-info-form">
                <GreenInput label="Imię" fullWidth type="text" ></GreenInput>
                <GreenInput label="Nazwisko" fullWidth type="text" ></GreenInput>
                <GreenInput label="Adres" fullWidth type="text" ></GreenInput>
                <GreenInput label="Numer telefonu" fullWidth type="tel" ></GreenInput>
                <GreenInput label="Email" fullWidth type="email" ></GreenInput>
             </div>   
        </div>
        <EventInfo eventData={eventData}/>
     </div>
    )
}