import React, {useEffect, useState} from 'react';
import {useNavigate} from "react-router-dom";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import {GroupService} from "../../../services/GroupService";
import {IGroup} from "../../../domain/IGroup";
import GroupCard from "../../../components/group/GroupCard";

const GroupInvitations = () => {
    const navigate = useNavigate();
    const axiosPrivate = useAxiosPrivate();
    const groupService = new GroupService(axiosPrivate);
    const [groupsToShow, setGroupsToShow] = useState([] as IGroup[]);

    useEffect(() => {
        groupService.getGroupInvites().then(g => {
            setGroupsToShow(g);
        })
    }, []);

    return (
        <div className="container w-100 p-3">
            <h4>Invites</h4>
            <hr/>
            <div className="card-deck mt-4">
                <div className="row row-cols-1 row-cols-sm-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4">
                    {groupsToShow.map(group => (
                        <GroupCard key={group.id} value={group} link={'../group/'}/>
                    ))}
                </div>
            </div>
        </div>
    );
};

export default GroupInvitations;