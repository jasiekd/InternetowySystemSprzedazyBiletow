import * as React from 'react';
import "../styles/MainStyle.css";
import { useNavigate } from "react-router-dom";
import logo from "../images/logo.png";
import '../styles/Header.css';
import TextField from '@mui/material/TextField';
import  SearchInput  from './SearchInput';
import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import Logout from '@mui/icons-material/Logout';
import { checkIsLogged,logOut } from '../controllers/Login';
import Swal from 'sweetalert2';
import OrganiserApplicationController from "../controllers/OrganiserApplicationController";

function BecomeOrganiser({addOrganiserApplication}){

    const onaddOrganiserApplication = () =>{
        Swal.fire({
            input: 'textarea',
            inputLabel: 'Zostań organizatorem',
            inputPlaceholder: 'Wpisz dane organizatora',
            inputAttributes: {
              'aria-label': 'Type your message here'
            },
            confirmButtonText: 'Aplikuj',
            confirmButtonColor: '#93BB60',
            cancelButtonText: 'Anuluj',
            showCancelButton: true,
            showCloseButton: true
          }).then((result) => 
          {
            if (!result.value) return;
                const description = result.value;
                addOrganiserApplication(description);
        })
    }


    return(
        <MenuItem onClick={()=>onaddOrganiserApplication()}>
            Zostań organizatorem
        </MenuItem>
    );
}

export default function Header({isLogged}) {
    const navigate = useNavigate();
    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);

    const handleClick = (event) => {
      setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
      setAnchorEl(null);
    };
    const onLogOut =()=>{
        logOut();
        navigate('/home');
    }
    return (
        <header className="App-header">
                <img className='main-logo' onClick={()=>navigate("/home")} src={logo}/>
                
                <div style={{width:"40rem"}}>
                    <SearchInput />  
                </div>
                {
                    
                    checkIsLogged()?
                    <React.Fragment>
                        <Box sx={{ display: 'flex', alignItems: 'center', textAlign: 'center' }}>
                            <Tooltip title="Account settings">
                            <IconButton
                                onClick={handleClick}
                                size="small"
                                sx={{ ml: 2 }}
                                aria-controls={open ? 'account-menu' : undefined}
                                aria-haspopup="true"
                                aria-expanded={open ? 'true' : undefined}
                            >
                                <Avatar sx={{ width: 32, height: 32 }}>D</Avatar>
                            </IconButton>
                            </Tooltip>
                        </Box>
                        <Menu
                            anchorEl={anchorEl}
                            id="account-menu"
                            open={open}
                            onClose={handleClose}
                            onClick={handleClose}
                            PaperProps={{
                            elevation: 0,
                            sx: {
                                overflow: 'visible',
                                filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
                                mt: 1.5,
                                '& .MuiAvatar-root': {
                                width: 32,
                                height: 32,
                                ml: -0.5,
                                mr: 1,
                                },
                                '&:before': {
                                content: '""',
                                display: 'block',
                                position: 'absolute',
                                top: 0,
                                right: 14,
                                width: 10,
                                height: 10,
                                bgcolor: 'background.paper',
                                transform: 'translateY(-50%) rotate(45deg)',
                                zIndex: 0,
                                },
                            },
                            }}
                            transformOrigin={{ horizontal: 'right', vertical: 'top' }}
                            anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
                        >

                            <MenuItem onClick={()=>navigate("/user-profile")}>
                                Profil
                            </MenuItem>
                            {
                                checkIsLogged()==="2"?
                                <OrganiserApplicationController>
                                    <BecomeOrganiser />
                                </OrganiserApplicationController> 
                                :
                                null
                            }
                            
                            
                            {
                                checkIsLogged()==="3"?
                                <>
                                    <MenuItem onClick={()=>navigate("/add-event")}>
                                        Dodaj wydarzenie
                                    </MenuItem>
                                </>
                                :
                                null
                            }
                            
                            {
                                checkIsLogged()==="1"?
                                <>
                                    <MenuItem onClick={()=>navigate("/add-localization")}>
                                        Dodaj lokalizacje
                                    </MenuItem>
                                    <MenuItem onClick={()=>navigate("/add-category")}>
                                        Dodaj kategorie
                                    </MenuItem>
                                    <MenuItem onClick={()=>navigate("/add-admin")}>
                                        Dodaj administratora
                                    </MenuItem>
                                    <MenuItem onClick={()=>navigate("/organisers-approval")}>
                                        Zatwierdzanie organizatorów
                                    </MenuItem>
                                    <MenuItem onClick={()=>navigate("/events-approval")}>
                                        Zatwierdzanie wydarzeń
                                    </MenuItem>
                                    <MenuItem onClick={()=>navigate("/offline-payment")}>
                                        Opłacanie biletów offline
                                    </MenuItem>
                                    <MenuItem onClick={()=>navigate("/show-users")}>
                                        Lista użytkowników strony
                                    </MenuItem>
                                </>
                                :
                                null
                            }
                            <Divider />
                        
                            <MenuItem onClick={()=>onLogOut()}>
                            <ListItemIcon>
                                <Logout fontSize="small" />
                            </ListItemIcon>
                            Logout
                            </MenuItem>
                        </Menu>
                    </React.Fragment>
                    :
                    <button className='main-btn login-nav-login' onClick={()=>navigate("/login")}>Zaloguj</button>
                }
                
                
        </header>
    )
}

