import React from "react";
import '../styles/ChooseTicket.css';
import exampleEvent from "../images/example-event.png";
export default function EventInfo(){
    return(
        <div className="buy-event-info">
            <div className="buy-title">Lorem ipsum nazwa</div>
            <div>12.12.2023 Kielce</div>
            <img src={exampleEvent}/>
        </div>
    )
}