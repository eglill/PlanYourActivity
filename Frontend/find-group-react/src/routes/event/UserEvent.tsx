import React, {FormEvent, useEffect, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import {EventService} from "../../services/EventService";
import {IUserEvent} from "../../domain/IEvent";
import {ILocation} from "../../domain/ILocation";
import UserEventInfo from "./info/UserEventInfo";

const UserEvent = () => {
    const navigate = useNavigate();
    const {eventId, groupId} = useParams();
    const axiosPrivate = useAxiosPrivate();
    const eventService = new EventService(axiosPrivate);
    const [eventToShow, setEventToShow] = useState({} as IUserEvent);
    const [locationToShow, setLocationToShow] = useState(undefined as ILocation | undefined);

    useEffect(() => {
        eventService.getUserEvent(eventId!).then(g => {
            if (g === undefined) {
                navigate("../group/events/" + groupId!);
            } else {
                setEventToShow(g);
                if (g.location !== undefined) {
                    setLocationToShow(g.location);
                }
            }
        })
    },[])

    const onDelete = async (event: FormEvent<HTMLButtonElement>) => {
        event.preventDefault();
        let res = await eventService.deleteUserEvent(eventId!)
        if (res) {
            navigate("../user/events/");
        }
        return;
    }

    return (
        <div className="container w-100">
            <UserEventInfo value={eventToShow} location={locationToShow} onDelete={onDelete}/>
        </div>
    );
};

export default UserEvent;