import React from "react";
import '../styles/ChooseTicket.css';
import { GreenInput } from "./GreenInput";
import exampleEvent from "../images/example-event.png";
import EventInfo from "./EventInfo";
export default function ChooseTicket({counter,setCounter}){
    return(
        <div className="choose-ticket">
           <div className="buy-form">
                <div className="buy-title">Wybierz ile biletów chcesz kupić</div>
                <GreenInput label="Ilość biletów" fullWidth type="number" value={counter} onChange={(e)=>setCounter(e.target.value)}></GreenInput>
                <div className="price-sum">Suma kosztów: {counter*80} PLN</div>
           </div>
           <EventInfo/>
        </div>
    )
}