import { useNavigate } from 'react-router-dom'
import people from '../images/people.png'
export default function LocationComponent({localImg,localText,localTitle,location,disableSearch}){
    const navigate = useNavigate();
    return(
        <div className='event-place'>
                <div className='event-title-header' style={{paddingLeft:"2rem"}}>Lokalizacja</div>
                <div className='event-content'>
                    <div className='event-img'>
                        <img src={
                            localImg!=""?
                            localImg
                            :
                            people
                            
                        }/>
                    </div>
                    <div className='event-description'>
                        <h3 data-testid="preview-event-title">{
                            localTitle!==""?
                            localTitle
                            :
                            "Nazwa lokalizacji"
                            
                        }</h3>
                        <div className='event-description-text' data-testid="preview-event-desc">
                        {
                            localText!==""?
                            localText
                            :
                            "Opis lokalizacji"
                        
                        }</div>
                        <button className='main-btn' onClick={()=>navigate("/search-list",{state:{phrase:"",location:location.locationID}})} disabled={disableSearch}>Więcej wydarzeń</button>
                    </div>
                </div>
            </div>
    )
}