import {fireEvent, render, screen,act} from '@testing-library/react'
import '@testing-library/jest-dom'
import AddEvent from '../../view/AddEvent';
import { waitFor } from '@testing-library/react'
import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';

const mock = new MockAdapter(axios);

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
   useNavigate: () => jest.fn(),
 }));
 jest.mock('../../controllers/Login',()=>({
    checkIsLogged: ()=>'3',

}))

mock.onGet('https://localhost:7235/api/Locations').reply(200, 
[
    { locationID: 1, name: "Kielce", description: "opis1",imgURL:"a"},
    { locationID: 2, name: "Gdańsk", description: "opis2",imgURL:"a"}
]

);
mock.onGet('https://localhost:7235/api/TypesOfEvents').reply(200,
  
[
    { typeID: 1, description: "Koncert" }, 
    { typeID: 2, description: "Teatr" }, 
    { typeID: 3, description: "Sport" }
]

);


test('test adding new event',async()=>{

    render(
        <AddEvent />
    );


  
    await waitFor(() => {
        expect(screen.getByTestId('buttonn')).toBeInTheDocument();
        expect(screen.getByTestId('buttonn2')).toBeInTheDocument();

        screen.debug();
        fireEvent.click(screen.getByTestId('buttonn'));
        fireEvent.click(screen.getByTestId('buttonn2'));
    })
    await waitFor(() => {

        fireEvent.click(screen.getByText('Kielce'));
        fireEvent.click(screen.getByText('Koncert'));

        //wpisywanie do pol testowych wartosci
        fireEvent.change(screen.getByTestId('test-event-title'),{target:{value:"TestNazwa"}})
          fireEvent.change(screen.getByTestId('test-event-seats'),{target:{value:10}})
          fireEvent.change(screen.getByTestId('test-event-price'),{target:{value:100}})
          fireEvent.change(screen.getByTestId('test-event-description'),{target:{value:"TestOpis"}})
          fireEvent.change(screen.getByTestId('test-event-date'),{target:{value:"2022-04-17T15:30"}})
          fireEvent.change(screen.getByTestId('test-event-img'),{target:{value:"TestLink"}})
  
          fireEvent.click(screen.getByTestId('test-event-adultsOnly'));

          //próba dodania wydarzenia
          fireEvent.click(screen.getByText('Dodaj'));
    })
  
       await waitFor(() => {
        //sprawdzanie czy po dodaniu pola zostały wyczyszczone
          expect(screen.getByTestId('test-event-title').value).toBe('');
          expect(screen.getByTestId('test-event-seats').value).toBe('0');
          expect(screen.getByTestId('test-event-price').value).toBe('0');
          expect(screen.getByTestId('test-event-description').value).toBe('');
          expect(screen.getByTestId('test-event-date').value).toBe('');
          expect(screen.getByTestId('test-event-img').value).toBe('');
          expect(screen.getByTestId('test-event-adultsOnly').value).toBe("on");
          expect(screen.getByText('Miejsce'));
          expect(screen.getByText('Kategoria'));
          
        });

})