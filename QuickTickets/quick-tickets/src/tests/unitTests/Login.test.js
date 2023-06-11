import {fireEvent, render, screen,act} from '@testing-library/react'
import '@testing-library/jest-dom'
import Login from '../../view/Login';
import { waitFor } from '@testing-library/react'

import { GoogleOAuthProvider } from '@react-oauth/google';
import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';

const mock = new MockAdapter(axios);

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
   useNavigate: () => jest.fn(),
 }));


 mock.onPost('https://localhost:7235/api/Account/login').reply(401, 
     { userName: "test", password: "qwerty!2345"}
 );



test('Login test',async()=>{

    render(
        <GoogleOAuthProvider clientId="740211268192-8vmaqh61n6u7tj1k3unmm6a36p8gpf19.apps.googleusercontent.com">
            <Login />
        </GoogleOAuthProvider>
    );


  
    await waitFor(() => {
        expect(screen.getByTestId('test-login')).toBeInTheDocument();
        expect(screen.getByTestId('test-password')).toBeInTheDocument();
        expect(screen.getByText('Zaloguj')).toBeInTheDocument();
    })
    await waitFor(() => {
       
          fireEvent.change(screen.getByTestId('test-login'),{target:{value:"TestLogin"}})
          fireEvent.change(screen.getByTestId('test-password'),{target:{value:"TestPassword"}})

          //próba dodania wydarzenia
          fireEvent.click(screen.getByText('Zaloguj'));
    })
    // await waitFor(() => {
    //     // expect(screen.getByTestId('test-login').getAttribute('error')).toBe(false);
    //     expect(screen.getByTestId('test-password').classList.contains('Mui-error')).toBe(true);


    // })
  
 

})