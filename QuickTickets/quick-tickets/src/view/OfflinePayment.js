import * as React from 'react';
import Footer from '../components/Footer';
import "../styles/MainStyle.css";
import "../styles/OrganisersApproval.css";
import { useNavigate } from "react-router-dom";
import Header from '../components/Header';
import "../styles/DropDownMenu.css";
import "../styles/SearchList.css";
import Pagination from '@mui/material/Pagination';
import TransactionController from "../controllers/TransactionController";
import { useEffect, useState } from "react";
import ReCAPTCHA from "react-google-recaptcha";
import moment from 'moment/moment';
import PropTypes from 'prop-types';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';

function ShowUnpaidTickets({GetPendingTransactions,AcceptTransaction,CancelTransaction,GetAllTransactions,showAll}){
    
    const [unpaidTickets,setUnpaidTickets] = useState([]);
    const [pageIndex,setpageIndex] = useState(1);

        useEffect(  () => {

            if(showAll)
            {
                GetAllTransactions(pageIndex,3).then((result)=>{
                    setUnpaidTickets(result);
                });
            }
            else{
                GetPendingTransactions(pageIndex,3).then((result)=>{
                    setUnpaidTickets(result);
                });
            }
           
          

        },[pageIndex]);

   
    const [captcha, setCaptcha] = useState({});

    const showCaptcha = (transactionID) => {
        setCaptcha((prevState) => {
            const newCaptcha = {};
      
            // Zchowaj wszystkie otwarte captche
            Object.keys(prevState).forEach((key) => {
                newCaptcha[key] = false;
            });
            
            // Pokaż captche
            if(transactionID)
            newCaptcha[transactionID] = !prevState[transactionID];
      
            return newCaptcha;
          });

    };
    const payOffline = (transactionID,value) => {
        if(!value) return;
        AcceptTransaction(transactionID).then(()=>{
            GetPendingTransactions(pageIndex,3).then((result)=>{
                setUnpaidTickets(result);
                if(pageIndex>result.totalPages){
                    setpageIndex(result.totalPages);
                }
            });
        })

    };
    const cancelUnpaidTicket = (transactionID) => {
        CancelTransaction(transactionID).then(()=>{
            GetPendingTransactions(pageIndex,3).then((result)=>{
                setUnpaidTickets(result);
                if(pageIndex>result.totalPages){
                    setpageIndex(result.totalPages);
                }
            });
        })

    };
    return(
        <div className='searchList'>                                   
            {
                showAll?
                <div className='title'>Lista wszystkich biletów</div>
                :
                <div className='title'>Lista nieopłaconych biletów</div>
            }
            
                {       unpaidTickets!== undefined &&
                        unpaidTickets.transactions?.map((transaction,index)=>(

                                <div className='organisers-list' style={{boxShadow:" 0 10px 16px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19)"}} key={index}>
                                    <div className='organisers-list-row'>
                                        <h3>Dane:</h3>
                                    </div>
                                    <div className='organisers-list-row'>
                                       E-mail: {transaction.user.email}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Login użytkownika: {transaction.user.login}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Imię i nazwisko: {transaction.user.name + " " + transaction.user.surname}
                                    </div>
                                    <div className='organisers-list-row'>
                                        Data urodzenia: { moment(transaction.user.dateOfBirth).format('MMMM Do YYYY, h:mm:ss a')}
                                    </div>
                                    
                                    <div className='organisers-list-row'>
                                        Cena: {transaction.price} zł
                                    </div>
                                    <div className='organisers-list-row'>
                                        Data transakcji: {moment(transaction.transactionDate).format('MMMM Do YYYY, h:mm:ss a')}
                                    </div>
                                    <div className='organisers-list-row'>
                                        <button className='main-btn2' onClick={()=>showCaptcha(transaction.transactionID)}>Opłać</button >
                                        <button className='cancel-btn' onClick={()=>cancelUnpaidTicket(transaction.transactionID)}>Anuluj</button>
                                    </div>
                                    {captcha[transaction.transactionID] && <ReCAPTCHA sitekey='6LcxKagmAAAAAMoALPGOnG9rmQwJRCNVpdhRQvfm' onChange={(value)=>payOffline(transaction.transactionID,value)}/>}
                                </div>
                            
                        ))
                   
                }  
                {
                    unpaidTickets!== undefined && unpaidTickets.totalPages>0 &&
                    <Pagination count={unpaidTickets?.totalPages} size='large' onChange={(e,v)=>{setpageIndex(v);showCaptcha()}}/>
                }
        </div>
    );

}
function TabPanel(props) {
    const { children, value, index, ...other } = props;
  
    return (
      <div
        role="tabpanel"
        hidden={value !== index}
        id={`simple-tabpanel-${index}`}
        aria-labelledby={`simple-tab-${index}`}
        {...other}
      >
        {value === index && (
          <Box sx={{ p: 3 }}>
            <Typography>{children}</Typography>
          </Box>
        )}
      </div>
    );
  }
  
  TabPanel.propTypes = {
    children: PropTypes.node,
    index: PropTypes.number.isRequired,
    value: PropTypes.number.isRequired,
  };
function a11yProps(index) {
    return {
      id: `simple-tab-${index}`,
      'aria-controls': `simple-tabpanel-${index}`,
    };
  }
export default function OfflinePayment(){
    const [value, setValue] = React.useState(0);

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };
    return(
        <div className="App">
            <Header/>
            <main className='content'>
                <Box sx={{ width: '100%',backgroundColor:"white" }}>
                    <Box sx={{ borderBottom: 1, borderColor: 'white' }}>
                        <Tabs value={value} onChange={handleChange} aria-label="basic tabs example" sx={{backgroundColor:"white"}}>
                        <Tab label="Niezatwierdzone płatności" {...a11yProps(0)} />
                        <Tab label="Wszystkie płatności" {...a11yProps(1)} />
                        </Tabs>
                    </Box>
                    <TabPanel value={value} index={0}>
                        <TransactionController>
                            <ShowUnpaidTickets />
                        </TransactionController>
                    </TabPanel>
                    <TabPanel value={value} index={1}>
                        <TransactionController>
                            <ShowUnpaidTickets
                                showAll={true}
                            />
                        </TransactionController>
                    </TabPanel>
                </Box>
                
            </main>
            <div className='App-footer'>
                <Footer/>
            </div>
        </div>

    );
}