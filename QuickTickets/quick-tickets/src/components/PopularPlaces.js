import React from "react";
import exampleEvent from "../images/place.jfif";
import '../styles/popular-places.css';
const PlaceData = [
    {
        icon:exampleEvent,
        title: "Example Place",
    },
    {
        icon:exampleEvent,
        title: "Example Place",
    },
    {
        icon:exampleEvent,
        title: "Example Place",
    },
    {
        icon: exampleEvent,
        title: "Example Place",
    },
]

function PopularPlaces(){
    return(
        <div>
            <div className='place-header'>
            Najpopularniejsze miejsca
            </div>
            <div className='place-content'>
                {
                    PlaceData.map((val,key)=>{
                        
                        return(
                            <div className="place-event">
                                <img className='place-img' src={val.icon}  alt=""/>
                                <div className="place-info">
                                    <div className="place-title">
                                        {val.title}
                                    </div>
                                </div>
                            </div>
                              
                        )
                       
                    })
                }
            </div>
        </div>
        
    )
}
export default PopularPlaces;