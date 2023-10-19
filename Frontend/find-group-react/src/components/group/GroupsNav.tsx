import React from 'react';
import {ButtonGroup} from "react-bootstrap";
import {Link} from "react-router-dom";

const GroupsNav = () => {
    return (
        <ButtonGroup className="p-3 w-50">
            <Link to="../user/groups/" className="btn btn-sm btn-warning">User groups</Link>
            <Link to="../groups/" className="btn btn-sm btn-warning">Public groups</Link>
            <Link to="../user/add-group/" className="btn btn-sm btn-warning">Create new</Link>
        </ButtonGroup>
    );
};

export default GroupsNav;