import React, {FormEvent, useEffect, useState} from 'react';
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import {BlockUserService} from "../../services/BlockUserService";
import {IAppUser} from "../../domain/IAppUser";
import DisplayBlockedUsers from "./DisplayBlockedUsers";

const BlockedUsers = () => {
    const axiosPrivate = useAxiosPrivate();
    const blockUserService = new BlockUserService(axiosPrivate);
    const [usersToShow, setUsersToShow] = useState([] as IAppUser[]);

    useEffect(() => {
        blockUserService.getAllBlockedUsers().then(g => {
            setUsersToShow(g)
        })
    },[])

    const onUnblock = async (event: FormEvent<HTMLButtonElement>, userId: string) => {
        event.preventDefault();
        let unblocked = await blockUserService.unblockUser(userId);
        if (unblocked) {
            blockUserService.getAllBlockedUsers().then(g => {
                setUsersToShow(g)
            })
        }
    }

    return (
        <div className="container w-100">
            <DisplayBlockedUsers users={usersToShow} onUnblock={onUnblock}/>
        </div>
    );
};

export default BlockedUsers;