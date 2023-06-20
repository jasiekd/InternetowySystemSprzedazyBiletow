import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import { useLocation, useNavigate } from "react-router-dom";
import Header from '../components/Header';
import "../styles/BuyTicket.css";
import Box from '@mui/material/Box';
import Stepper from '@mui/material/Stepper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';
import Typography from '@mui/material/Typography';
import ChooseTicket from '../components/ChooseTicket';
import FillInData from '../components/FillInData';
import Pay from '../components/Pay';
import SummaryBuy from '../components/SummaryBuy';
import TransactionController from '../controllers/TransactionController';
import LoginController from '../controllers/Login';



export default function BuyTicket({getMyTicketByID,GetStatusTransaction}) {
    const [ticketsCounter,setTicketCounter] = React.useState(1);
    const [eventData,setEventData] = React.useState();
    const [ticketData,setTicketData] = React.useState();
    const location = useLocation();
    const steps = ['Wybierz bilety', 'Dane użytkownika', 'Zapłać','Podsumowanie'];
    const buyPages = [
        <ChooseTicket eventData={eventData} counter={ticketsCounter} setCounter={setTicketCounter}/>,
        <LoginController>
            <FillInData eventData={eventData}/>
        </LoginController>,
        <TransactionController>
            <Pay eventData={eventData} counter={ticketsCounter}/>
        </TransactionController>,
        <LoginController>
            <SummaryBuy eventData={ticketData}/>
        </LoginController>
    ];

    const navigate = useNavigate();
    const [activeStep, setActiveStep] = React.useState(0);
    const [skipped, setSkipped] = React.useState(new Set());

    

    const isStepSkipped = (step) => {
        return skipped.has(step);
    };

    const handleNext = () => {
        let newSkipped = skipped;
        if (isStepSkipped(activeStep)) {
            newSkipped = new Set(newSkipped.values());
            newSkipped.delete(activeStep);
        }
        setActiveStep((prevActiveStep) => prevActiveStep + 1);
        setSkipped(newSkipped);
    };

    const handleBack = () => {
        setActiveStep((prevActiveStep) => prevActiveStep - 1);
    };

    const handleSkip = () => {
    
    setActiveStep((prevActiveStep) => prevActiveStep + 1);
    setSkipped((prevSkipped) => {
        const newSkipped = new Set(prevSkipped.values());
        newSkipped.add(activeStep);
        return newSkipped;
        });
    };

    const handleReset = () => {
        setActiveStep(0);
    };

    React.useEffect(() => {
        if(activeStep===4)
        {
            navigate("/home")
        }
      },[activeStep]);

      React.useEffect(()=>{
        if(location.state === null){
            const urlParams = new URLSearchParams(window.location.search);
            const id = urlParams.get('id');
            if(id)
            {
                setActiveStep(3);
                GetStatusTransaction(id).then((r)=>{
                    if(r){
                        getMyTicketByID(id).then(r=>{
                            setTicketData(r);
                        })
                    }
                })
               
               
            }
            else
            {
                navigate('/home')
            }
            
        }
        else{
            if(location.state.id)
            {
                console.log(location.state.id)
                setActiveStep(3);
            }else{
                setEventData(location.state.event)
            }
            
        }
      },[])
    return (

        <div className="App">
            <Header/>
        <main className='content'>
            <div className='buy-ticket-menu'>
                <Box sx={{m:5}}>
                    <Stepper activeStep={activeStep}>
                        {steps.map((label, index) => {
                            const stepProps = {};
                            const labelProps = {};
                        
                            if (isStepSkipped(index)) {
                                stepProps.completed = false;
                            }
                            return (
                                <Step 
                                    key={label} {...stepProps}
                                    sx={{
                                    '& .MuiStepLabel-root .Mui-completed': {
                                      color: '#93BB60', // circle color (COMPLETED)
                                    },
                                    '& .MuiStepLabel-root .Mui-active': {
                                        color: '#60bb62', // circle color (ACTIVE)
                                    }
                                    }}>
                                        <StepLabel {...labelProps}>{label}</StepLabel>
                                </Step>
                            );
                        })}
                    </Stepper>
                   
                        <React.Fragment>
                        <Typography> 
                            <div className='buy-ticket-content'>
                                {
                                    buyPages[activeStep]
                                }
                            </div>
                        </Typography>
                        <Box sx={{ display: 'flex', flexDirection: 'row', pt: 2 }}>
                            {
                                activeStep===3||activeStep===0?
                                    null
                                :
                                    <button className='main-btn' disabled={activeStep === 0||activeStep===3} onClick={handleBack}>
                                        Wróć
                                    </button>
                            }
                           
                            <Box sx={{ flex: '1 1 auto' }} />
                            {
                                activeStep === 2?
                                null
                                :
                                <button className='main-btn' onClick={handleNext}>
                                {activeStep === steps.length - 1 ? 'Zakończ' : 'Dalej'}
                            </button>
                            }
                           
                        </Box>
                        </React.Fragment>
                    </Box>
           

            </div>
        </main>
        <div className='App-footer'>
            <Footer/>
          </div>
      </div>
    );
}