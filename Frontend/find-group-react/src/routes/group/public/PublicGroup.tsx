import React, {MouseEvent} from 'react';
import {Link, useLocation, useNavigate} from 'react-router-dom';
import useAxiosPrivate from "../../../hooks/useAxiosPrivate";
import {GroupService} from "../../../services/GroupService";
import {IGroup} from "../../../domain/IGroup";

const PublicGroup = () => {
    const navigate = useNavigate();
    let { state }: {state: IGroup} = useLocation();
    const axiosPrivate = useAxiosPrivate();
    const groupService = new GroupService(axiosPrivate);

    const onAdd = async (event: MouseEvent) => {
        event.preventDefault();
        let result = await groupService.joinGroup(state.id!)
        if (result) {
            navigate("../group/" + state.id)
        }
    }

    return (
        <div>
            <div>
                <h4>Group</h4>
                <hr/>
                    <dl className="row">
                        <dt className="col-sm-2">
                            Name
                        </dt>
                        <dd className="col-sm-10">
                            {state.name}
                        </dd>
                        <dt className="col-sm-2">
                            Description
                        </dt>
                        <dd className="col-sm-10">
                            {state.description}
                        </dd>
                        <dt className="col-sm-2">
                            Maximum participants
                        </dt>
                        <dd className="col-sm-10">
                            {state.maxParticipants}
                        </dd>
                        <dt className="col-sm-2">
                            Participants
                        </dt>
                        <dd className="col-sm-10">
                            {state.participants}
                        </dd>
                        <dt className="col-sm-2">
                            Maximum age
                        </dt>
                        <dd className="col-sm-10">
                            {state.maxAge}
                        </dd>
                        <dt className="col-sm-2">
                            Minimum age
                        </dt>
                        <dd className="col-sm-10">
                            {state.minAge}
                        </dd>
                        <dt className="col-sm-2">
                            Group gender
                        </dt>
                        <dd className="col-sm-10">
                            {state.groupGender || "all"}
                        </dd>
                    </dl>
            </div>
            <div className="w-50">
                <Link to="../groups" className="btn btn btn-secondary m-2 w-25">Back to List</Link>
                <button
                    onClick={(e) => onAdd(e)}
                    id="registerSubmit" className="btn btn-success m-2 w-25">Join</button>
            </div>
        </div>
    );
};

export default PublicGroup;