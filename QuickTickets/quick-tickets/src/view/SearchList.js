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

export default function SearchList() {

    const navigate = useNavigate();
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

    const categoryList =[
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
        }
    ];

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
            <div className='filteringOptions'>
                <div className='filteringElement'>
                    <button className='filteringButton' onClick={handleOpenDate}><img className='leftIcon' src={calendar}/><div className='filteringTitle'>Data</div><img className='rightIcon' src={arrow}/></button>
                    
                        {openDate ? (
                            <div className="menu">
                                <LocalizationProvider dateAdapter={AdapterDayjs}>
                                    <DemoContainer components={['DatePicker']}>
                                        <DatePicker label="Okres od" />
                                    </DemoContainer>
                                </LocalizationProvider>

                                <LocalizationProvider dateAdapter={AdapterDayjs}>
                                    <DemoContainer components={['DatePicker']}>
                                        <DatePicker label="Okres do" />
                                    </DemoContainer>
                                </LocalizationProvider>
                                <button className='main-btn'>Zapisz</button>
                            </div>
                    ) : null}
                </div>  
                <div className='filteringElement'>
                    <button className='filteringButton' onClick={handleOpenPlace}><img className='leftIcon' src={place}/><div className='filteringTitle'>Miejsce</div><img className='rightIcon' src={arrow}/></button>
                    
                        {openPlace ? (
                            <div className="menu">
                                {
                                categoryList.map((val,key)=>{
                                    return(
                                        <button className='drop-down-btn' key={key}>{val.title}</button>
                                    )
                                })
                               }
                            </div>
                    ) : null}
                </div>  
                <div className='filteringElement'>
                    <button className='filteringButton' onClick={handleOpenCategory}><img className='leftIcon' src={category}/><div className='filteringTitle'>Kategoria</div><img className='rightIcon' src={arrow}/></button>
                    
                        {openCategory ? (
                            <div className="menu">
                               {
                                categoryList.map((val,key)=>{
                                    return(
                                        <button className='drop-down-btn' key={key}>{val.title}</button>
                                    )
                                })
                               }
                                
                            </div>
                    ) : null}
                </div>  
                <div className='filteringElement'>
                    <button className='filteringButton' onClick={handleOpenPrice}><img className='leftIcon' src={price}/><div className='filteringTitle'>Cena</div><img className='rightIcon' src={arrow}/></button>
                    
                        {openPrice ? (
                            <div className="menu">
                                <GreenInput label="Cena od" type='number'/>
                                <GreenInput label="Cena do" type='number'/>
                                <button className='main-btn'>Zapisz</button>
                            </div>
                    ) : null}
                </div>  
            </div>
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