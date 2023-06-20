import * as React from 'react';
import PropTypes from 'prop-types';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import { GreenInput } from './GreenInput';


import '../styles/ProfileTabs.css'
import moment from 'moment';
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';
import EventComponent from './EventComponent';
import OrganisatorEventsTab from './OrganisatorEventsTab';
import EventsController from '../controllers/Events';
import { checkIsLogged } from '../controllers/Login';
import TicketController from '../controllers/TicketController';
import ProfileTicketsList from './ProfileTicketsList';
function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`vertical-tabpanel-${index}`}
      aria-labelledby={`vertical-tab-${index}`}
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
    id: `vertical-tab-${index}`,
    'aria-controls': `vertical-tabpanel-${index}`,
  };
}

export default function ProfileTabs({getUser,updateAccount}) {
  const [value, setValue] = React.useState(0);
  const [disableEditForm,setDisableUserForm] = React.useState(true);
  const navigate = useNavigate()
  const handleChange = (event, newValue) => {
    setValue(newValue);
  };
  
    const [userData,setUserData] = React.useState({
        name: "",
        surname: "",
        email: "",
        login: "",
        password: "",
        dateOfBirth:""
    })
    const [formRegex,setFormRegex] = React.useState({
      emailText: "",
      emailAlert: false,
      loginText: "",
      loginAlert: false,
      passwordText: "",
      passwordAlert: false,
  })

    React.useEffect(()=>{
        getUser().then(r=>{
            if(r === undefined){
              navigate("/home")
            }
            else
            {
              setUserData(prev=>({
                ...prev,
                name: r.name,
                surname: r.surname,
                email: r.email,
                login: r.login,
                dateOfBirth: moment(r.dateOfBirth).format('YYYY-MM-DD')
            }))
            }
            
        })
    },[])

    const handleChangePassword = (text) =>{
      const regex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/;

    if (regex.test(text)) {
      setFormRegex(prev=>({
        ...prev,
        passwordAlert:false,
        passwordText:""
      }))
    } else {
      setFormRegex(prev=>({
        ...prev,
        passwordAlert:true,
        passwordText:"Hasło musi zawieraż conajmniej: jedną dużą i małą litere, liczbe oraz 8 znaków "
      }))
    }
      setUserData(prev=>({...prev,password:text}))
    }

    const handleChangeEmail = (email) =>{
      const regex = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i;
    if (regex.test(email)) {
      setFormRegex(prev=>({
        ...prev,
        emailAlert:false,
        emailText:""
      }))
    } else {
      setFormRegex(prev=>({
        ...prev,
        emailAlert:true,
        emailText:"Nie poprawny format adresu email"
      }))
    }
      setUserData(prev=>({...prev,email:email}))
    }

    const handleChangeLogin = (login) =>{
      const regex = /^[a-zA-Z0-9._-]{5,}$/;
    if (regex.test(login)) {
      setFormRegex(prev=>({
        ...prev,
        loginAlert:false,
        loginText:""
      }))
    } else {
      setFormRegex(prev=>({
        ...prev,
        loginAlert:true,
        loginText:"Login musi mieć minimum 5 znaków"
      }))
    }
      setUserData(prev=>({...prev,login:login}))
    }
  return (
    <Box
      sx={{ flexGrow: 1, bgcolor: 'background.paper', display: 'flex',height:'35rem',overflow:"auto" }}
    >
      <Tabs
        orientation="vertical"
        variant="scrollable"
        value={value}
        onChange={handleChange}
        aria-label="Vertical tabs example"
        sx={{ borderRight: 1, borderColor: 'divider' }}
      >
        <Tab label="Informacje o profilu" {...a11yProps(0)} />
        <Tab label="Aktywne bilety" {...a11yProps(1)} />
        <Tab label="Nieaktywne bilety" {...a11yProps(2)} />
        {
          checkIsLogged()==="3"?<Tab label="Organizowane wydarzenia" {...a11yProps(3)} />:null
        }
        
      </Tabs>
      <TabPanel value={value} index={0}>
        <div className='edit-list'>
            <GreenInput disabled={disableEditForm} value={userData.name} label="Imie" fullWidth type="text" onChange={(e)=>setUserData(prev=>({...prev,name:e.target.value}))}></GreenInput>
            <GreenInput disabled={disableEditForm} value={userData.surname} label="Nazwisko" fullWidth type="text" onChange={(e)=>setUserData(prev=>({...prev,surname:e.target.value}))}></GreenInput>
            <GreenInput error={formRegex.emailAlert} helperText={formRegex.emailText} disabled={disableEditForm} value={userData.email} label="Email" fullWidth type="email" onChange={(e)=>handleChangeEmail(e.target.value)}></GreenInput>
            <GreenInput error={formRegex.loginAlert} helperText={formRegex.loginText} disabled={disableEditForm} value={userData.login} label="Login" fullWidth type="text" onChange={(e)=>handleChangeLogin(e.target.value)}></GreenInput>
            <GreenInput error={formRegex.passwordAlert} helperText={formRegex.passwordText} disabled={disableEditForm} value={userData.password} label="Hasło" fullWidth type="password" onChange={(e)=>handleChangePassword(e.target.value)}></GreenInput>
            <GreenInput disabled={disableEditForm} value={userData.dateOfBirth} label="Data Urodzenia" fullWidth type="date" onChange={(e)=>setUserData(prev=>({...prev,dateOfBirth:e.target.value}))}></GreenInput>
            <div className='user-profile-buttons'>
              <Button 
                variant="contained" 
                size="large"
                onClick={()=>setDisableUserForm(!disableEditForm)}
              >
                {
                  !disableEditForm?
                  "Anuluj"
                  :
                  "Edytuj"
                }
                
              </Button>
              <Button 
                variant="contained" 
                size="large" 
                color="success"
                onClick={()=>updateAccount(userData,formRegex)}
              >
                Zapisz
              </Button>
            </div>
           
        </div>
                                
      </TabPanel>
      <TabPanel value={value} index={1} >
        <div className='purchased-list'>
          <TicketController>
            <ProfileTicketsList
              choice={true}
              printAble={true}
            />
          </TicketController>
        </div>
           
            
        </TabPanel>
      <TabPanel value={value} index={2}>
      <div className='purchased-list'>
        <TicketController>
            <ProfileTicketsList
              choice={false}
              
            />
          </TicketController>
        </div>
      </TabPanel>
      {
        checkIsLogged()==="3"?
        <TabPanel value={value} index={3}>
          <div className='purchased-list'>
            <EventsController>
              <OrganisatorEventsTab/>
            </EventsController>
          </div>                  
        </TabPanel>
        :
        null
      }
      
    </Box>
  );
}