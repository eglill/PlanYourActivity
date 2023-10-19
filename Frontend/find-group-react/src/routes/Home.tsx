import {useEffect, useState} from "react";
import useAxiosPrivate from "../hooks/useAxiosPrivate";
import {EventService} from "../services/EventService";
import {IGroupEvent} from "../domain/IEvent";

const Home = () => {

    const axiosPrivate = useAxiosPrivate();
    const eventService = new EventService(axiosPrivate);
    const [eventsToShow, setEventsToShow] = useState([] as IGroupEvent[]);

    return (
        <h3>XD</h3>
    );
}

export default Home;