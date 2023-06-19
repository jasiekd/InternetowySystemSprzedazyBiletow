import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import { GreenInput } from '../components/GreenInput';
import "../styles/DropDownMenu.css";
import calendar from "../images/calendar.png";
import place from "../images/place.png";
import category from "../images/category.png";
import price from "../images/price.png";
import arrow from "../images/arrow.png";
import "../styles/SearchList.css";
import Pagination from '@mui/material/Pagination';
import EventsController from '../controllers/Events';
import { useLocation } from 'react-router-dom';
import moment from 'moment';


function FilteringOptions({getTypesOfEvents, setFilters,filters,getEventLocations}){
    const [openDate, setOpenDate] = React.useState(false);
    const [openPlace, setOpenPlace] = React.useState(false);
    const [openCategory, setOpenCategory] = React.useState(false);
    const [openPrice, setOpenPrice] = React.useState(false);
    const handleOpenDate = () => {
        setOpenDate(!openDate);
    };
    const handleOpenPlace = () => {
        setOpenPlace(!openPlace);
    };
    const handleOpenCategory = () => {
        setOpenCategory(!openCategory);
    };
    const handleOpenPrice = () => {
        setOpenPrice(!openPrice);
    };
    const [categoryList,setCategoryList] = React.useState([]);
    const [locationsList,setLocationsList] = React.useState([]);
    //const [selectedCategory,setSelectedCategory] = React.useState("Kategoria");
    //const [selecredLocation,setSelectedLocation] = React.useState("Miejsce");
    //const [selectedDate,setSelectedDate] = React.useState("Data");
    const [dateFrom,setDateFrom] = React.useState(null); 
    const [dateTo,setDateTo] = React.useState(null); 
    //const [selectedPrice,setSelectedPrice] = React.useState("Cena");
    const [priceFrom,setPriceFrom] = React.useState("");
    const [priceTo,setPriceTo] = React.useState("");
    React.useEffect(()=>{
        getTypesOfEvents().then((result)=>{
            setCategoryList(result);
        })

        getEventLocations().then((result)=>{
            setLocationsList(result);
        })
    },[])

    const selectLocation = (loc) =>{
        if(loc === null)
        {
            setFilters(prev =>({
                ...prev,
                place: null,
                selectLocation: "wszystkie"
            }))
            
            handleOpenPlace();
        }else{
            
            setFilters(prev =>({
                ...prev,
                place: loc.locationID,
                selectedLocation: loc.name
            }))
            handleOpenPlace();
        }
        
    }
    const selectCategory = (type) =>{
        if(type === null){
            setFilters(prev =>({
                ...prev,
                type: null,
                selectedCategory: "wszystki"
            }))
            handleOpenCategory();
        }else{
            setFilters(prev =>({
                ...prev,
                type: type.typeID,
                selectedCategory: type.description
            }))
            handleOpenCategory();
        }
       
    }
    const selectDate = () =>{
       
           setFilters(prev =>({
            ...prev,
            dateFrom: dateFrom,
            dateTo: dateTo,
            selectedDate: "od "+dateFrom+" do "+dateTo
        }))
           
           
        handleOpenDate();
    }
    const deleteDate = () =>{
        handleOpenDate();
        setDateTo(null);
        setDateFrom(null);
        setFilters(prev =>({
            ...prev,
            dateFrom: null,
            dateTo: null,
            selectedDate: "Data"
        }))
        
        
    }

    const selectPrice = () =>{

        setFilters(prev =>({
            ...prev,
            priceFrom: priceFrom,
            priceTo: priceTo,
            selectedPrice: "od "+priceFrom + " do "+priceTo
        }))
            
        handleOpenPrice();
    }
    const deletePrice = () =>{
        handleOpenPrice();
        setPriceFrom(null);
        setPriceTo(null);
        setFilters(prev =>({
            ...prev,
            priceFrom: null,
            priceTo: null,
            selectedPrice:"Cena"
        }))
    }


    return(
            <div className='filteringOptions'>
            <div className='filteringElement'>
                <button className='filteringButton' onClick={handleOpenDate}><img className='leftIcon' src={calendar}/><div className='filteringTitle'>{filters.selectedDate}</div><img className='rightIcon' src={arrow}/></button>
                    
                    {openDate ? (
                        <div className="menu">
                            Okres od
                            <GreenInput value={dateFrom} label="" onChange={(e)=>setDateFrom(e.target.value)} fullWidth type="date"></GreenInput>
                            Okres do
                            <GreenInput value={dateTo} label="" onChange={(e)=>setDateTo(e.target.value)} fullWidth type="date"></GreenInput>

                            
                            <div style={{display:"flex", gap:"1rem"}}>
                                <button className='main-btn' onClick={()=>deleteDate()}>Usuń</button>
                                <button className='main-btn' onClick={()=>selectDate()}>Zapisz</button>
                            </div>
                        </div>
                ) : null}
            </div>  
            <div className='filteringElement'>
                <button className='filteringButton' onClick={handleOpenPlace}><img className='leftIcon' src={place}/><div className='filteringTitle'>{filters.selectedLocation}</div><img className='rightIcon' src={arrow}/></button>
                
                    {openPlace ? (
                        <div className="menu">
                            <button className='drop-down-btn' key={-1} onClick={()=>selectLocation(null)}>wszystkie</button>
                            {
                                locationsList.map((val,key)=>{
                                    return(
                                        <button className='drop-down-btn' key={key} onClick={()=>selectLocation(val)}>{val.name}</button>
                                    )
                                })
                            }
                        </div>
                ) : null}
            </div>  
            <div className='filteringElement'>
                <button className='filteringButton' onClick={handleOpenCategory}><img className='leftIcon' src={category}/><div className='filteringTitle'>{filters.selectedCategory}</div><img className='rightIcon' src={arrow}/></button>
                
                    {openCategory ? (
                        <div className="menu">
                            <button className='drop-down-btn' key={-1} onClick={()=>selectCategory(null)}>wszystkie</button>
                            {
                                categoryList.map((val,key)=>{
                                    return(
                                        <button className='drop-down-btn' key={key} onClick={()=>selectCategory(val)}>{val.description}</button>
                                    )
                                })
                            }
                        </div>
                ) : null}
            </div>  
            <div className='filteringElement'>
                <button className='filteringButton' onClick={handleOpenPrice}><img className='leftIcon' src={price}/><div className='filteringTitle'>{filters.selectedPrice}</div><img className='rightIcon' src={arrow}/></button>
                
                    {openPrice ? (
                        <div className="menu">
                            Cena od
                            <GreenInput label="" type='number' value={priceFrom} onChange={(e)=>setPriceFrom(e.target.value)}/>
                            Cena do
                            <GreenInput label="" type='number'value={priceTo} onChange={(e)=>setPriceTo(e.target.value)}/>
                            <div style={{display:"flex", gap:"1rem"}}>
                                <button className='main-btn' onClick={()=>deletePrice()}>Usuń</button>
                                <button className='main-btn' onClick={()=>selectPrice()}>Zapisz</button>
                            </div>
                        </div>
                ) : null}
            </div>  
        </div>
    )
}

