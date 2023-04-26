import * as React from 'react';
import { styled } from '@mui/material/styles';
import TextField from '@mui/material/TextField';


export const GreenInput = styled(TextField)({
    '& label.Mui-focused': {
      color: '#93BB60',
    },
    '& .MuiInput-underline:after': {
      borderBottomColor: '#93BB60',
    },
    '& .MuiOutlinedInput-root': {
      '& fieldset': {
        borderColor: '',
      },
      '&:hover fieldset': {
        borderColor: 'green',
      },
      '&.Mui-focused fieldset': {
        borderColor: '#93BB60',
      },
    },
  });