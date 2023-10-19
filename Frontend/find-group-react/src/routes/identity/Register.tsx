import {FormEvent, useEffect, useState} from "react";
import { useNavigate } from "react-router-dom";
import { IRegisterData } from "../../dto/IRegisterData";
import { IdentityService } from "../../services/IdentityService";
import RegisterFormView from "./RegisterFormView";
import {GenderService} from "../../services/GenderService";
import {IGender} from "../../domain/IGender";
import {IFormHandler} from "../../helpers/IFormHandler";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import useAxiosPublic from "../../hooks/useAxiosPublic";


const Register = () => {
    const navigate = useNavigate();

    const axiosPublic = useAxiosPublic();
    const genderService = new GenderService(axiosPublic);
    const identityService = new IdentityService(axiosPublic);

    const [gendersToShow, setGendersToShow] = useState([] as IGender[]);
    const [validationErrors, setValidationErrors] = useState([] as string[]);
    const [values, setInput] = useState({
        password: "",
        confirmPassword: "",
        email: "",
        firstName: "",
        lastName: "",
        gender: "",
        birthDate: ""
    } as IRegisterData);

    useEffect(() => {
        genderService.getAll().then(g => {
            setGendersToShow(g);
        })
    }, []);

    const handleChange: IFormHandler = (target) => {

        setInput({ ...values, [target.name]: target.value });
    }

    const [validated, setValidated] = useState(false);
    const onSubmit = async (event: FormEvent<HTMLFormElement>) => {

        const form = event.currentTarget;

        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        } else {
            event.preventDefault();
            // remove errors
            setValidationErrors([]);

            let response = await identityService.register(values);

            if (response !== "") {
                if (response === undefined) {
                    setValidationErrors(["Invalid inputs"]);
                    return
                }
                setValidationErrors([response]);
                return;
            }
            navigate("/");
        }

        setValidated(true);
    }

    return (
        <RegisterFormView values={values} genders={gendersToShow} handleChange={handleChange} onSubmit={onSubmit} validationErrors={validationErrors} validated={validated}/>
    );
}

export default Register;