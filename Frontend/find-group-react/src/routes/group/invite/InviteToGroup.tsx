import React, {FormEvent, useEffect, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import {IGroupWithData} from "../../../domain/IGroupWithData";
import {IAppUser} from "../../../domain/IAppUser";
import {GroupService} from "../../../services/GroupService";
import {UsersService} from "../../../services/UsersService";
import DisplayInvitedUsers from "./DisplayInvitedUsers";
import UserGroupMenu from "../../../components/group/UserGroupMenu";

const InviteToGroup = () => {
    const navigate = useNavigate();
    const { groupId } = useParams();
    const axiosPrivate = useAxiosPrivate();
    const groupService = new GroupService(axiosPrivate);
    const usersService = new UsersService(axiosPrivate);
    const [isUserAdmin, setIsUserAdmin] = useState(false);
    const [usersToShow, setUsersToShow] = useState([] as IAppUser[]);

    useEffect(() => {
        groupService.getIfUserIsAdmin(groupId!).then(g => {
            setIsUserAdmin(g)
            if (!g) {
                navigate("../group/" + groupId);
            }
        })
    },[])

    useEffect(() => {
        usersService.getAll().then(g => {
            setUsersToShow(g)
        })
    },[])

    const onInvite = async (event: FormEvent<HTMLButtonElement>, userId: string) => {
        event.preventDefault();
        await groupService.inviteToGroup(groupId!, userId);
    }

    return (
        <div className="container w-100">
            <UserGroupMenu groupId={groupId!}/>

            <DisplayInvitedUsers users={usersToShow} onInvite={onInvite}/>
        </div>
    );
};

export default InviteToGroup;