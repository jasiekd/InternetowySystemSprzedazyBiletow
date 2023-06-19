import * as React from 'react';
import Footer from '../components/Footer';
import exampleEvent from "../images/example-event.png";
import "../styles/MainStyle.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import "../styles/SearchList.css";
import Pagination from '@mui/material/Pagination';
import moment from 'moment';
import "../styles/EventApprovals.css"
import Button from '@mui/material/Button';
import Alert from '@mui/material/Alert';
export default function EventsApproval({getPendingEvents,cancleEvent,acceptEvent}){
    const navigate = useNavigate();
    

    const [update,setUpdate] = React.useState(true);
    const [pageCount,setPageCount] = React.useState(1);
    const [pendingEvents,setPendingEvents] = React.useState();
    React.useEffect(()=>{
        getPendingEvents(pageCount,10).then(r=>{
            setPendingEvents(r);
        })
    },[pageCount,update])


    const onAcceptEvent = (id,key) =>{
        acceptEvent(id).then(r=>{
            setUpdate(!update)
        })
    }

    const onCancleEvent = (id,key) =>{
        cancleEvent(id).then(r=>{
            setUpdate(!update)
        })
    }


    return(
        <div className="App">
        <Header/>
    <main className='content'>

        <div className='searchList'>
            <div className='title'>Zatwierdzanie wydarzeń</div>
            {
                pendingEvents&&pendingEvents.totalCount>0?
                    
                    pendingEvents.events.map((val,key)=>{
                        return(
                            <div className='event-approval-content'>

                           
                            <div className='event-on-list' key={key}>
                                <div className='event-list-img'><img src={val.imgURL}/></div>
                                <div className='event-list-info'>
                                    <div className='event-list-title'>{val.title}</div>
                                    <div className='event-list-placeTime'>
                                        <div className='event-list-time'>{moment(val.date).format("DD-MM-YYYY")}</div>
                                        <div className='event-list-place'>{val.location.name}</div>
                                    </div>
                                </div>
                                <div className='event-list-price'>już od {val.ticketPrice}PLN</div>

                            </div>
                             <div className='approve-option'>
                             <Button 
                                sx={{width:"10rem"}}
                                variant="contained" 
                                color="success"  
                                onClick={()=>onAcceptEvent(val.eventID,key)} 
                                size='large'
                            >
                                Zatwierdź
                            </Button>
                            <Button 
                                sx={{width:"10rem"}}
                                variant="contained" 
                                color="error"  
                                onClick={()=>onCancleEvent(val.eventID,key)} 
                                size='large'
                            >
                                Odrzuć
                            </Button>
                            <Button 
                                sx={{width:"10rem"}}
                                variant="contained" 
                                size='large'
                                onClick={()=>navigate("/event-preview",{state:{preview: val}})}
                            >
                                Zobacz
                            </Button>
                            
                            </div>
                            </div>
                        )
                    })
                :
                <Alert variant="filled" severity="info">
                    Brak wydarzeń do wyświetlenia
                </Alert>
            }  
            {
                pendingEvents&&pendingEvents.totalCount>0?
                <Pagination count={Math.ceil(pendingEvents.totalCount/10)} size='large' onChange={(e,v)=>{setPageCount(v)}}/>
                :
                null
            }
           
        </div>
    </main>
    <div className='App-footer'>
        <Footer/>
      </div>
  </div>

    );
}