import {FormEvent, useState} from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

interface IProps {
    name: string;
    buttonName: string;
    title: string;
    message: string;
    buttonStyle: string;
    onSubmit: (event: FormEvent<HTMLButtonElement>) => void;
}

function ConfirmModal(props: IProps) {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const onSubmit = async (event: FormEvent<HTMLButtonElement>)  => {
        props.onSubmit(event)
        handleClose()
    }

    return (
        <>
            <Button variant={props.buttonStyle} onClick={handleShow}>
                {props.buttonName}
            </Button>

            <Modal
                show={show}
                onHide={handleClose}
                backdrop="static"
                keyboard={false}
            >
                <Modal.Header closeButton>
                    <Modal.Title>{props.title}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {props.message} {props.name}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant={props.buttonStyle} type="submit" id="registerSubmit"
                            onClick={(e) => onSubmit(e)}>{props.buttonName}</Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default ConfirmModal;