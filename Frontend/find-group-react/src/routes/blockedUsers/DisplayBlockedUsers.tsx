import React, {FormEvent} from 'react';
import {IAppUser} from "../../domain/IAppUser";
import {ButtonGroup, Table} from "react-bootstrap";
import ConfirmModal from "../../components/modal/ConfirmModal";

interface IProps {
    users: IAppUser[],
    onUnblock: (event: FormEvent<HTMLButtonElement>, userId: string) => void,
}
const DisplayBlockedUsers = (props: IProps) => {
    return (
        <div className="p-3">
            <h4>Blocked users</h4>
            <hr/>
            <Table striped>
                <tbody>
                {props.users.map(user => (
                    <tr>
                        <td className="align-middle">{user.firstName}</td>
                        <td className="align-middle">{user.lastName}</td>
                        <td>
                            <ButtonGroup className="w-50 pull-right">
                                <ConfirmModal name={user.firstName + " " + user.lastName} buttonName="Unblock" title="Confirm unblock"
                                              message="Unblock user:" onSubmit={(e) => props.onUnblock(e, user.id!)}
                                              buttonStyle="warning"/>
                            </ButtonGroup>
                        </td>
                    </tr>
                ))}
                </tbody>
            </Table>
        </div>
    );
};

export default DisplayBlockedUsers;