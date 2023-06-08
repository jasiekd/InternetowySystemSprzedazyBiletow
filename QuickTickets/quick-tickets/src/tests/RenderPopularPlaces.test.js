import {render, screen} from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import '@testing-library/jest-dom'
import MostPopularPlaces from '../components/MostPopularPlaces.js'
import EventsController from '../controllers/Events.js'

test('display popular places',async()=>{

    render(
        <EventsController>
            <MostPopularPlaces/>
        </EventsController>
    );

    expect(screen.getByTestId('test-place-content')).toBeInTheDocument();
    setTimeout(()=>expect(screen.getByTestId('test-place-event')).toBeInTheDocument(),200);
})