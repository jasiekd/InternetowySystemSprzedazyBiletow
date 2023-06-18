import React, { useState } from "react";
import '../styles/ChooseTicket.css';
import { GreenInput } from "./GreenInput";
import exampleEvent from "../images/example-event.png";
import EventInfo from "./EventInfo";
export default function ChooseTicket({counter,setCounter,eventData}){
    const [isError,setIsError] = useState(false);
    const [helper,setHelper] = useState("")
    const onChangeCount = (value) =>{
        if(value>(eventData.availableSeats))
        {
            setIsError(true);
            setHelper("Maksymalna liczba biletów to "+(eventData.availableSeats))
        }
        else if(value<1)
        {
            setIsError(true);
            setHelper("Minimalna liczba biletów to 1")
        }
        else{
            setHelper("")
            setIsError(false);
            setCounter(value)
        }
        
    }

    return(
        <div className="choose-ticket">
           <div className="buy-form">
                {
                    eventData?
                    <>
                        <div className="buy-title">Wybierz ile biletów chcesz kupić</div>
                        <GreenInput label="Ilość biletów" error={isError} helperText={helper} fullWidth type="number" value={counter} onChange={(e)=>onChangeCount(e.target.value)}></GreenInput>
                        <div className="price-sum">Suma kosztów: {counter*eventData.ticketPrice} PLN</div>
                    </>
                    :
                    null
                }
                
           </div>
           <EventInfo eventData={eventData}/>
        </div>
    )
}