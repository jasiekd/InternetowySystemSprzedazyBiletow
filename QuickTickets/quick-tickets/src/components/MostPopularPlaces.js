import { useState,useEffect } from "react";
import { useNavigate } from "react-router-dom";

export default function MostPopularPlaces({getHotLocations}){
    const [popularPlaces,setPopularPlaces] = useState(null);
    const navigate = useNavigate();
    useEffect(()=>{

        getHotLocations().then((result)=>{
            setPopularPlaces(result);
        })
    },[])
    return(
    <div className='place-content' data-testid='test-place-content'>
        {
            popularPlaces?
            popularPlaces.map((val,key)=>{
                
                return(
                    <div key={key} className="place-event" data-testid='test-place-event' onClick={()=>navigate("/search-list?location="+val.locationID)}>
                        <img className='place-img' src={val.imgURL}  alt=""/>
                        <div className="place-info">
                            <div className="place-title">
                                {val.name}
                            </div>
                        </div>
                    </div>
                    
                )
            
            })
            :
            null
        }
    </div>
    );
}