import {FormEvent} from "react";
import { IRegisterData } from "../../dto/IRegisterData";
import {IGender} from "../../domain/IGender";
import {IFormHandler} from "../../helpers/IFormHandler";
import {Input} from "../../components/Input/Input";
import {SelectGender} from "../../components/Input/SelectGender";
import {ErrorList} from "../../components/ErrorList";
import Form from 'react-bootstrap/Form';
import {Button} from "react-bootstrap";

interface IProps {
    values: IRegisterData;

    genders: IGender[],

    validationErrors: string[];

    handleChange: IFormHandler;

    onSubmit: (event: FormEvent<HTMLFormElement>) => void;

    validated: boolean;
}

const RegisterFormView = (props: IProps) => {
    return (
        <Form  className="formInput" noValidate validated={props.validated} onSubmit={(e) => props.onSubmit(e)}>
            <h2>Create a new account.</h2>
            <hr />

            <ErrorList errors={ props.validationErrors } />

            <Input label="Email" type="email" id="Input_Email" name="email" value={props.values.email}
                   placeholder="name@example.com" changeHandler={props.handleChange} autoComplete="email"
                   pattern="^.{1,}$" errorMsg="Invalid email" required={true}/>

            <Input label="Password" type="password" id="Input_Password" name="password" value={props.values.password}
                   placeholder="password" changeHandler={props.handleChange} autoComplete="new-password"
                   pattern="^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\\w\\s]).{6,}$"
                   errorMsg="Password must contain at least: upper case, lower case, number, special char, lenght of 6"
                   required={true}/>

            <Input label="Confirm Password" type="password" id="Input_ConfirmPassword" name="confirmPassword" value={props.values.confirmPassword}
                   placeholder="password" changeHandler={props.handleChange} autoComplete="new-password"
                   pattern={props.values.password} errorMsg="Passwords doesn't match" required={true}/>

            <Input label="First name" type="text" id="Input_FirstName" name="firstName" value={props.values.firstName}
                   placeholder="First Name" changeHandler={props.handleChange} autoComplete="given-name"
                   pattern="^.{1,}$" errorMsg="Can't be empty" required={true}/>

            <Input label="Last name" type="text" id="Input_LastName" name="lastName" value={props.values.lastName}
                   placeholder="Last Name" changeHandler={props.handleChange} autoComplete="family-name"
                   pattern="^.{1,}$" errorMsg="Can't be empty" required={true}/>

            <SelectGender label="Choose gender" id="Input_Gender" name="gender" value={props.values.gender}
                          changeHandler={props.handleChange} genders={props.genders}
                          placeholder="Gender" errorMsg="Must choose gender" required={true}/>

            <Input label="Date of birth" type="date" id="Input_birthDate" name="birthDate" value={props.values.birthDate}
                   placeholder="" changeHandler={props.handleChange} autoComplete=""
                   pattern="" errorMsg="Can't be empty" required={true}/>

            <Button
                type="submit" id="registerSubmit" className="w-100 btn btn-lg btn-primary">Register</Button>
        </Form>
    );
}

export default RegisterFormView;
