import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import exampleEvent from "../images/example-event.png";
import "../styles/Event.css";
import examplePlace from "../images/place.jfif";
import Swal from 'sweetalert2';
import EventComponent from '../components/EventComponent.js';
import LocationComponent from '../components/LocationComponent.js';
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
    const showAddComent=()=>{
        const { value: text } = Swal.fire({
            input: 'textarea',
            inputLabel: 'Dodaj Komentarz',
            inputPlaceholder: 'Napisz co sądzisz o tym wydarzeniu...',
            inputAttributes: {
              'aria-label': 'Type your message here'
            },
            confirmButtonText: 'Zapisz',
            confirmButtonColor: '#93BB60',
            cancelButtonText: 'Anuluj',
            showCancelButton: true,
            showCloseButton: true
          })
          
          if (text) {
            Swal.fire(text)
          }
    }
    return (

        <div className="App">
            <Header/>
        <main className='content'>
            <EventComponent
                eventImg={exampleEvent}
                eventTitle={"Lorem ipsum nazwa"}
                eventDate={"12.12.2023"}
                eventPlace={"Kielce"}
                eventText={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. Nulla facilisi. Phasellus dignissim metus eget turpis semper, in lacinia elit viverra. Nam quis est eget diam maximus hendrerit id sit amet est. Morbi in tristique est. Mauris vel lorem erat. Nullam varius luctus finibus. Aenean lacinia metus id ligula facilisis commodo.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. Nulla facilisi. Phasellus dignissim metus eget turpis semper, in lacinia elit viverra. Nam quis est eget diam maximus hendrerit id sit amet est. Morbi in tristique est. Mauris vel lorem erat. Nullam varius luctus finibus. Aenean lacinia metus id ligula facilisis commodo."}
            />
            <LocationComponent 
                localImg={examplePlace} 
                localText={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. Nulla facilisi. Phasellus dignissim metus eget turpis semper, in lacinia elit viverra. Nam quis est eget diam maximus hendrerit id sit amet est. Morbi in tristique est. Mauris vel lorem erat. Nullam varius luctus finibus. Aenean lacinia metus id ligula facilisis commodo.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus non sem ac mauris congue vehicula. Cras eget lacus libero. Donec non tincidunt ipsum, a scelerisque nunc. Quisque interdum arcu in ipsum gravida, sit amet consectetur enim egestas. Aenean quis tellus in nisl aliquet pellentesque quis sed tellus. Nulla facilisi. Phasellus dignissim metus eget turpis semper, in lacinia elit viverra. Nam quis est eget diam maximus hendrerit id sit amet est. Morbi in tristique est. Mauris vel lorem erat. Nullam varius luctus finibus. Aenean lacinia metus id ligula facilisis commodo."}
            />
            <div className='comments'>
            <div className='event-title-header' style={{paddingLeft:"2rem",paddingTop:"1rem"}}>Komentarze<button className='main-btn' style={{marginRight:"0",marginLeft:"auto"}} onClick={showAddComent}>Dodaj komentarz</button></div>
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