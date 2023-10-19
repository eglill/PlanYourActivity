import React, {FormEvent} from 'react';
import {IGroupEvent} from "../../../domain/IEvent";
import {DescriptionListElem} from "../../../components/DescriptionListElem";
import {ButtonGroup} from "react-bootstrap";
import ConfirmModal from "../../../components/modal/ConfirmModal";
import {ILocation} from "../../../domain/ILocation";

interface IProps {
    value: IGroupEvent;
    location: ILocation;
    isAdmin: boolean;
    onDelete: (event: FormEvent<HTMLButtonElement>) => void;
    onAdd: (event: FormEvent<HTMLButtonElement>) => void;
}
const GroupEventInfo = (props: IProps) => {
    return (
        <div className="p-3">
            <div>
                <h4>{props.value.name}</h4>
                <hr/>
                <dl className="row d-flex justify-content-center">
                    <DescriptionListElem description="Starts" value={props.value.startsAt}/>
                    <DescriptionListElem description="Ends" value={props.value.endsAt}/>
                    <DescriptionListElem description="Address" value={props.location.address}/>
                    <DescriptionListElem description="Address description" value={props.location.description}/>
                    <DescriptionListElem description="Country" value={props.location.country}/>
                    <DescriptionListElem description="activity" value={props.value.activity}/>
                </dl>
            </div>

            <ButtonGroup className="w-50" style={{ 'display': props.isAdmin ? '' : 'none' }}>
                <ConfirmModal name={props.value.name} buttonName="Add" title="Confirm adding"
                              message="Add event to personal calendar: " onSubmit={props.onAdd}
                              buttonStyle="success"/>
                <ConfirmModal name={props.value.name} buttonName="Delete" title="Confirm delete"
                              message="Delete event:" onSubmit={props.onDelete}
                              buttonStyle="danger"/>
            </ButtonGroup>

            <ButtonGroup className="w-50" style={{ 'display': props.isAdmin ? 'none' : '' }}>
                <ConfirmModal name={props.value.name} buttonName="Add" title="Confirm adding"
                              message="Add event to personal calendar: " onSubmit={props.onAdd}
                              buttonStyle="success"/>
            </ButtonGroup>
        </div>
    );
};

export default GroupEventInfo;