import React, { useEffect, useState } from "react";
import '../styles/ChooseTicket.css';
import '../styles/FillInData.css';
import { GreenInput } from "./GreenInput";
import EventInfo from "./EventInfo";
import moment from "moment";
export default function FillInData({eventData,getUser}){
    const [userData,setUserData] = useState(null);
    useEffect(()=>{
        getUser().then(r=>{
            setUserData(r);
        })
    },[])
    return(
        <div className="choose-ticket">
        <div className="buy-form">
             <div className="buy-title">Dane użytkownika</div>
             <div className="buy-info-form">
                {
                    userData?
                    <React.Fragment>
                        <span><h3>Imie: </h3>{userData.name}</span>
                        <span><h3>Nazwisko: </h3>{userData.surname}</span>
                        <span><h3>Email: </h3>{userData.email}</span>
                        <span><h3>Data urodzenia: </h3>{userData.dateOfBirth}</span>
                    </React.Fragment>
                   
                    :
                    null
                }
             </div>   
        </div>
        <EventInfo eventData={eventData}/>
     </div>
    )
}