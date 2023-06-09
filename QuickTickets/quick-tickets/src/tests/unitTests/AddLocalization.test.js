import {render, screen} from '@testing-library/react'
import '@testing-library/jest-dom'
import AddLocalization from "../../view/AddLocalization"
import { fireEvent } from '@testing-library/react'
import { waitFor } from '@testing-library/react'
import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';
const mock = new MockAdapter(axios);
const mockedUsedNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
   ...jest.requireActual('react-router-dom'),
  useNavigate: () => mockedUsedNavigate,
}));

//mock kontrolo dostępu
jest.mock('../../controllers/Login',()=>({
    checkIsLogged: ()=>'1',

}))

//mock żądania na bezkend z statusem odpowiedzi 201
 mock.onPost('https://localhost:7235/api/Locations/addLocation').reply(201, {
  message: 'Success',
  data: { /* dane odpowiedzi */ },
});


test('adding new localization',async()=>{

  //renderowanie testowanego komponentu
    render(
        <AddLocalization />
    );
 
    
     await waitFor(() => {
      //wpisywanie do pol testowych wartosci
        fireEvent.change(screen.getByTestId('test-location-title'),{target:{value:"TestNazwa"}})
        fireEvent.change(screen.getByTestId('test-location-desc'),{target:{value:"TestOpis"}})
        fireEvent.change(screen.getByTestId('test-location-link'),{target:{value:"TestLink"}})

        //próba dodania lokalizacji
        fireEvent.click(screen.getByText('Dodaj'));
     })

     await waitFor(() => {
      //sprawdzanie czy po dodaniu pola zostały wyczyszczone
        expect(screen.getByTestId('test-location-title').value).toBe('');
        expect(screen.getByTestId('test-location-desc').value).toBe('');
        expect(screen.getByTestId('test-location-link').value).toBe('');
      });
})