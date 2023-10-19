import {FormEvent, useState} from "react";
import { ILoginData } from "../../dto/ILoginData";
import { IdentityService } from "../../services/IdentityService";
import LoginFormView from "./LoginFormView";
import { useNavigate } from "react-router-dom";
import {IFormHandler} from "../../helpers/IFormHandler";
import useAuth from "../../hooks/useAuth";
import useAxiosPublic from "../../hooks/useAxiosPublic";

const Login = () => {
    const navigate = useNavigate();

    const [values, setInput] = useState({
        email: "",
        password: "",
        persist: false
    } as ILoginData);

    const [validationErrors, setValidationErrors] = useState([] as string[]);

    const handleChange: IFormHandler = (target) => {
        if (target.type === "checkbox") {
            setInput({ ...values, [target.name]: (target as EventTarget & HTMLInputElement).checked });
            return;
        }

        setInput({ ...values, [target.name]: target.value });
    }

    const { setJwtResponse} = useAuth();

    const axiosPublic = useAxiosPublic();
    const identityService = new IdentityService(axiosPublic);

    const [validated, setValidated] = useState(false);

    const onSubmit = async (event: FormEvent<HTMLFormElement>) => {
        const form = event.currentTarget;

        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        } else {
            event.preventDefault();
            if (values.email.length === 0 || values.password.length === 0) {
                setValidationErrors(["Bad input values!"]);
                return;
            }
            // remove errors
            setValidationErrors([]);

            let jwtData = await identityService.login(values);

            if (jwtData === undefined) {
                setValidationErrors(["Invalid email or password!"]);
                return;
            }

            if (setJwtResponse){
                setJwtResponse(jwtData);
                navigate("/");
            }
        }

        setValidated(true);

    }

    return (
        <LoginFormView values={values} handleChange={handleChange} onSubmit={onSubmit} validationErrors={validationErrors}  validated={validated}/>
    );
}

export default Login;
;