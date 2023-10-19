import React, {FormEvent, useEffect, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import {EventService} from "../../../services/EventService";
import {CountryService} from "../../../services/CountryService";
import {ColourService} from "../../../services/ColourService";
import {ActivityService} from "../../../services/ActivityService";
import {ICountry} from "../../../domain/ICountry";
import {IColour} from "../../../domain/IColour";
import {IActivity} from "../../../domain/IActivity";
import {ILocationAdd} from "../../../dto/ILocationAdd";
import {IAddGroupEvent, IAddUserEvent} from "../../../dto/IEvent";
import {IFormHandler} from "../../../helpers/IFormHandler";
import CreateGroupEventForm from "../../group-event/create/CreateGroupEventForm";
import CreateUserEventForm from "./CreateUserEventForm";

const CreateUserEvent = () => {
    const navigate = useNavigate();
    const axiosPrivate = useAxiosPrivate();
    const eventService = new EventService(axiosPrivate);
    const countryService = new CountryService(axiosPrivate);
    const colourService = new ColourService(axiosPrivate);
    const activityService = new ActivityService(axiosPrivate);
    const [countriesToShow, setCountriesToShow] = useState([] as ICountry[]);
    const [coloursToShow, setColoursToShow] = useState([] as IColour[]);
    const [activitiesToShow, setActivitiesToShow] = useState([] as IActivity[]);
    const [validationErrors, setValidationErrors] = useState([] as string[]);
    const [validated, setValidated] = useState(false);

    const [location, setLocation] = useState({
        address: "",
        countryId: "",
        description: ""
    } as ILocationAdd);

    const [values, setInput] = useState({
        name: "",
        startsAt: "",
        endsAt: "",
        colourId: "",
        location: location,
        activityId: ""
    } as IAddUserEvent);

    useEffect(() => {
        countryService.getAll().then(g => {
            setCountriesToShow(g);
        })
    }, []);

    useEffect(() => {
        colourService.getAll().then(g => {
            setColoursToShow(g);
        })
    }, []);

    useEffect(() => {
        activityService.getAll().then(g => {
            setActivitiesToShow(g);
        })
    }, []);

    const handleChange: IFormHandler = (target) => {

        setInput({ ...values, [target.name]: target.value });
    }

    const handleLocationChange: IFormHandler = (target) => {

        setLocation({ ...location, [target.name]: target.value });
    }

    const onSubmit = async (event: FormEvent<HTMLFormElement>) => {

        const form = event.currentTarget;

        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        } else {
            event.preventDefault();
            // remove errors
            setValidationErrors([]);

            values.startsAt = new Date(values.startsAt).toISOString().toString()
            values.endsAt = new Date(values.endsAt).toISOString().toString()

            values.location = location;

            let response = await eventService.AddUserEvent(values);

            if (response === undefined) {
                setValidationErrors(["Creation failed"]);
                return
            }
            navigate("../user/event/" + response);
        }

        setValidated(true);
    }

    return (
        <div>
            <CreateUserEventForm values={values} location={location} countries={countriesToShow}
                                 activities={activitiesToShow} colours={coloursToShow} validationErrors={validationErrors}
                                 handleChange={handleChange} handleLocationChange={handleLocationChange}
                                 onSubmit={onSubmit} validated={validated}/>
        </div>
    );
};

export default CreateUserEvent;