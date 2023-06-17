import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import { useLocation, useNavigate } from "react-router-dom";
import Header from '../components/Header';
import "../styles/Event.css";
import EventComponent from '../components/EventComponent.js';
import LocationComponent from '../components/LocationComponent.js';
import moment from 'moment';
import { checkIsLogged } from '../controllers/Login';
import Alert from '@mui/material/Alert';
export default function EventPreview({}) {
    const location = useLocation();
    const navigate = useNavigate();
   
    const [ready,setReady] = React.useState(false);
    const [eventInfos,setEventInfos] = React.useState();
    React.useEffect(()=>{
        if(location.state && location.state.preview)
        {
            setEventInfos(location.state.preview);
        }
        else{
            navigate("/home");
        }
    },[])
    return (

        <div className="App">
            <Header/>
        <main className='content'>
            {
                eventInfos?
                    <>
                    <div style={{display:"flex",width:"100%",gap:"2rem"}}>
                        <Alert variant="filled" severity="info" sx={{width:"100%"}}>
                            <h1>Tryb podglądu wydarzenia</h1>
                        </Alert>
                    </div>
                        
                        <EventComponent
                            eventImg={eventInfos.imgURL}
                            eventTitle={eventInfos.title}
                            eventDate={moment(eventInfos.date).format("DD-MM-YYYY")}
                            eventPlace={eventInfos.location.name}
                            eventText={eventInfos.description}
                            eventData={eventInfos}
                            disableBuy={true}
                            occupiedSeats={eventInfos.occupiedSeats}
                            seats={eventInfos.seats}
                        />
                        <LocationComponent 
                            localImg={eventInfos.location.imgURL} 
                            localText={eventInfos.location.description}
                            location={eventInfos.location}
                            disableSearch={true}
                        />
                    </>
                :
                    null
            }
            
        </main>
        <div className='App-footer'>
            <Footer/>
          </div>
      </div>
    );
}