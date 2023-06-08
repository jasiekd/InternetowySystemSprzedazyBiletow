import {render, screen} from '@testing-library/react'
import userEvent from '@testing-library/user-event'
import '@testing-library/jest-dom'
import LocationForm from "../view/AddLocalization"
import { fireEvent } from '@testing-library/react'
const mockedUsedNavigate = jest.fn();

jest.mock('react-router-dom', () => ({
   ...jest.requireActual('react-router-dom'),
  useNavigate: () => mockedUsedNavigate,
}));

jest.mock('../services/AccountService', () => ({
    checkIsLogged: jest.fn().mockReturnValue('1'),
  }));

test('adding new localization',async()=>{

    render(
        <LocationForm/>
    );
    expect(screen.getByTestId('test-app')).toBeInTheDocument();

        fireEvent.change(screen.getByTestId('test-location-title'),{target:{value:"Test Nazwa"}})
        fireEvent.change(screen.getByTestId('test-location-desc'),{target:{value:"Test Opis"}})
        fireEvent.change(screen.getByTestId('test-location-link'),{target:{value:"Test Link"}})

        expect(screen.getByTestId("preview-event-title")).toHaveTextContent("Test Nazwa")
        expect(screen.getByTestId("preview-event-desc")).toHaveTextContent("Test Opis")

        fireEvent.click(screen.getByTestId("test-add-location"))
        expect(Swal.fire).toBeCalled();

    /*},2000);*/
    
    
})