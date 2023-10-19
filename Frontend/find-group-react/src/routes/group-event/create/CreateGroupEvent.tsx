import React, {FormEvent, useEffect, useState} from 'react';
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import {EventService} from "../../../services/EventService";
import {IAddGroupEvent} from "../../../dto/IEvent";
import {IFormHandler} from "../../../helpers/IFormHandler";
import {useNavigate, useParams} from "react-router-dom";
import {ICountry} from "../../../domain/ICountry";
import {CountryService} from "../../../services/CountryService";
import {ColourService} from "../../../services/ColourService";
import {IColour} from "../../../domain/IColour";
import CreateGroupEventForm from "./CreateGroupEventForm";
import {ILocationAdd} from "../../../dto/ILocationAdd";
import {IActivity} from "../../../domain/IActivity";
import {ActivityService} from "../../../services/ActivityService";
import UserGroupMenu from "../../../components/group/UserGroupMenu";

const CreateGroupEvent = () => {
    const navigate = useNavigate();
    const { groupId } = useParams();
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
        activityId: "",
        groupId: groupId!,
    } as IAddGroupEvent);

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

            let response = await eventService.AddGroupEvent(values);

            if (response === undefined) {
                setValidationErrors(["Creation failed"]);
                return
            }
            navigate("../group/event/" + response + '/' + groupId!);
        }

        setValidated(true);
    }

    return (
        <div className="container w-100">
            <UserGroupMenu groupId={groupId!}/>
            <CreateGroupEventForm values={values} location={location} countries={countriesToShow} colours={coloursToShow}
                                  activities={activitiesToShow} validationErrors={validationErrors} handleChange={handleChange}
                                  handleLocationChange={handleLocationChange} onSubmit={onSubmit} validated={validated}/>
        </div>
    );
};

export default CreateGroupEvent;