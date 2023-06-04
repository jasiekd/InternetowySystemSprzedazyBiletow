import * as React from 'react';
import { styled } from '@mui/material/styles';
import TextField from '@mui/material/TextField';
import '../styles/SearchInput.css';
import searchIcon from '../images/search-icon.png';
import { useNavigate } from "react-router-dom";

const InputStyled = styled(TextField)({

    '& label': {
      color: '#939393',

      fontWeight: "600"
    },
    '& label.Mui-focused': {
        color: '#939393',
      },
    
    '& .MuiInput-underline:after': {
      borderBottomColor: '#red',

    },
    '& .MuiOutlinedInput-root': {
        background: "#E7E7E7",
      '& fieldset': {
        border: "none",

      },
    },
  });

export default function SearchInput() {
  const [phrase,setPhrase] = React.useState("");
  const navigate = useNavigate();
    return(
        <div className='search-content'>
            <InputStyled 
              id="filled-basic" 
              label="Szukaj wydarzenia..." 
              variant="outlined" 
              fullWidth 
              value={phrase}
              onChange={(e)=>setPhrase(e.target.value)}
            />
            <button className='search-button' onClick={()=>navigate("/search-list",{state:{phrase:phrase,location:null}})}>
                <img src={searchIcon}/>
            </button>
        </div>
    );
  }