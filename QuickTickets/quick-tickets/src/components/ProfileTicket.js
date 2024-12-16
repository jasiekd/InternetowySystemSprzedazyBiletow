import React from "react";
import SummaryBuy from "./SummaryBuy";
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import Button from '@mui/material/Button';
import '../styles/ProfileTicket.css'
import { useNavigate } from "react-router-dom";
import LoginController from "../controllers/Login";
import moment from "moment";
export default function ProfileTicket({seats,event,printAble,editAble,imgURL,date,location,price,title,editBlocked,preview}){
    const [open, setOpen] = React.useState(false);
    const navigate = useNavigate();
    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };
    return(
        <React.Fragment>
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
                            <LoginController>
                                <SummaryBuy
                                    eventData={event}
                                />
                            </LoginController>
                            
                        </div>
                        
                    </DialogContentText>
                    </DialogContent>
                    <DialogActions>
                        <button onClick={handleClose} className="main-btn">Zamknij</button>
                    </DialogActions>
            </Dialog>

            <div className='purchased-on-list'>
                <div className='event-list-img'><img src={imgURL}/></div>
                <div className='event-list-info'>
                <div className='event-list-title'>{title}</div>
                    <div className='event-list-placeTime'>
                        <div className='event-list-time'>{moment(date).format('DD.MM.YYYY, hh:mm a')}</div>
                        <div className='event-list-place'>{location}</div>
                        <div className='event-list-place'>cena: {price}zł</div>
                        <div className='event-list-place'>ilość miejsc: {seats}</div>
                    </div>
                </div>
                <div className="profile-button-event-section">
                    {
                        preview?
                        <Button 
                            sx={{height:"3rem"}}
                            variant="contained" 
                            size='large'
                            onClick={()=>navigate("/event-preview",{state:{preview:event.event?event.event:event}})}
                           
                        >
                            Podgląd
                        </Button>
                        :null
                    }
                    {
                        editAble?
                        <Button 
                            sx={{height:"3rem"}}
                            variant="contained" 
                            size='large'
                            disabled={editBlocked}
                            onClick={()=>navigate("/edit-event",{state:{event:event}})}
                        >
                            Edytuj
                        </Button>
                        :null
                    }
                   
                    {
                        printAble?
                        
                        <Button 
                        sx={{height:"3rem"}}
                        variant="contained" 
                        size='large'
                        color="success"
                        onClick={handleClickOpen}
                            >
                                Pokaż bilet
                            </Button>
                        :
                        null
                    }
                </div>
               
                
            </div>
        </React.Fragment>
    )
}