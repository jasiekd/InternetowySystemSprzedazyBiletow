import {fireEvent, render, screen} from '@testing-library/react'
import '@testing-library/jest-dom'
import AddCategory from '../../view/AddCategory';
import { waitFor } from '@testing-library/react'
import axios from 'axios';
import MockAdapter from 'axios-mock-adapter';
const mock = new MockAdapter(axios);

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
   useNavigate: () => jest.fn(),
 }));
 jest.mock('../../controllers/Login',()=>({
    checkIsLogged: ()=>'1',

}))
mock.onPost('https://localhost:7235/api/TypesOfEvents/AddTypeOfEvent').reply(200,
    { typeID: 1, description: "TestName" }


);
test('test adding category form',async()=>{

    render(
        <AddCategory/>
    );

    await waitFor(() => {
        expect(screen.getByTestId("test-category-title")).toBeInTheDocument();
        fireEvent.change(screen.getByTestId("test-category-title"),{target: { value: "TestName" }});
        expect(screen.getByTestId('test-category-title').value).toBe("TestName");
        fireEvent.click(screen.getByText('Dodaj'));
    })
    await waitFor(() => {
        expect(screen.getByTestId('test-category-title').value).toBe('');
    })

})