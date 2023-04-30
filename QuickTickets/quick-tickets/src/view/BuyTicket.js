import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import exampleEvent from "../images/example-event.png";
import "../styles/BuyTicket.css";
import Box from '@mui/material/Box';
import Stepper from '@mui/material/Stepper';
import Step from '@mui/material/Step';
import StepLabel from '@mui/material/StepLabel';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import ChooseTicket from '../components/ChooseTicket';
import FillInData from '../components/FillInData';
import Pay from '../components/Pay';
import SummaryBuy from '../components/SummaryBuy';



export default function BuyTicket() {
    const [ticketsCounter,setTicketCounter] = React.useState(0);

    const steps = ['Wybierz bilety', 'Uzupełnij dane', 'Zapłać','Podsumowanie'];
    const buyPages = [<ChooseTicket counter={ticketsCounter} setCounter={setTicketCounter}/>,<FillInData/>,<Pay counter={ticketsCounter}/>,<SummaryBuy/>];

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
                            <button className='main-btn' disabled={activeStep === 0} onClick={handleBack}>
                                Wróć
                            </button>
                            <Box sx={{ flex: '1 1 auto' }} />
                            <button className='main-btn' onClick={handleNext}>
                                {activeStep === steps.length - 1 ? 'Zakończ' : 'Dalej'}
                            </button>
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