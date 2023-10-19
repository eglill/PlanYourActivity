import React, {FormEvent} from 'react';
import {IFormHandler} from "../../../helpers/IFormHandler";
import {IAddGroupEvent} from "../../../dto/IEvent";
import {ICountry} from "../../../domain/ICountry";
import Form from "react-bootstrap/Form";
import {ErrorList} from "../../../components/ErrorList";
import {Input} from "../../../components/Input/Input";
import {SelectColour} from "../../../components/Input/SelectColour";
import {Button} from "react-bootstrap";
import {IColour} from "../../../domain/IColour";
import {SelectCountry} from "../../../components/Input/SelectCountry";
import {ILocationAdd} from "../../../dto/ILocationAdd";
import {IActivity} from "../../../domain/IActivity";
import {SelectActivity} from "../../../components/Input/SelectActivity";

interface IProps {
    values: IAddGroupEvent;
    location: ILocationAdd;
    countries: ICountry[];
    activities: IActivity[];
    colours: IColour[];
    validationErrors: string[];
    handleChange: IFormHandler;
    handleLocationChange: IFormHandler;
    onSubmit: (event: FormEvent<HTMLFormElement>) => void;
    validated: boolean;
}

const CreateGroupEventForm = (props: IProps) => {
    return (
        <Form className="formInput" noValidate validated={props.validated} onSubmit={(e) => props.onSubmit(e)}>
            <ErrorList errors={ props.validationErrors }/>

            <Input label="Name" type="text" id="Input_Name" name="name" value={props.values.name}
                   placeholder="Name" changeHandler={props.handleChange} autoComplete="name"
                   pattern="^.{1,}$" errorMsg="Invalid name" required={true}/>

            <Input label="Starts" type="datetime-local" id="Input_StartsAt" name="startsAt" value={(props.values.startsAt).toString()}
                   placeholder="" changeHandler={props.handleChange} autoComplete="off"
                   pattern="" errorMsg="Can't be empty" required={true}/>

            <Input label="Ends" type="datetime-local" id="Input_EndsAt" name="endsAt" value={(props.values.endsAt).toString()}
                   placeholder="" changeHandler={props.handleChange} autoComplete="off"
                   pattern="" errorMsg="Can't be empty" required={true}/>

            <SelectColour label="Choose Colour" id="Input_Colour" name="colourId" value={props.values.colourId}
                          changeHandler={props.handleChange} colours={props.colours}
                          placeholder="Colour" errorMsg="Must choose colour" required={true}/>

            <Input label="Address" type="text" id="Input_Address" name="address" value={props.location.address}
                   placeholder="Address" changeHandler={props.handleLocationChange} autoComplete="address"
                   pattern="^.{1,}$" errorMsg="Invalid Address" required={true}/>

            <Input label="Location Description" type="text" id="Input_Description" name="description" value={props.location.description!}
                   placeholder="Description" changeHandler={props.handleLocationChange} autoComplete="off"
                   pattern="^.{0,}$" errorMsg="Invalid Address" required={false}/>

            <SelectCountry label="Choose Country" id="Input_Country" name="countryId" value={props.location.countryId}
                          changeHandler={props.handleLocationChange} countries={props.countries}
                          placeholder="Country" errorMsg="Must choose country" required={true}/>

            <SelectActivity label="Choose Activity" id="Input_ActivityId" name="activityId" value={props.values.activityId}
                           changeHandler={props.handleChange} activities={props.activities}
                           placeholder="Activity" errorMsg="Must choose activity" required={true}/>
            <Button
                type="submit" id="registerSubmit" className="w-100 btn btn-lg btn-primary">Save</Button>
        </Form>
    );
};

export default CreateGroupEventForm;