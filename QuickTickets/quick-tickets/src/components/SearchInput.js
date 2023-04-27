import * as React from 'react';
import { styled } from '@mui/material/styles';
import TextField from '@mui/material/TextField';
import '../styles/SearchInput.css';
import searchIcon from '../images/search-icon.png';
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
    return(
        <div className='search-content'>
            <InputStyled id="filled-basic" label="Szukaj wydarzenia..." variant="outlined" fullWidth/>
            <button className='search-button'>
                <img src={searchIcon}/>
            </button>
        </div>
    );
  }