import * as React from 'react';
import mainImg from '../images/event.jpg';
import Button from '@mui/material/Button';
import SearchIcon from '@mui/icons-material/Search';
import Trendy from '../components/Trendy';
import PopularPlaces from '../components/PopularPlaces';
import categoryicon from '../images/categoryicon.png';
import Footer from '../components/Footer';
export default function Home() {
    return (

        <div className="App">
            
        <header className="App-header">
         <p className='logo'>Quick Tickets</p>
         <img src=''  alt=""/>
         
         <input id="searcher" type="text" />

         <Button startIcon={<SearchIcon/>} sx={{ 
            backgroundColor: '#AEDF70', 
            color: 'white', 
            '&:hover': {
                backgroundColor: '#96C05F',
                boxShadow: 'none',
            },
        }}  className='greenButton'>

        </Button>
        </header>
        
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
              <Trendy/>  
          </div>
          <div className='events-categories'>
            <div className='events-categories-header'>
                <h3>Kategorie wydarzeń:</h3>
            </div>
            <div className='events-categories-leftbtn'>
                <button className='btn'>
                    <img className='img' src={categoryicon} alt=""/>
                    <p>Koncerty1</p>
                </button>
            </div>
            <div className='events-categories-column'>
           
                <div className='events-categories-row'>
                    <button className='btn'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Koncerty</p>
                    </button>
                    <button className='btn'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Koncerty</p>
                    </button>
                    <button className='btn'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Koncerty</p>
                    </button>
                    <button className='btn'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Koncerty</p>
                    </button>
                </div>
                <div className='events-categories-row'>
                    <button className='btn'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Koncerty</p>
                    </button>
                    <button className='btn'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Koncerty</p>
                    </button>
                    <button className='btn'>
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Koncerty</p>
                    </button>
                    <button className='btn'>
                       
                        <img className='img' src={categoryicon} alt=""/>
                        <p>Koncerty</p>
                    </button>
                </div>
                </div>
          </div>
          <div className='popular-places'>
            <PopularPlaces/>
          </div>
         
        </main>
        <div className='App-footer'>
            <Footer/>
          </div>
      </div>
    );
}