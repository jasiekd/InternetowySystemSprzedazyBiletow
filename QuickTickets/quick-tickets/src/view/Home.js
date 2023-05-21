import * as React from 'react';
import mainImg from '../images/event.jpg';
import categoryicon from '../images/categoryicon.png';
import Footer from '../components/Footer';
import exampleEvent from "../images/example-event.png";
import "../styles/MainStyle.css";
import examplePlace from "../images/place.jfif";
import { useNavigate } from "react-router-dom";
import logo from "../images/logo.png";
import Header from '../components/Header';
import { logOut } from '../controllers/Login';
import { useState, useEffect } from 'react';
import EventsController from '../controllers/Events.js';
function TrendyContent({getHotEvents}){
    const[trendyEvent,setTrendyEvent] = useState(null);

    useEffect(()=>{
        getHotEvents().then((result)=>{
            setTrendyEvent(result);
        })
    },[])
    return(
        <div className='trendy-content'>
            {
                    trendyEvent?
                        trendyEvent.map((val,key)=>{       
                            return(
                                <div className="trendy-event" key={key}>
                                    <img className='trendy-event-img' src={val.imgURL}  alt=""/>
                                    <div className="trendy-info">
                                        <div className="trendy-title">
                                            {val.title}
                                        </div>
                                        <div className="trendy-place">
                                            {val.location.name}
                                        </div>
                                        <div className="trendy-price">
                                            od {val.ticketPrice} zł
                                        </div>
                                            <button className="main-btn">Kup teraz</button>    
                                        </div>
                                </div>
                                        
                            )
                                
                        })
                    :
                        null  
            }
        </div>
    )
}
function MostPopularPlaces({getHotLocations}){
    const [popularPlaces,setPopularPlaces] = useState(null);

    useEffect(()=>{

        getHotLocations().then((result)=>{
            setPopularPlaces(result);
        })
    },[])
    return(
    <div className='place-content'>
        {
            popularPlaces?
            popularPlaces.map((val,key)=>{
                
                return(
                    <div className="place-event">
                        <img className='place-img' src={logo}  alt=""/>
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
export default function Home() {

    const navigate = useNavigate();

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
    const PlaceData = [
        {
            icon:examplePlace,
            title: "Example Place",
        },
        {
            icon:examplePlace,
            title: "Example Place",
        },
        {
            icon:examplePlace,
            title: "Example Place",
        },
        {
            icon: examplePlace,
            title: "Example Place",
        },
    ]


    return (

        <div className="App">
            <Header/>
        
        
        <main className='content'>
          <div className='info'>
            <div className='info-left'>
                <img className='info-event-img' src={mainImg}  alt=""/>
            </div>
            <div className='info-right'>
            <h3>Zapewniamy bilety na najlepsze wydarzenia tej zimy!11!!!!</h3>
            <p>Zapewniamy bilety na najlepsze wydarzenia tej zimy!11!!!!Zapewniamy bilety na najlepsze wydarzenia tej zimy!11!!!!</p>
          
            </div>
            
            </div>
          <div className='trendy'>
            <div>
                <div className='trendy-header'>
                    Na czasie
                </div>
                <EventsController>
                    <TrendyContent/>
                </EventsController>
            </div>
          </div>
          <div className='events-categories'>
            <div className='events-categories-header'>
                <h3>Kategorie wydarzeń:</h3>
            </div>
            <div className='events-categories-leftbtn'>
                <button className='btn'>
                    <img className='img' src={categoryicon} alt=""/>
                    <p>Koncerty</p>
                </button>
            </div>
            <div className='events-categories-column'>
           
                <div className='events-categories-row'>
                    <button className='btn events-categories-element'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Teatr</p>
                    </button>
                    <button className='btn events-categories-element'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Dla dzieci</p>
                    </button>
                    <button className='btn events-categories-element'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Widowiska</p>
                    </button>
                    <button className='btn events-categories-element'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Kino</p>
                    </button>
                </div>
                <div className='events-categories-row'>
                    <button className='btn events-categories-element'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Sport</p>
                    </button>
                    <button className='btn events-categories-element'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Klasyka</p>
                    </button>
                    <button className='btn events-categories-element' >
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Targi | Wystawy</p>
                    </button>
                    <button className='btn events-categories-element'>
                       
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Festivale</p>
                    </button>
                </div>
                </div>
          </div>
          <div className='popular-places'>
                <div>
                    <div className='place-header'>
                        Najpopularniejsze miejsca
                    </div>
                    <EventsController>
                        <MostPopularPlaces/>
                    </EventsController>

                    
                </div>
          </div>
         
        </main>
            <Footer/>
      </div>
    );
}