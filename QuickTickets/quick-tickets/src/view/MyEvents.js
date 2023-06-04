import * as React from 'react';
import Footer from '../components/Footer';
import exampleEvent from "../images/example-event.png";
import "../styles/MainStyle.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import "../styles/DropDownMenu.css";
import "../styles/SearchList.css";
import Pagination from '@mui/material/Pagination';
import moment from 'moment';
export default function MyEvents({getOrganisatorEvents}){
    const navigate = useNavigate();
    const [myEvents,setMyEvents] = React.useState();
    const [pageCount,setPageCount] = React.useState(1);
   
    React.useEffect(()=>{
        getOrganisatorEvents(pageCount,10).then(r=>{
            console.log(r);
            setMyEvents(r)
        })
    },[pageCount])
    return(
        <div className="App">
        <Header/>
    <main className='content'>

        <div className='searchList'>
            <div className='title'>Moje wydarzenia</div>
            {
                myEvents?
                    myEvents.value.events.map((val,key)=>{
                        return(
                            <div className='event-on-list'>
                                <div className='event-list-img'><img src={val.imgURL}/></div>
                                <div className='event-list-info'>
                                    <div className='event-list-title'>{val.title}</div>
                                    <div className='event-list-placeTime'>
                                        <div className='event-list-time'>{moment(val.dateCreated).format("DD-MM-YYYY")}</div>
                                        <div className='event-list-place'>{val.location.name}</div>
                                    </div>
                                </div>
                                <div className='event-list-price'>już od {val.ticketPrice}PLN</div>
                                <div className='buy-option'><button className='main-btn' onClick={()=>navigate("/event")}>Zobacz</button></div>
                            </div>
                        )
                    })
                :
                null
                
            }  
            {
                myEvents?
                <Pagination count={Math.ceil(myEvents.value.totalCount/10)} size='large' onChange={(e,v)=>{setPageCount(v)}}/>
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