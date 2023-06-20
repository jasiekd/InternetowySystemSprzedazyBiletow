import ProfileTicket from "./ProfileTicket"
import { Pagination } from '@mui/material';
import React, { useEffect, useState } from "react";
import Alert from '@mui/material/Alert';

export default function ProfileTicketsList({getMyTickets,choice,printAble})
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
            <h1>Lista biletów</h1>
             {
                eventList&&eventList.value.totalCount>0?
                    eventList.value.tickets.map((val,key)=>{
                       console.log(val);
                        return(
                            <ProfileTicket
                            key={key}
                            printAble={printAble}
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
                <Alert variant="filled" severity="info">
                 Brak biletów
                </Alert>  
            }
            {
                 eventList&&eventList.value.totalCount>0?
                 <Pagination count={Math.ceil(eventList.value.totalCount/10)} size='large' onChange={(e,v)=>{setPageCount(v)}}/>
                 :
                 null
            }
        </React.Fragment>
    )
}