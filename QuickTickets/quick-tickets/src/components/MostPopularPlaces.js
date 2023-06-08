import { useState,useEffect } from "react";

export default function MostPopularPlaces({getHotLocations}){
    const [popularPlaces,setPopularPlaces] = useState(null);

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
                    <div className="place-event" data-testid='test-place-event'>
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