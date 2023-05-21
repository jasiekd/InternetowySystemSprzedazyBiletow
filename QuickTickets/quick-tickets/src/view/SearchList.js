import * as React from 'react';
import Footer from '../components/Footer';
import exampleEvent from "../images/example-event.png";
import "../styles/MainStyle.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import Select from '@mui/material/Select';
import { GreenInput } from '../components/GreenInput';
import MenuItem from '@mui/material/MenuItem';
import "../styles/DropDownMenu.css";
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import calendar from "../images/calendar.png";
import place from "../images/place.png";
import category from "../images/category.png";
import price from "../images/price.png";
import arrow from "../images/arrow.png";
import "../styles/SearchList.css";
import Pagination from '@mui/material/Pagination';
import EventsController from '../controllers/Events';

function FilteringOptions({getTypesOfEvents,getEventLocations}){
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
    const [selectedCategory,setSelectedCategory] = React.useState("Kategoria");
    const [selecredLocation,setSelectedLocation] = React.useState("Miejsce");
    const [selectedDate,setSelectedDate] = React.useState("Data");
    const [dateFrom,setDateFrom] = React.useState(null); 
    const [dateTo,setDateTo] = React.useState(null); 
    const [selectedPrice,setSelectedPrice] = React.useState("Cena");
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

    const selectLocation = (name) =>{
        setSelectedLocation(name);
        handleOpenPlace();
    }
    const selectCategory = (name) =>{
        setSelectedCategory(name);
        handleOpenCategory();
    }
    const selectDate = () =>{
        if(dateFrom!==""&&dateTo!=="")
            setSelectedDate(dateFrom+" - "+dateTo);
        handleOpenDate();
    }
    const deleteDate = () =>{
        setSelectedDate("Data");
        handleOpenDate();
        setDateTo("");
        setDateFrom("");
    }

    const selectPrice = () =>{
        if(priceFrom!==""&&priceTo!=="")
            setSelectedPrice(priceFrom+" - "+priceTo);
        handleOpenPrice();
    }
    const deletePrice = () =>{
        setSelectedPrice("Cena");
        handleOpenPrice();
        setPriceFrom("");
        setPriceTo("");
    }
    return(
            <div className='filteringOptions'>
            <div className='filteringElement'>
                <button className='filteringButton' onClick={handleOpenDate}><img className='leftIcon' src={calendar}/><div className='filteringTitle'>{selectedDate}</div><img className='rightIcon' src={arrow}/></button>
                    
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
                <button className='filteringButton' onClick={handleOpenPlace}><img className='leftIcon' src={place}/><div className='filteringTitle'>{selecredLocation}</div><img className='rightIcon' src={arrow}/></button>
                
                    {openPlace ? (
                        <div className="menu">
                            <button className='drop-down-btn' key={-1} onClick={()=>selectLocation("Miejsce")}>wszystkie</button>
                            {
                                locationsList.map((val,key)=>{
                                    return(
                                        <button className='drop-down-btn' key={key} onClick={()=>selectLocation(val.name)}>{val.name}</button>
                                    )
                                })
                            }
                        </div>
                ) : null}
            </div>  
            <div className='filteringElement'>
                <button className='filteringButton' onClick={handleOpenCategory}><img className='leftIcon' src={category}/><div className='filteringTitle'>{selectedCategory}</div><img className='rightIcon' src={arrow}/></button>
                
                    {openCategory ? (
                        <div className="menu">
                            <button className='drop-down-btn' key={-1} onClick={()=>selectCategory("Kategoria")}>wszystkie</button>
                            {
                                categoryList.map((val,key)=>{
                                    return(
                                        <button className='drop-down-btn' key={key} onClick={()=>selectCategory(val.description)}>{val.description}</button>
                                    )
                                })
                            }
                        </div>
                ) : null}
            </div>  
            <div className='filteringElement'>
                <button className='filteringButton' onClick={handleOpenPrice}><img className='leftIcon' src={price}/><div className='filteringTitle'>{selectedPrice}</div><img className='rightIcon' src={arrow}/></button>
                
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

export default function SearchList() {

    const navigate = useNavigate();
    

    const eventList =[
        {
            title: "item 1",
        },
        {
            title: "item 2",
        },
        {
            title: "item 3",
        },
        {
            title: "item 4",
        },
        {
            title: "item 5",
        },
        {
            title: "item 6",
        },
        {
            title: "item 7",
        },
        {
            title: "item 8",
        }
    ]
    return (

        <div className="App">
            <Header/>
        <main className='content'>
            <EventsController>
                <FilteringOptions/>
            </EventsController>
            <div className='searchList'>
                {
                    eventList.map((val,key)=>{
                        return(
                            <div className='event-on-list'>
                                <div className='event-list-img'><img src={exampleEvent}/></div>
                                <div className='event-list-info'>
                                    <div className='event-list-title'>Lorem ipsum nazwa</div>
                                    <div className='event-list-placeTime'>
                                        <div className='event-list-time'>12.12.2023</div>
                                        <div className='event-list-place'>Kielce</div>
                                    </div>
                                </div>
                                <div className='event-list-price'>już od 80PLN</div>
                                <div className='buy-option'><button className='main-btn' onClick={()=>navigate("/event")}>Kup teraz</button></div>
                            </div>
                        )
                    })
                }  
                <Pagination count={10} size='large'/>
            </div>
        </main>
        <div className='App-footer'>
            <Footer/>
          </div>
      </div>
    );
}