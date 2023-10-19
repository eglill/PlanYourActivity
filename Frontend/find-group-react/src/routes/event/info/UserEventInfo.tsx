import React, {FormEvent} from 'react';
import {IGroupEvent, IUserEvent} from "../../../domain/IEvent";
import {ILocation} from "../../../domain/ILocation";
import {DescriptionListElem} from "../../../components/DescriptionListElem";
import {ButtonGroup} from "react-bootstrap";
import ConfirmModal from "../../../components/modal/ConfirmModal";

interface IProps {
    value: IUserEvent;
    location?: ILocation;
    onDelete: (event: FormEvent<HTMLButtonElement>) => void;
}
const UserEventInfo = (props: IProps) => {
    return (
        <div className="p-3">
            <div>
                <h4>{props.value.name}</h4>
                <hr/>
                <dl className="row d-flex justify-content-center">
                    <DescriptionListElem description="Starts" value={props.value.startsAt}/>
                    <DescriptionListElem description="Ends" value={props.value.endsAt}/>
                    <DescriptionListElem description="Address" value={props.location?.address || "-"}/>
                    <DescriptionListElem description="Address description" value={props.location?.description || "-"}/>
                    <DescriptionListElem description="Country" value={props.location?.country || "-"}/>
                    <DescriptionListElem description="activity" value={(props.value.activity === undefined) ? "-" : props.value.activity}/>
                </dl>
            </div>

            <ButtonGroup className="w-50">
                <ConfirmModal name={props.value.name} buttonName="Delete" title="Confirm delete"
                              message="Delete event:" onSubmit={props.onDelete}
                              buttonStyle="danger"/>
            </ButtonGroup>

        </div>
    );
};

export default UserEventInfo;