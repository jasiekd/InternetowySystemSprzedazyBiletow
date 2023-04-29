import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import exampleEvent from "../images/example-event.png";
import "../styles/Event.css";
import examplePlace from "../images/place.jfif";

export default function Event() {

    const navigate = useNavigate();
    const comments =[
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
        {
            num: 1
        },
    ]
    return (

        <div className="App">
            <Header/>
        <main className='content'>
            <div className='event-info'>
                <div className='event-title'>
                    <div className='event-title-header'>Lorem ipsum nazwa</div>
                    <div>12.12.2023     Kielce</div>
                </div>
                <div className='event-content'>
                    <div className='event-img'>
                        <img src={exampleEvent}/>
                    </div>
                    <div className='event-description'>
                        <div className='event-description-title'>Opis wydarzenia</div>
                        <div className='event-description-text'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. Nulla facilisi. Phasellus dignissim metus eget turpis semper, in lacinia elit viverra. Nam quis est eget diam maximus hendrerit id sit amet est. Morbi in tristique est. Mauris vel lorem erat. Nullam varius luctus finibus. Aenean lacinia metus id ligula facilisis commodo.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. Nulla facilisi. Phasellus dignissim metus eget turpis semper, in lacinia elit viverra. Nam quis est eget diam maximus hendrerit id sit amet est. Morbi in tristique est. Mauris vel lorem erat. Nullam varius luctus finibus. Aenean lacinia metus id ligula facilisis commodo.</div>
                        <button className='main-btn'>Kup teraz</button>
                    </div>
                </div>     
            </div>
            <div className='event-place'>
                <div className='event-title-header' style={{paddingLeft:"2rem"}}>Lokalizacja</div>
                <div className='event-content'>
                    <div className='event-img'>
                        <img src={examplePlace}/>
                    </div>
                    <div className='event-description'>
                        <div className='event-description-text'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. Nulla facilisi. Phasellus dignissim metus eget turpis semper, in lacinia elit viverra. Nam quis est eget diam maximus hendrerit id sit amet est. Morbi in tristique est. Mauris vel lorem erat. Nullam varius luctus finibus. Aenean lacinia metus id ligula facilisis commodo.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. Nulla facilisi. Phasellus dignissim metus eget turpis semper, in lacinia elit viverra. Nam quis est eget diam maximus hendrerit id sit amet est. Morbi in tristique est. Mauris vel lorem erat. Nullam varius luctus finibus. Aenean lacinia metus id ligula facilisis commodo.</div>
                        <button className='main-btn'>Więcej wydarzeń</button>
                    </div>
                </div>
            </div>
            <div className='comments'>
            <div className='event-title-header' style={{paddingLeft:"2rem",paddingTop:"1rem"}}>Komentarze<button className='main-btn' style={{marginRight:"0",marginLeft:"auto"}}>Dodaj komentarz</button></div>
                <div className='comment-list'>
                    {
                        comments.map((val,key)=>{
                            return(
                                <div className='comment'>
                                    <div className='comment-author'>Jan Nowak</div>
                                    <div className='comment-tetx'>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. </div>
                                </div>
                            )
                        })
                    }
                </div>
            </div>
        </main>
        <div className='App-footer'>
            <Footer/>
          </div>
      </div>
    );
}