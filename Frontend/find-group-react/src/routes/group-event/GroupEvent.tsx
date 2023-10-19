import React, {FormEvent, useEffect, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import {EventService} from "../../services/EventService";
import {IGroupEvent} from "../../domain/IEvent";
import {GroupService} from "../../services/GroupService";
import UserGroupMenu from "../../components/group/UserGroupMenu";
import GroupEventInfo from "./info/groupEventInfo";
import {ILocation} from "../../domain/ILocation";

const GroupEvent = () => {
    const navigate = useNavigate();
    const {eventId, groupId} = useParams();
    const axiosPrivate = useAxiosPrivate();
    const eventService = new EventService(axiosPrivate);
    const groupService = new GroupService(axiosPrivate);
    const [eventToShow, setEventToShow] = useState({} as IGroupEvent);
    const [locationToShow, setLocationToShow] = useState({} as ILocation);
    const [isUserAdmin, setIsUserAdmin] = useState(false);

    useEffect(() => {
        eventService.getGroupEvent(eventId!).then(g => {
            if (g === undefined) {
                navigate("../group/events/" + groupId!);
            } else {
                setEventToShow(g);
                setLocationToShow(g.location);
            }
        })
    },[])

    useEffect(() => {
        groupService.getIfUserIsAdmin(groupId!).then(g => {
            setIsUserAdmin(g)
        })
    },[])

    const onDelete = async (event: FormEvent<HTMLButtonElement>) => {
        event.preventDefault();
        let res = await eventService.deleteGroupEvent(eventId!)
        if (res) {
            navigate("../group/events/" + groupId!);
        }
        return;
    }

    const onAdd = async (event: FormEvent<HTMLButtonElement>) => {
        event.preventDefault();
        let res = await eventService.AddGroupEventToEvents(eventId!)
        if (res) {
            navigate("../group/events/" + groupId!);
        }
        return;
    }

    return (
        <div className="container w-100">
            <UserGroupMenu groupId={groupId!}/>

            <GroupEventInfo value={eventToShow} location={locationToShow} isAdmin={isUserAdmin} onDelete={onDelete} onAdd={onAdd}/>
        </div>
    );
};

export default GroupEvent;