import React from 'react';
import {IGroup} from "../../domain/IGroup";
import {Link} from "react-router-dom";

interface IProps {
    value: IGroup;

    link: string;
}

const GroupCard = (props: IProps) => {
    return (
        <div className="">
            <div className="card text-white bg-secondary mb-3 mx-auto" style={{width: "17rem"}}>
                <div className="card-body">
                    <h5 className="card-title">{props.value.name}</h5>
                    <p className="card-text">{props.value.description}</p>
                </div>
                <ul className="list-group list-group-flush ">
                    <li className="list-group-item text-white bg-secondary">Participants: {props.value.participants} out of {props.value.maxParticipants}</li>
                    <li className="list-group-item text-white bg-secondary">Age range: {props.value.minAge ??= 0} - {props.value.maxAge || 100}</li>
                    <li className="list-group-item text-white bg-secondary">Gender: {props.value.groupGender || "all"}</li>
                </ul>
                <Link to={props.link} state={props.value} className="stretched-link"></Link>
            </div>
        </div>
    );
};

export default GroupCard;