import React from 'react';
import {ButtonGroup} from "react-bootstrap";
import {Link} from "react-router-dom";

const UserGroupMenu = ({groupId}:{groupId:string}) => {
    return (
        <>
            <ButtonGroup className="w-100 p-2">
                <Link to={"../group/" + groupId} className="btn btn-warning">Group info</Link>
                <Link to={"../group/events/" + groupId} className="btn btn-warning">Events</Link>
                <Link to={"../group/conversation/" + groupId} className="btn btn-warning">Conversation</Link>
                {/*<Link to={"../group/polls/" + groupId} className="btn btn-warning">Polls</Link>*/}
                <Link to={"../group/users/" + groupId} className="btn btn-warning">Users</Link>
            </ButtonGroup>
        </>
    );
};

export default UserGroupMenu;