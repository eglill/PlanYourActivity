import React, {FormEvent} from 'react';
import {IAppUser} from "../../../domain/IAppUser";
import {ButtonGroup, Table} from "react-bootstrap";
import ConfirmModal from "../../../components/modal/ConfirmModal";

interface IProps {
    users: IAppUser[],
    isAdmin: boolean,
    onKick: (event: FormEvent<HTMLButtonElement>, userId: string) => void,
    onBlock: (event: FormEvent<HTMLButtonElement>, userId: string) => void,
}
const DisplayUsers = (props: IProps) => {

    return (
        <div className="p-3">
            <h4>Users</h4>
            <hr/>
            <Table striped>
                <tbody>
                {props.users.map(user => (
                    <tr>
                        <td className="align-middle">{user.firstName}</td>
                        <td className="align-middle">{user.lastName}</td>
                        <td>
                            <ButtonGroup className="w-100" style={{ 'display': props.isAdmin ? '' : 'none' }}>
                                <ConfirmModal name={user.firstName + " " + user.lastName} buttonName="Block" title="Confirm block"
                                              message="Block user:" onSubmit={(e) => props.onBlock(e, user.id!)}
                                              buttonStyle="danger"/>
                                <ConfirmModal name={user.firstName + " " + user.lastName} buttonName="Kick" title="Confirm kick"
                                              message="Kick user:" onSubmit={(e) => props.onKick(e, user.id!)}
                                              buttonStyle="danger"/>
                            </ButtonGroup>

                            <ButtonGroup className="w-50 pull-right" style={{ 'display': props.isAdmin ? 'none' : '' }}>
                                <ConfirmModal name={user.firstName + " " + user.lastName} buttonName="Block" title="Confirm block"
                                              message="Block user:" onSubmit={(e) => props.onBlock(e, user.id!)}
                                              buttonStyle="danger"/>
                            </ButtonGroup>
                        </td>
                    </tr>
                ))}
                </tbody>
            </Table>
        </div>
    );
};

export default DisplayUsers;