import React from "react";
import exampleEvent from "../images/example-event.png";
import '../styles/trendy-style.css';
const EventData = [
    {
        icon:exampleEvent,
        title: "Example Event",
        place: "Example Place",
        minPrice: "129"
    },
    {
        icon:exampleEvent,
        title: "Example Event",
        place: "Example Place",
        minPrice: "129"
    },
    {
        icon:exampleEvent,
        title: "Example Event",
        place: "Example Place",
        minPrice: "129"
    },
    {
        icon: exampleEvent,
        title: "Example Event",
        place: "Example Place",
        minPrice: "129"
    },
]

function Trendy(){
    return(
        <div>
            <div className='trendy-header'>
                Na czasie
            </div>
            <div className='trendy-content'>
                {
                    EventData.map((val,key)=>{
                        
                        return(
                            <div className="trendy-event">
                                <img className='trendy-event-img' src={val.icon}  alt=""/>
                                <div className="trendy-info">
                                    <div className="trendy-title">
                                        {val.title}
                                    </div>
                                    <div className="trendy-place">
                                        {val.place}
                                    </div>
                                    <div className="trendy-price">
                                        od {val.minPrice} zł
                                    </div>
                                    <button className="trendy-buy-button">Kup teraz</button>    
                                </div>
                            </div>
                              
                        )
                       
                    })
                }
            </div>
        </div>
        
    )
}
export default Trendy;