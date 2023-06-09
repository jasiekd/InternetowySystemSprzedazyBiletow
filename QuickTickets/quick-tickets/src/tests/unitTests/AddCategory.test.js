import {fireEvent, render, screen} from '@testing-library/react'
import '@testing-library/jest-dom'
import AddCategory from '../../view/AddCategory';

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
   useNavigate: () => jest.fn(),
 }));
 jest.mock('../../controllers/Login',()=>({
    checkIsLogged: ()=>'1',

}))

test('test adding category form',async()=>{

    render(
        <AddCategory/>
    );

    expect(screen.getByTestId("test-category-title")).toBeInTheDocument();
    fireEvent.change(screen.getByTestId("test-category-title"),{target: { value: "TestName" }});
    expect(screen.getByTestId('test-category-title').value).toBe("TestName");

})