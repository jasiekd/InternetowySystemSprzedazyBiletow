import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import { useLocation, useNavigate } from "react-router-dom";
import Header from '../components/Header';
import "../styles/Event.css";
import Swal from 'sweetalert2';
import EventComponent from '../components/EventComponent.js';
import LocationComponent from '../components/LocationComponent.js';
import moment from 'moment';
export default function Event({getEvent}) {
    const location = useLocation();
    const navigate = useNavigate();
    const comments =[
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
    ]
    const showAddComent=()=>{
        const { value: text } = Swal.fire({
            input: 'textarea',
            inputLabel: 'Dodaj Komentarz',
            inputPlaceholder: 'Napisz co sądzisz o tym wydarzeniu...',
            inputAttributes: {
              'aria-label': 'Type your message here'
            },
            confirmButtonText: 'Zapisz',
            confirmButtonColor: '#93BB60',
            cancelButtonText: 'Anuluj',
            showCancelButton: true,
            showCloseButton: true
          })
          
          if (text) {
            Swal.fire(text)
          }
    }
    const [ready,setReady] = React.useState(false);
    const [eventInfos,setEventInfos] = React.useState();
    React.useEffect(()=>{
        if(location.state === null){
            navigate("/home")
        }else{
            setReady(true);
            getEvent(location.state.eventId).then(r=>{
                setEventInfos(r);
            })
        }
    },[])
    return (

        <div className="App">
            <Header/>
        <main className='content'>
            {
                eventInfos?
                    <>
                        <EventComponent
                            eventImg={eventInfos.imgURL}
                            eventTitle={eventInfos.title}
                            eventDate={moment(eventInfos.date).format("DD-MM-YYYY")}
                            eventPlace={eventInfos.location.name}
                            eventText={eventInfos.description}
                            eventData={eventInfos}
                        />
                        <LocationComponent 
                            localImg={eventInfos.location.imgURL} 
                            localText={eventInfos.location.description}
                            location={eventInfos.location}
                        />
                        <div className='comments'>
                        <div className='event-title-header' style={{paddingLeft:"2rem",paddingTop:"1rem"}}>Komentarze<button className='main-btn' style={{marginRight:"0",marginLeft:"auto"}} onClick={showAddComent}>Dodaj komentarz</button></div>
                            <div className='comment-list'>
                                {
                                    ready?
                                        comments.map((val,key)=>{
                                            return(
                                                <div className='comment'>
                                                    <div className='comment-author'>Jan Nowak</div>
                                                    <div className='comment-tetx'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. </div>
                                                </div>
                                            )
                                        })
                                    :
                                        null
                                    
                                }
                            </div>
                        </div>
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