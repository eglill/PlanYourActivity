import {IFormHandler} from "../../helpers/IFormHandler";
import {IColour} from "../../domain/IColour";
import Form from "react-bootstrap/Form";
import Feedback from "react-bootstrap/Feedback";

export const SelectColour = ({ label, id, name, value, changeHandler, colours, placeholder, errorMsg, required}:
                                  { label: string, id: string, name: string, value: string | undefined,
                                      changeHandler: IFormHandler, colours: IColour[],
                                      placeholder: string, errorMsg: string, required: boolean})=> {
    return (
        <div className="form-floating mb-2">
            <Form.Select className="form-select"
                         size="sm"
                         aria-required="true"
                         value={value}
                         aria-label={label}
                         id={id}
                         onChange={(e) => changeHandler(e.target)}
                         name={name}
                         required={required}>
                <option hidden={true} disabled value="">{placeholder}</option>
                {colours.map(colour => (
                    <option key={colour.id} value={colour.id} style={{backgroundColor: colour.hex}}>{colour.hex}</option>))}
            </Form.Select>
            <Form.Label htmlFor={id}>{label}</Form.Label>
            <Feedback type="invalid">{errorMsg}</Feedback>
        </div>
    );
}