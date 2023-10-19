import React, {useEffect, useState} from 'react';
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import {GroupService} from "../../../services/GroupService";
import {IGroup} from "../../../domain/IGroup";
import GroupCard from "../../../components/group/GroupCard";
import {ButtonGroup} from "react-bootstrap";
import {Link} from "react-router-dom";
import GroupsNav from "../../../components/group/GroupsNav";

const PublicGroups = () => {
    const axiosPrivate = useAxiosPrivate();
    const groupService = new GroupService(axiosPrivate);
    const [groupsToShow, setGroupsToShow] = useState([] as IGroup[]);

    useEffect(() => {
        groupService.getAll().then(g => {
            setGroupsToShow(g);
        })
    }, []);

    return (
        <div className="container">
            <GroupsNav/>

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

export default PublicGroups;