import React from 'react';
import {Link} from "react-router-dom";
import {IGroupEvent} from "../../domain/IEvent";

interface IProps {
    value: IGroupEvent;
    link: string;
}

const EventCard = (props: IProps) => {
    return (
        <div className="">
            <div className="card text-dark mb-3 mx-auto" style={{width: "17rem", backgroundColor: props.value.colour}}>
                <div className="card-body">
                    <h5 className="card-title">{props.value.name}</h5>
                </div>
                <ul className="list-group list-group-flush">
                    <li className="list-group-item text-dark" style={{backgroundColor: props.value.colour}}>
                        Starts at: {props.value.startsAt}</li>
                    <li className="list-group-item text-dark" style={{backgroundColor: props.value.colour}}>
                        Ends at: {props.value.endsAt}</li>
                    <li className="list-group-item text-dark" style={{backgroundColor: props.value.colour}}>
                        Activity: {props.value.activity}</li>
                    <li className="list-group-item text-dark" style={{backgroundColor: props.value.colour}}>
                        Location: {props.value.location.address}</li>
                </ul>
                <Link to={props.link} state={props.value} className="stretched-link"></Link>
            </div>
        </div>
    );
};

export default EventCard;