import React, {FormEvent } from 'react';
import {IGroupWithData} from "../../../domain/IGroupWithData";
import {DescriptionListElem} from "../../../components/DescriptionListElem";
import {ButtonGroup} from "react-bootstrap";
import ConfirmModal from "../../../components/modal/ConfirmModal";
import GroupUpdateModal from "../../../components/group/GroupUpdateModal";
import {IFormHandler} from "../../../helpers/IFormHandler";
import {IAddGroup} from "../../../dto/IAddGroup";
import {Link} from "react-router-dom";

interface IProps {
    value: IGroupWithData;
    updateGroup: IAddGroup
    isAdmin: boolean;
    onDelete: (event: FormEvent<HTMLButtonElement>) => void;
    onLeave: (event: FormEvent<HTMLButtonElement>) => void;
    onUpdate: (event: FormEvent<HTMLFormElement>) => Promise<boolean>;
    validationErrors: string[];
    handleUpdateChange: IFormHandler;
    validated: boolean;
}

const GroupInfo = (props: IProps) => {
    return (
        <div className="p-3">
            <div>
                <h4>{props.value.name}</h4>
                <hr/>
                <dl className="row d-flex justify-content-center">
                    <DescriptionListElem description="Description" value={props.value.name}/>
                    <DescriptionListElem description="Max participants" value={props.value.maxParticipants}/>
                    <DescriptionListElem description="Participants" value={props.value.participants}/>
                    <DescriptionListElem description="Max age" value={props.value.maxAge || "-"}/>
                    <DescriptionListElem description="Min age" value={props.value.minAge || "-"}/>
                    <DescriptionListElem description="Group gender" value={props.value.groupGender || "all"}/>
                    <DescriptionListElem description="Group is private" value={String(props.value.private)}/>
                    <DescriptionListElem description="Joining group is locked" value={String(props.value.joiningLocked)}/>
                    <DescriptionListElem description="Group admin" value={props.value.admin}/>
                </dl>
            </div>

            <ButtonGroup className="w-50" style={{ 'display': props.isAdmin ? '' : 'none' }}>
                <Link to={"../group/invite/" + props.value.id} className="btn btn-secondary">Invite</Link>
                <GroupUpdateModal group={props.updateGroup} onUpdate={props.onUpdate} handleUpdateChange={props.handleUpdateChange}
                validated={props.validated} validationErrors={props.validationErrors}/>
                <ConfirmModal name={props.value.name} buttonName="Delete" title="Confirm delete"
                              message="Delete group:" onSubmit={props.onDelete}
                              buttonStyle="danger"/>
            </ButtonGroup>

            <ButtonGroup className="w-50" style={{ 'display': props.isAdmin ? 'none' : '' }}>
                <ConfirmModal name={props.value.name} buttonName="Leave" title="Confirm leaving"
                              message="Leave group:" onSubmit={props.onLeave}
                              buttonStyle="danger"/>
            </ButtonGroup>

        </div>
    );
};

export default GroupInfo;