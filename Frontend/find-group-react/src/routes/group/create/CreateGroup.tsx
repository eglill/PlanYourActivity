import React, {FormEvent, useEffect, useState} from 'react';
import CreateGroupForm from "./CreateGroupForm";
import {IGender} from "../../../domain/IGender";
import {IAddGroup} from "../../../dto/IAddGroup";
import {IFormHandler} from "../../../helpers/IFormHandler";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import {GenderService} from "../../../services/GenderService";
import useAxiosPublic from "../../../hooks/useAxiosPublic";
import {useNavigate} from "react-router-dom";
import {GroupService} from "../../../services/GroupService";

const CreateGroup = () => {
    const axiosPrivate = useAxiosPrivate();
    const axiosPublic = useAxiosPublic();
    const navigate = useNavigate();
    const genderService = new GenderService(axiosPublic);
    const groupService = new GroupService(axiosPrivate);
    const [gendersToShow, setGendersToShow] = useState([] as IGender[]);
    const [validationErrors, setValidationErrors] = useState([] as string[]);
    const [values, setInput] = useState({
        name: "",
        description: "",
        maxParticipants: 1,
        maxAge: undefined,
        minAge: undefined,
        private: false,
        joiningLocked: false,
        groupGenderId: ""
    } as IAddGroup);

    useEffect(() => {
        genderService.getAll().then(g => {
            setGendersToShow(g);
        })
    }, []);

    const handleChange: IFormHandler = (target) => {
        if (target.type === "checkbox") {
            setInput({ ...values, [target.name]: (target as EventTarget & HTMLInputElement).checked });
            return;
        }

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

            let response = await groupService.addGroup(values);

            if (response === "") {
                setValidationErrors(["Creation failed"]);
                return
            }
            navigate("../group/" + response);
        }

        setValidated(true);
    }

    return (
        <div className="container">
            <h2>Create Group</h2>
            <hr/>
            <CreateGroupForm values={values} genders={gendersToShow} validationErrors={validationErrors} handleChange={handleChange} onSubmit={onSubmit} validated={validated}/>
        </div>
    );
};

export default CreateGroup;