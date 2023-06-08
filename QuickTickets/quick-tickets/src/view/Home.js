import * as React from 'react';
import mainImg from '../images/event.jpg';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import Header from '../components/Header';
import { useState, useEffect } from 'react';
import EventsController from '../controllers/Events.js';

import concert from '../images/category-icons/concert.png'
import theater from '../images/category-icons/theater.png'
import cinema from '../images/category-icons/cinema.png'
import fair from '../images/category-icons/fair.png'
import festival from '../images/category-icons/festival.png'
import sports from '../images/category-icons/sports.png'
import standUp from '../images/category-icons/stand-up-comedy.png'
import children from '../images/category-icons/children.png'
import classic from '../images/category-icons/classic.png'
import MostPopularPlaces from '../components/MostPopularPlaces';
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

export default function Home() {

    return (

        <div className="App">
            <Header/>
        
        
        <main className='content'>
          <div className='info'>
            <div className='info-left'>
                <img className='info-event-img' src={mainImg}  alt=""/>
            </div>
            <div className='info-right'>
            <h1>Witajcie! Cieszymy się, że odwiedzacie stronę QuickTickets</h1>
            <br/>
            <br/>
            <p>Najlepszego miejsca, aby zapewnić sobie bilety na najbardziej ekscytujące wydarzenia! Jeśli jesteście gotowi na niesamowite przygody, niezwykłe spektakle i niezapomniane chwile, to trafiłeś we właściwe miejsce.</p>
            <br/>
            <p>Nasze doświadczenie i zaangażowanie sprawiają, że każdy zakupiony u nas bilet staje się wyjątkowym przeżyciem. Pragniemy zapewnić Wam dostęp do najbardziej ekskluzywnych i popularnych wydarzeń, abyście mogli delektować się ich magią i energią.</p>
            </div>
            
            </div>
          <div className='trendy'>
            <div>
                <div className='content-header'>
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
                <button className='btn events-categories-element'>
                    <img className='img' src={concert} alt="" style={{height:"10rem"}}/>
                    <p>Koncerty</p>
                </button>
            </div>
            <div className='events-categories-column'>
           
                <div className='events-categories-row'>
                    <button className='btn events-categories-element'>
                        <img className='img' src={theater} alt=""/>
                        <p>Teatr</p>
                    </button>
                    <button className='btn events-categories-element'>
                        <img className='img' src={children} alt=""/>
                        <p>Dla dzieci</p>
                    </button>
                    <button className='btn events-categories-element'>
                        <img className='img' src={standUp} alt=""/>
                        <p>Stand-Up</p>
                    </button>
                    <button className='btn events-categories-element'>
                        <img className='img' src={cinema} alt=""/>
                        <p>Kino</p>
                    </button>
                </div>
                <div className='events-categories-row'>
                    <button className='btn events-categories-element'>
                        <img className='img' src={sports} alt=""/>
                        <p>Sport</p>
                    </button>
                    <button className='btn events-categories-element'>
                        <img className='img' src={classic} alt=""/>
                        <p>Klasyka</p>
                    </button>
                    <button className='btn events-categories-element' >
                        <img className='img' src={fair} alt=""/>
                        <p>Targi</p>
                    </button>
                    <button className='btn events-categories-element'>
                       
                        <img className='img' src={festival} alt=""/>
                        <p>Festivale</p>
                    </button>
                </div>
                </div>
          </div>
          <div className='popular-places'>
                <div>
                    <div className='content-header'>
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