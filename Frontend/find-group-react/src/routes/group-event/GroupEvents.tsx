import React, {useEffect, useState} from 'react';
import UserGroupMenu from "../../components/group/UserGroupMenu";
import {Link, useParams} from "react-router-dom";
import {ButtonGroup} from "react-bootstrap";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import {EventService} from "../../services/EventService";
import {IGroupEvent} from "../../domain/IEvent";
import EventCard from "../../components/event/EventCard";
import GroupUpdateModal from "../../components/group/GroupUpdateModal";
import ConfirmModal from "../../components/modal/ConfirmModal";
import {GroupService} from "../../services/GroupService";

const GroupEvents = () => {
    const { groupId } = useParams();
    const axiosPrivate = useAxiosPrivate();
    const eventService = new EventService(axiosPrivate);
    const groupService = new GroupService(axiosPrivate);
    const [eventsToShow, setEventsToShow] = useState([] as IGroupEvent[]);
    const [isUserAdmin, setIsUserAdmin] = useState(false);

    useEffect(() => {
        eventService.getGroupEvents(groupId!).then(g => {
            setEventsToShow(g);
        })
    }, []);

    useEffect(() => {
        groupService.getIfUserIsAdmin(groupId!).then(g => {
            setIsUserAdmin(g)
        })
    },[])

    return (
        <div className="container w-100">
            <UserGroupMenu groupId={groupId!}/>
            <ButtonGroup className="m-2 btn-group-sm" style={{ 'display': isUserAdmin ? '' : 'none' }}>
                <Link to={"../group/add-event/" + groupId!} className="btn btn-lg btn-success">Create new event</Link>
            </ButtonGroup>
            <hr/>
            <div className="card-deck mt-4">
                <div className="row row-cols-1 row-cols-sm-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4">
                    {eventsToShow.map(event => (
                        <EventCard key={event.id} value={event} link={'../group/event/' + event.id + '/' + groupId!}/>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default GroupEvents;