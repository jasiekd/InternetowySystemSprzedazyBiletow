import ProfileTicket from "./ProfileTicket"
import { Pagination } from '@mui/material';
import React, { useEffect, useState } from "react";
export default function ProfileTicketsList({getMyTickets,choice})
{
    const [eventList,setEventList] = useState()
    const [pageCount,setPageCount] = React.useState(1);
    useEffect(()=>{
        getMyTickets(choice,pageCount,10).then(r=>{
            console.log(r);
            setEventList(r);
        })
    },[pageCount])
    return(
        <React.Fragment>
             {
                eventList?
                    eventList.value.tickets.map((val,key)=>{
                       console.log(val);
                        return(
                            <ProfileTicket
                            key={key}
                            printAble={true}
                            preview={true}
                            event={val}
                            imgURL={val.event.imgURL}
                            title={val.event.title}
                            price={val.cost}
                            seats={val.numberOfTickets}
                            date={val.event.date}
                            />
                        )
                        
                    })
                :
                    null     
            }
            {
                 eventList!==undefined?
                 <Pagination count={Math.ceil(eventList.value.totalCount/10)} size='large' onChange={(e,v)=>{setPageCount(v)}}/>
                 :
                 null
            }
        </React.Fragment>
    )
}