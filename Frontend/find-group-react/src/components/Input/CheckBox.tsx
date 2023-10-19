import {IFormHandler} from "../../helpers/IFormHandler";
import Form from "react-bootstrap/Form";

export const CheckBox = ({ label, id, name, changeHandler, checked }:
                          { label: string, id: string, name: string,
                              changeHandler: IFormHandler, checked: boolean})=> {
    return (
        <div className="form-floating mb-3">
            <Form.Check
                type="checkbox"
                onChange={(e) => changeHandler(e.target)}
                checked={checked}
                id={id}
                name={name}
                label={label}/>
            <Form.Label htmlFor={id} />
        </div>
    );
}