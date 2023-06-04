import React from "react";
import '../styles/ChooseTicket.css';
import moment from "moment";
export default function EventInfo({eventData}){
    return(
        <div className="buy-event-info">
            {
                eventData?
                <>
                    <div className="buy-title">{eventData.title}</div>
                    <div>{moment(eventData.date).format("DD-MM-YYYY")} {eventData.location.name}</div>
                    <img src={eventData.imgURL}/>
                </>
                :
                null
            }
            
        </div>
    )
}