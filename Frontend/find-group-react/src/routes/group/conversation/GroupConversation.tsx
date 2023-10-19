import React, {FormEvent, useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import {ConversationService} from "../../../services/ConversationService";
import {IAppUser} from "../../../domain/IAppUser";
import {IMessage} from "../../../domain/IMessage";
import {IFormHandler} from "../../../helpers/IFormHandler";
import UserGroupMenu from "../../../components/group/UserGroupMenu";
import DisplayUsers from "../users/DisplayUsers";
import DisplayConversation from "./DisplayConversation";

const GroupConversation = () => {
    const { groupId } = useParams();
    const axiosPrivate = useAxiosPrivate();
    const conversationService = new ConversationService(axiosPrivate);
    const [messagesToShow, setMessagesToShow] = useState([] as IMessage[]);
    const [errorMsg, setErrorMsg] = useState(undefined as undefined | string);
    const [content, setContent] = useState("");


    useEffect(() => {
        conversationService.getGroupConversation(groupId!).then(g => {
            if (g !== undefined) {
                setMessagesToShow(g.messages);
            } else {
                setErrorMsg("Failed to load messages");
            }
        })
    },[])

    const handleChange: IFormHandler = (target) => {

        setContent(target.value);
    }

    const onAdd = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        await conversationService.addMessage({content: content, groupId: groupId!})
        conversationService.getGroupConversation(groupId!).then(g => {
            if (g !== undefined) {
                setMessagesToShow(g.messages);
                setContent("");
            } else {
                setErrorMsg("Failed to load messages");
            }
        })
    }

    return (
        <div className="container w-100">
            <UserGroupMenu groupId={groupId!}/>

            <DisplayConversation messages={messagesToShow} onAdd={onAdd} content={content} handleChange={handleChange}/>
        </div>
    );
};

export default GroupConversation;