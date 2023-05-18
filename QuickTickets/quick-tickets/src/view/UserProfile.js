import React from "react";
import '../styles/UserProfile.css';
import Header from '../components/Header';
import { GreenInput } from "../components/GreenInput";
import exampleEvent from "../images/example-event.png";
import Pagination from '@mui/material/Pagination';
import { useNavigate } from "react-router-dom";
import Footer from '../components/Footer';
import Swal from 'sweetalert2';
import SummaryBuy from "../components/SummaryBuy";
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
function UserProfile(){
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
    const [open, setOpen] = React.useState(false);
    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };
    return(
        <div className="App">
            <Dialog
                open={open}
                onClose={handleClose}
                maxWidth
            >
                <DialogTitle>
                {"Twój bilet"}
                </DialogTitle>
                <DialogContent>
                <DialogContentText>
                    <div style={{display:"flex"}}>
                        <SummaryBuy/>
                    </div>
                    
                </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <button onClick={handleClose} className="main-btn">Zamknij</button>
                </DialogActions>
            </Dialog>




            <Header/>
            <main className='content'>                
                    <div className="content-data">
                        <div className="content-data-column">
                                <h1>Witaj Jan Nowak</h1>

                                <GreenInput label="Imię" fullWidth type="text" ></GreenInput>
                                <GreenInput label="Nazwisko" fullWidth type="text" ></GreenInput>
                                <GreenInput label="Adres" fullWidth type="text" ></GreenInput>
                                <GreenInput label="Numer telefonu" fullWidth type="tel" ></GreenInput>
                                <GreenInput label="Email" fullWidth type="email" ></GreenInput>
                                <button className="main-btn" type="submit">Zmień</button>
                        </div>
                        
                    </div>
                    <div className='searchList'>
                        <h1>Zakupione bilety:</h1>
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
                                                <div className='event-list-place'>cena: 200zł</div>
                                                <div className='event-list-place'>ilość: 10</div>
                                            </div>
                                        </div>
                                        <div className='buy-option right'><button className='main-btn' onClick={handleClickOpen}>Pokaż bilet</button></div>
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
        
    )
}
export default UserProfile;