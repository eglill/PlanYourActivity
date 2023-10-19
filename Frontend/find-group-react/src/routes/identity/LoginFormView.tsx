import {FormEvent} from "react";
import { ILoginData } from "../../dto/ILoginData";
import './login.css';
import {Input} from "../../components/Input/Input";
import {IFormHandler} from "../../helpers/IFormHandler";
import Form from "react-bootstrap/Form";
import {ErrorList} from "../../components/ErrorList";
import {Button} from "react-bootstrap";
import {CheckBox} from "../../components/Input/CheckBox";

interface IProps {
    values: ILoginData;

    validationErrors: string[];

    handleChange: IFormHandler;

    onSubmit: (event: FormEvent<HTMLFormElement>) => void;

    validated: boolean;
}

const LoginFormView = (props: IProps) => {
    return (
        <Form className="formInput form-signin w-100 m-auto" noValidate validated={props.validated} onSubmit={(e) => props.onSubmit(e)}>
            <h2>Login</h2>
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

            <CheckBox label="Trust This Device" id="Input_Persist" name="persist"
                      changeHandler={props.handleChange} checked={props.values.persist} />

            <Button
                type="submit"
                id="registerSubmit"
                className="w-100 btn btn-lg btn-primary">Login</Button>
        </Form>
    );
}

export default LoginFormView;
