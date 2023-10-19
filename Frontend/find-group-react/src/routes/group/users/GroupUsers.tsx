import React, {FormEvent, useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import {GroupService} from "../../../services/GroupService";
import {IAppUser} from "../../../domain/IAppUser";
import UserGroupMenu from "../../../components/group/UserGroupMenu";
import DisplayUsers from "./DisplayUsers";
import {BlockUserService} from "../../../services/BlockUserService";

const GroupUsers = () => {
    const { groupId } = useParams();
    const axiosPrivate = useAxiosPrivate();
    const groupService = new GroupService(axiosPrivate);
    const blockUserService = new BlockUserService(axiosPrivate);
    const [isUserAdmin, setIsUserAdmin] = useState(false);
    const [usersToShow, setUsersToShow] = useState([] as IAppUser[]);

    useEffect(() => {
        groupService.getIfUserIsAdmin(groupId!).then(g => {
            setIsUserAdmin(g)
        })
    },[])

    useEffect(() => {
        groupService.getGroupUsers(groupId!).then(g => {
            setUsersToShow(g);
        })
    },[])

    const onKick = async (event: FormEvent<HTMLButtonElement>, userId: string) => {
        event.preventDefault();
        let res = await groupService.kickUserFromGroup(userId, groupId!)
        if (res) {
            groupService.getGroupUsers(groupId!).then(g => {
                setUsersToShow(g);
            })
        }
    }

    const onBlock = async (event: FormEvent<HTMLButtonElement>, userId: string) => {
        event.preventDefault();
        let block = await blockUserService.blockUser(userId);
        if (block && isUserAdmin) {
            await onKick(event, userId);
        }
    }

    return (
        <div className="container w-100">
            <UserGroupMenu groupId={groupId!}/>

            <DisplayUsers users={usersToShow} isAdmin={isUserAdmin} onKick={onKick} onBlock={onBlock}/>
        </div>
    );
};

export default GroupUsers;