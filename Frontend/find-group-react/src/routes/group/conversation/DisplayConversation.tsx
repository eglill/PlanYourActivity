import React, {FormEvent} from 'react';
import {IAppUser} from "../../../domain/IAppUser";
import {IMessage} from "../../../domain/IMessage";
import {IFormHandler} from "../../../helpers/IFormHandler";
import Form from "react-bootstrap/Form";
import {Input} from "../../../components/Input/Input";
import {Button, Toast} from "react-bootstrap";

interface IProps {
    messages: IMessage[],
    onAdd: (event: FormEvent<HTMLFormElement>) => void,
    content: string
    handleChange: IFormHandler;
}
const DisplayConversation = (props: IProps) => {
    return (
        <div className="pt-3">
            {props.messages.map(message => (
                <div className="p-1">
                    <Toast>
                        <div className="toast-header">
                            <strong className="mr-auto">{message.creator}</strong>
                        </div>
                        <div className="toast-body">{message.content}</div>
                    </Toast>
                </div>
            ))}

            <Form className="row formInput pt-3" onSubmit={(e) => props.onAdd(e)}>
                <div className="col col-8 col-sm-10">
                    <Input label="Content" type="text" id="Input_Content" name="content" value={props.content}
                           placeholder="Content" changeHandler={props.handleChange} autoComplete="off"
                           pattern="^.{1,}$" errorMsg="Invalid content" required={true}/>
                </div>
                <div className="col col-4 col-sm-2">
                    <Button
                        type="submit" id="registerSubmit" className="w-100 btn btn-lg btn-primary">Send</Button>
                </div>
            </Form>
        </div>
    );
};

export default DisplayConversation;