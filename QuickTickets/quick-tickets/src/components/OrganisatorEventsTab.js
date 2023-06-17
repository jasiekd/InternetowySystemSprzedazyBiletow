import { useEffect } from "react"
import { EventStatus } from "../controllers/Events"
import * as React from 'react';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import ProfileTicket from "./ProfileTicket";
import Pagination from '@mui/material/Pagination';
export default function OrganisatorEventsTab({getOrganisatorEvents}){
    const [status, setStatus] = React.useState('');
    const [eventsInfo,setEventInfos] = React.useState();
    const [pageCount,setPageCount] = React.useState(1);
  const handleChange = (event) => {
    setStatus(event.target.value);
  };
    useEffect(()=>{
        getOrganisatorEvents(pageCount,10,status).then((r)=>{
            console.log(r)
            setEventInfos(r)
        })
    },[status,pageCount])
    return (
        <div><Box sx={{ minWidth: 600,paddingBottom:"2rem"}}>
            Wybierz status wydarzenia
        <FormControl fullWidth>
          <InputLabel id="demo-simple-select-label" >Status wydarzenia</InputLabel>
          <Select
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            value={status}
            label="Status wydarzenia"
            onChange={handleChange}
          >
            <MenuItem value={"Confirmed"}>Zatwierdzone</MenuItem>
            <MenuItem value={"Pending"}>Wysłane do weryfikacji</MenuItem>
            <MenuItem value={"Cancelled"}>Anulowane</MenuItem>
          </Select>
        </FormControl>
      </Box>
        {
            eventsInfo?
            eventsInfo.value.events.map((val,key)=>{
                return(
                    <ProfileTicket
                        key={key}
                        imgURL={val.imgURL}
                        date={val.date}
                        location={val.location.name}
                        price={val.ticketPrice}
                        title={val.title}
                        editAble={true}
                        editBlocked={status!=="Pending"?true:false}
                        event={val}
                        preview={true}
                        seats={val.seats}
                    />
                )
            })
                
            :
            null
        }
        {
            eventsInfo?
            <Pagination count={Math.ceil(eventsInfo.value.totalCount/10)} size='large' onChange={(e,v)=>{setPageCount(v)}}/>
            :
            null
        }
        </div>
    )
}