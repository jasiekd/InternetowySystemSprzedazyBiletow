import {fireEvent, render, screen} from '@testing-library/react'
import '@testing-library/jest-dom'
import AddAdmin from '../../view/AddAdmin';
import { waitFor } from '@testing-library/react'
import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';
import { AddAdminForm } from '../../view/AddAdmin';
const mock = new MockAdapter(axios);

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
   useNavigate: () => jest.fn(),
 }));
 jest.mock('../../controllers/Login',()=>({
    checkIsLogged: ()=>'1',

}))

test('test adding new event',async()=>{

    const mockAddAdmin = jest.fn(async () => {
        return Promise.resolve([]); 
      })
    render(
        <AddAdminForm AddAdmin={mockAddAdmin} />
    );

  
    

          fireEvent.change(screen.getByLabelText('Login'),{target:{value:"TestLogin"}})
          fireEvent.change(screen.getByLabelText('Hasło'),{target:{value:"TestHaslo"}})
          fireEvent.change(screen.getByLabelText('Email'),{target:{value:"TestEmail"}})
          fireEvent.change(screen.getByLabelText('Imię'),{target:{value:"TestImię"}})
          fireEvent.change(screen.getByLabelText('Nazwisko'),{target:{value:"TestNazwisko"}})
          fireEvent.change(screen.getByLabelText('Data urodzenia'),{target:{value:"2000-01-01"}})
          
        
          fireEvent.click(screen.getByText('Zarejestruj'));
       
          await waitFor(() => {
            expect(screen.getByLabelText('Login').value).toBe('');
            expect(screen.getByLabelText('Hasło').value).toBe('');
            expect(screen.getByLabelText('Email').value).toBe('');
            expect(screen.getByLabelText('Imię').value).toBe('');
            expect(screen.getByLabelText('Nazwisko').value).toBe('');
            expect(screen.getByLabelText('Data urodzenia').value).toBe('');

          })
})