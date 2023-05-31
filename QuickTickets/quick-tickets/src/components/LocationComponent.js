import people from '../images/people.png'
export default function LocationComponent({localImg,localText,localTitle}){
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
                        <h3>{
                            localTitle!==""?
                            localTitle
                            :
                            "Nazwa lokalizacji"
                            
                        }</h3>
                        <div className='event-description-text'>
                        {
                            localText!==""?
                            localText
                            :
                            "Opis lokalizacji"
                        
                        }</div>
                        <button className='main-btn'>Więcej wydarzeń</button>
                    </div>
                </div>
            </div>
    )
}