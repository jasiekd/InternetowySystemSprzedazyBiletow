import { useNavigate } from "react-router-dom";
import people from '../images/people.png'
export default function EventComponent({eventImg,eventTitle,eventDate,eventPlace,eventText, disableBuy}){
    const navigate = useNavigate();
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
                            eventDate!==""?
                            eventDate
                            :
                            "Data wydarzenia"
                        
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
                        <div className='event-description-text'>
                            {
                                eventText!==""?
                                eventText
                                :
                                "Opis wydarzenia"
                            
                            }
                        </div>
                        <button className='main-btn' onClick={()=>navigate("/buy-ticket")} disabled={disableBuy}>Kup teraz</button>
                    </div>
                </div>     
            </div>          
    )
}