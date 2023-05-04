import axios from "axios";
import { HostName } from "./HostName";

export default class RegisterService{
    async register(data)
    {
        console.log(data);
        try{
            const response = await axios.post(HostName+'/api/Account/register',
            {
                name: data.nameRegVal,
                surname: data.surnameRegVal,
                email: data.emailRegVal,
                login: data.loginRegVal,
                password: data.passwordRegVal,
                dateOfBirth: data.dateOfBirthRegVal
            })

            return response;
        }
        catch(error){
            return error.response;
        }
    }
}