function SearchEvents({search,filters}){

    const navigate = useNavigate();
    const [events,setEvents] = React.useState();
    const [pageCount,setPageCount] = React.useState(1);
    React.useEffect(()=>{

        search(filters.phrase,filters.priceFrom,filters.priceTo,filters.dateFrom,filters.dateTo,filters.place,filters.type,pageCount,10).then(r=>{
            setEvents(r);
            
        })
    },[filters])


    return(
        <div className='searchList'>
                {
                    events!==undefined?
                        
                        events.value.events.map((val,key)=>{
                            return(
                                <div className='event-on-list'>
                                    <div className='event-list-img'><img src={val.imgURL}/></div>
                                    <div className='event-list-info'>
                                        <div className='event-list-title'>{val.title}</div>
                                        <div className='event-list-placeTime'>
                                            <div className='event-list-time'>{moment(val.date).format("DD-MM-YYYY")}</div>
                                            <div className='event-list-place'>{val.location.name}</div>
                                        </div>
                                    </div>
                                    <div className='event-list-price'>już od {val.ticketPrice}PLN</div>
                                    <div className='buy-option'><button className='main-btn' onClick={()=>navigate("/event",{state:{eventId:val.eventID}})}>Zobacz</button></div>
                                </div>
                            )
                        })
                        
                        
                    :
                    null
                }  
                {
                    events!==undefined?
                    <Pagination count={Math.ceil(events.value.totalCount/10)} size='large' onChange={(e,v)=>{setPageCount(v)}}/>
                    :
                    null
                }
            </div>
    )
}
export default function SearchList() {
    const navigate = useNavigate();

    const [filters, setFilters] = React.useState({
        phrase: "",
        dateFrom: null,
        dateTo: null,
        priceFrom: null,
        priceTo: null,
        place: null,
        type: null,
        selectedCategory:"Kategoria",
        selecredLocation:"Miejsce",
        selectedDate:"Data",
        selectedPrice:"Cena",
      });
      

 
    const location = useLocation();
    
 
    React.useEffect(()=>{
            if(location.state === null || location.state.phrase === null )
            {
                 navigate("/home")
            }
            else{
                if(location.state.location)
                    setFilters(prev =>({
                        ...prev,
                        phrase: location.state.phrase,
                        place: location.state.location.locationID,
                        selectedLocation: location.state.location.name,
                        type: location.state.type
                    }))
                else
                    setFilters(prev =>({
                        ...prev,
                        phrase: location.state.phrase,
                        type: location.state.type
                    }))
      
            }     
    },[location])

   
    return (

        <div className="App">
            <Header/>
        <main className='content'>
            
            <EventsController >
                <FilteringOptions filters={filters} setFilters={setFilters}/>
            </EventsController>


            <EventsController >
                <SearchEvents filters={filters}/>
            </EventsController>

        </main>
        <div className='App-footer'>
            <Footer/>
          </div>
      </div>
    );
}