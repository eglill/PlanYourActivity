import React, {FormEvent, useEffect, useState} from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import {IAddGroup} from "../../dto/IAddGroup";
import CreateGroupForm from "../../routes/group/create/CreateGroupForm";
import {IGender} from "../../domain/IGender";
import {IFormHandler} from "../../helpers/IFormHandler";
import useAxiosPublic from "../../hooks/useAxiosPublic";
import {GenderService} from "../../services/GenderService";

interface IProps {
    group: IAddGroup;
    onUpdate: (event: FormEvent<HTMLFormElement>) => Promise<boolean>;
    validationErrors: string[];
    handleUpdateChange: IFormHandler;
    validated: boolean;
}

function GroupUpdateModal(props: IProps) {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const axiosPublic = useAxiosPublic();
    const genderService = new GenderService(axiosPublic);
    const [gendersToShow, setGendersToShow] = useState([] as IGender[]);

    useEffect(() => {
        genderService.getAll().then(g => {
            setGendersToShow(g);
        })
    }, []);

    const onSubmit = async (event: FormEvent<HTMLFormElement>)  => {
        if (await props.onUpdate(event)) {
            handleClose()
        }
    }

    return (
        <>
            <Button variant="secondary" onClick={handleShow}>
                Edit
            </Button>

            <Modal
                show={show}
                onHide={handleClose}
                backdrop="static">
                <Modal.Header closeButton>
                    <Modal.Title>Edit group</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <CreateGroupForm values={props.group} genders={gendersToShow} validationErrors={props.validationErrors}
                                     handleChange={props.handleUpdateChange} onSubmit={onSubmit} validated={props.validated}/>
                </Modal.Body>
                {/*<Modal.Footer>*/}
                {/*    <Button variant="secondary" onClick={handleClose}>*/}
                {/*        Close*/}
                {/*    </Button>*/}
                {/*    <Button variant="secondary" type="submit" id="registerSubmit"*/}
                {/*            onClick={(e) => props.onSubmit(e)}>Save</Button>*/}
                {/*</Modal.Footer>*/}
            </Modal>
        </>
    );
}

export default GroupUpdateModal;