import {render, screen, waitFor} from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import '@testing-library/jest-dom'
import MostPopularPlaces from '../../components/MostPopularPlaces.js'


jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
   useNavigate: () => jest.fn(),
 }));



test('display popular places',async()=>{

    // mock pobrania 4 elemntów z backjendu
    const mockGetHotEvents = jest.fn(async () => {
        return Promise.resolve([{},{},{},{}]); 
      })

    // renderowanie komponentu
    render(
            <MostPopularPlaces getHotLocations={mockGetHotEvents}/>
    );

    //sprawdzanie czy głowny komponent zostal wyrenderowany
    expect(screen.getByTestId('test-place-content')).toBeInTheDocument();
    
    await waitFor(()=>{
        //testowanie czy wyrenderowano 4 hot events
        expect(screen.getAllByTestId('test-place-event')).toHaveLength(4);
    })


})