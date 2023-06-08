import * as React from 'react';
import PropTypes from 'prop-types';
import Tabs from '@mui/material/Tabs';
import Tab from '@mui/material/Tab';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import { GreenInput } from './GreenInput';
import PurchasedTicket from './PurchasedTicket';
import { Pagination } from '@mui/material';
import '../styles/ProfileTabs.css'
import moment from 'moment';
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

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };
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
    const [userData,setUserData] = React.useState({
        name: "",
        surname: "",
        email: "",
        login: "",
        password: "",
        dateOfBirth:""
    })
    React.useEffect(()=>{
        getUser().then(r=>{
          
            setUserData(prev=>({
                ...prev,
                name: r.name,
                surname: r.surname,
                email: r.email,
                login: r.login,
                dateOfBirth: moment(r.dateOfBirth).format('YYYY-MM-DD')
            }))
        })
    },[])
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
        <Tab label="Przedawnione bilety" {...a11yProps(2)} />
        <Tab label="Moje wydarzenia" {...a11yProps(3)} />
      </Tabs>
      <TabPanel value={value} index={0}>
        <div className='edit-list'>
            <GreenInput value={userData.name} label="Imie" fullWidth type="text" onChange={(e)=>setUserData(prev=>({...prev,name:e.target.value}))}></GreenInput>
            <GreenInput value={userData.surname} label="Nazwisko" fullWidth type="text" onChange={(e)=>setUserData(prev=>({...prev,surname:e.target.value}))}></GreenInput>
            <GreenInput value={userData.email} label="Email" fullWidth type="email" onChange={(e)=>setUserData(prev=>({...prev,email:e.target.value}))}></GreenInput>
            <GreenInput value={userData.login} label="Login" fullWidth type="text" onChange={(e)=>setUserData(prev=>({...prev,login:e.target.value}))}></GreenInput>
            <GreenInput value={userData.password} label="Hasło" fullWidth type="password" onChange={(e)=>setUserData(prev=>({...prev,password:e.target.value}))}></GreenInput>
            <GreenInput value={userData.dateOfBirth} label="Data Urodzenia" fullWidth type="date" onChange={(e)=>setUserData(prev=>({...prev,dateOfBirth:e.target.value}))}></GreenInput>
            <button className="main-btn" type="submit" onClick={()=>updateAccount(userData)}>Zmień</button>
        </div>
                                
      </TabPanel>
      <TabPanel value={value} index={1} >
        <div className='purchased-list'>
            {
                eventList.map((key,val)=>{
                    return(
                        <PurchasedTicket/>
                    )
                    
                })
            }
            <Pagination count={10} size='large'/>
        </div>
           
            
        </TabPanel>
      <TabPanel value={value} index={2}>
      <div className='purchased-list'>
            {
                eventList.map((key,val)=>{
                    return(
                        <PurchasedTicket/>
                    )
                    
                })
            }
            <Pagination count={10} size='large'/>
        </div>
      </TabPanel>
      <TabPanel value={value} index={3}>
        Item Four
      </TabPanel>
    </Box>
  );
}