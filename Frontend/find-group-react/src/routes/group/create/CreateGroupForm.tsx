import React, {FormEvent} from 'react';
import {IGender} from "../../../domain/IGender";
import {IFormHandler} from "../../../helpers/IFormHandler";
import {IAddGroup} from "../../../dto/IAddGroup";
import Form from "react-bootstrap/Form";
import {ErrorList} from "../../../components/ErrorList";
import {Input} from "../../../components/Input/Input";
import {Button} from "react-bootstrap";
import {SelectGender} from "../../../components/Input/SelectGender";
import {CheckBox} from "../../../components/Input/CheckBox";
import {InputNumber} from "../../../components/Input/InputNumber";

interface IProps {
    values: IAddGroup;
    genders: IGender[];
    validationErrors: string[];
    handleChange: IFormHandler;
    onSubmit: (event: FormEvent<HTMLFormElement>) => void;
    validated: boolean;
}

const CreateGroupForm = (props: IProps) => {
    return (
        <Form className="formInput" noValidate validated={props.validated} onSubmit={(e) => props.onSubmit(e)}>
            <ErrorList errors={ props.validationErrors } />

            <Input label="Name" type="text" id="Input_Name" name="name" value={props.values.name}
                   placeholder="Name" changeHandler={props.handleChange} autoComplete="name"
                   pattern="^.{1,}$" errorMsg="Invalid name" required={true}/>

            <Input label="Description" type="text" id="Input_Description" name="description" value={props.values.description}
                   placeholder="Description" changeHandler={props.handleChange} autoComplete="off"
                   pattern="^.{1,}$" errorMsg="Invalid description" required={true}/>

            <InputNumber label="Maximum participants" id="Input_MaxParticipants" name="maxParticipants"
                         value={props.values.maxParticipants} placeholder="Maximum participants" changeHandler={props.handleChange}
                         required={true} min={1} max={undefined} errorMsg={"Required"}/>

            <InputNumber label="Minimum age" id="Input_MinAge" name="minAge"
                         value={props.values.minAge} placeholder="Minimum age" changeHandler={props.handleChange}
                         required={false} min={0} max={props.values.maxAge} errorMsg={"Min can't be greater than max"}/>

            <InputNumber label="Maximum age" id="Input_MaxAge" name="maxAge"
                         value={props.values.maxAge} placeholder="Maximum age" changeHandler={props.handleChange}
                         required={false} min={props.values.minAge} max={undefined} errorMsg={"Max can't be smaller than min"}/>

            <SelectGender label="Choose gender" id="Input_Gender" name="groupGenderId" value={props.values.groupGenderId}
                          changeHandler={props.handleChange} genders={props.genders}
                          placeholder="Gender" errorMsg="Must choose gender" required={false}/>

            <CheckBox label="Private group" id="Input_Private" name="private"
                      changeHandler={props.handleChange} checked={props.values.private} />

            <CheckBox label="Joining to group is locked" id="Input_JoiningLocked" name="joiningLocked"
                      changeHandler={props.handleChange} checked={props.values.joiningLocked} />

            <Button
                type="submit" id="registerSubmit" className="w-100 btn btn-lg btn-primary">Save</Button>
        </Form>
    );
};

export default CreateGroupForm;