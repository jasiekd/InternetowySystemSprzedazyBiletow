import { useNavigate } from "react-router-dom";
import people from '../images/people.png'
import moment from "moment/moment";

export default function EventComponent({eventImg,eventTitle,eventDate,eventPlace,eventText, disableBuy,eventData,displayPreview,occupiedSeats,seats}){
    const navigate = useNavigate();
    // useEffect(()=>{
    //     // let date=eventDate.toISOString();
    //     console.log(date);
    // },[eventDate])
    return(
            <div className='event-info'>
                <div className='event-title'>
                    <div className='event-title-header'>
                        {
                            eventTitle!==""?
                            eventTitle
                            :
                            "Nazwa wydarzenia"
                        }
                    
                    </div>
                    <div>
                        {
                            !isNaN(eventDate)&&   
                            moment(eventDate.toISOString()).format('MMMM Do YYYY, h:mm:ss a') ||"Data wydarzenia"


                            // console.log('aha',eventDate.$d.toISOString())
                            // eventDate!==""?
                            // eventDate.$d.toISOString()
                            // :
                        
                        }
                        {" "}   
                        {
                            eventPlace!=="Miejsce"?
                            eventPlace
                            :
                            "Miejsce wydarzenia"
                            
                        }
                    
                    </div>
                </div>
                <div className='event-content'>
                    <div className='event-img'>
                        <img src={
                            eventImg!==""?
                            eventImg
                            :
                            people
                        }/>
                    </div>
                    <div className='event-description'>
                        <div className='event-description-title'>Opis wydarzenia</div>
           
                        

                        {
                            displayPreview?
                            <div className='event-description-text event-preview'>
                            {
                                eventText!==""?
                                eventText
                                :
                                "Opis wydarzenia"
                            
                            }
                            </div>
                            :
                            <div className='event-description-text '>
                            {
                                eventText!==""?
                                eventText
                                :
                                "Opis wydarzenia"
                            
                            }
                            </div>
                        }
                        <div style={{display:"flex",gap:"2rem"}}>
                            <button className='main-btn' onClick={()=>navigate("/buy-ticket",{state:{event:eventData}})} disabled={disableBuy}>Kup teraz</button>
                            <h2> Pozosta bilety: {seats-occupiedSeats}</h2>
                        </div>
                        
                    </div>
                </div>     
            </div>          
    )
}