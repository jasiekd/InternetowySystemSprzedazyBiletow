import React from "react";
import SummaryBuy from "../components/SummaryBuy";
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import exampleEvent from "../images/example-event.png";
export default function PurchasedTicket(){
    const [open, setOpen] = React.useState(false);
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
                            <SummaryBuy/>
                        </div>
                        
                    </DialogContentText>
                    </DialogContent>
                    <DialogActions>
                        <button onClick={handleClose} className="main-btn">Zamknij</button>
                    </DialogActions>
            </Dialog>

            <div className='purchased-on-list'>
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
        </React.Fragment>
    )
}