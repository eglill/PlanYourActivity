import React, {FormEvent, useEffect, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import useAxiosPrivate from "../../hooks/useAxiosPrivate";
import {GroupService} from "../../services/GroupService";
import {IGroupWithData} from "../../domain/IGroupWithData";
import GroupInfo from "./info/GroupInfo";
import {IAddGroup} from "../../dto/IAddGroup";
import {IFormHandler} from "../../helpers/IFormHandler";
import UserGroupMenu from "../../components/group/UserGroupMenu";

const Group = () => {
    const navigate = useNavigate();
    const { groupId } = useParams();
    const axiosPrivate = useAxiosPrivate();
    const groupService = new GroupService(axiosPrivate);
    const [groupToShow, setGroupToShow] = useState({} as IGroupWithData);
    const [updateGroup, setUpdateGroup] = useState({} as IAddGroup);
    const [isUserAdmin, setIsUserAdmin] = useState(false);
    const [validated, setValidated] = useState(false);
    const [validationErrors, setValidationErrors] = useState([] as string[]);

    useEffect(() => {
        groupService.getGroup(groupId!).then(g => {
            if (g === undefined) {
                navigate("user/groups/");
            } else {
                setGroupToShow(g);
                setUpdateGroup({
                    name: g.name,
                    description: g.description,
                    maxParticipants: g.maxParticipants,
                    maxAge: g.maxAge,
                    minAge: g.minAge,
                    private: g.private,
                    joiningLocked: g.joiningLocked,
                    groupGenderId: ""
                });
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
        let res = await groupService.deleteGroup(groupId!)
        if (res) {
            navigate("../user/groups/");
        } else {
            navigate("../group/" + groupId);
        }
    }

    const onLeave = async (event: FormEvent<HTMLButtonElement>) => {
        event.preventDefault();
        let res = await groupService.leaveGroup(groupId!)
        if (res) {
            navigate("../user/groups/");
        } else {
            navigate("../group/" + groupId);
        }
    }

    const onUpdate = async (event: FormEvent<HTMLFormElement>) => {

        const form = event.currentTarget;

        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
            return false;
        } else {
            event.preventDefault();
            // remove errors
            setValidationErrors([]);

            let response = await groupService.updateGroup(updateGroup, groupId!);

            if (!response) {
                setValidationErrors(["Creation failed"]);
                return false;
            }

            groupService.getGroup(groupId!).then(g => {
                if (g === undefined) {
                    navigate("user/groups/");
                } else {
                    setGroupToShow(g)
                }
                setUpdateGroup({...updateGroup, groupGenderId:""})
            })

            return true
        }
        // setValidated(true);
    }

    const handleUpdateChange: IFormHandler = (target) => {
        if (target.type === "checkbox") {
            setUpdateGroup({ ...updateGroup, [target.name]: (target as EventTarget & HTMLInputElement).checked });
            return;
        }
        setUpdateGroup({ ...updateGroup, [target.name]: target.value });
    }


    return (
        <div className="container w-100">
            <UserGroupMenu groupId={groupId!}/>

            <GroupInfo value={groupToShow} updateGroup={updateGroup} isAdmin={isUserAdmin} onDelete={onDelete} onLeave={onLeave}
            onUpdate={onUpdate} validationErrors={validationErrors} validated={validated} handleUpdateChange={handleUpdateChange}/>
        </div>
    );
};

export default Group;