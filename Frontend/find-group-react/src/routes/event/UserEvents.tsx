import React, {useEffect, useState} from 'react';
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import {EventService} from "../../services/EventService";
import {IGroupEvent} from "../../domain/IEvent";
import {ButtonGroup} from "react-bootstrap";
import {Link} from "react-router-dom";
import EventCard from "../../components/event/EventCard";

const UserEvents = () => {
    const axiosPrivate = useAxiosPrivate();
    const eventService = new EventService(axiosPrivate);
    const [eventsToShow, setEventsToShow] = useState([] as IGroupEvent[]);

    useEffect(() => {
        eventService.getUserEvents().then(g => {
            setEventsToShow(g);
        })
    }, []);



    return (
        <div className="container w-100">
            <ButtonGroup className="m-2 btn-group-sm">
                <Link to={"../user/add-event/"} className="btn btn-lg btn-success">Create new event</Link>
            </ButtonGroup>
            <hr/>
            <div className="card-deck mt-4">
                <div className="row row-cols-1 row-cols-sm-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4">
                    {eventsToShow.map(event => (
                        <EventCard key={event.id} value={event} link={'../user/event/' + event.id}/>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default UserEvents;