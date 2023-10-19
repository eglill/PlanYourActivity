import {IFormHandler} from "../../helpers/IFormHandler";
import Form from "react-bootstrap/Form";

export const InputNumber = ({ label, id, name, value, placeholder, changeHandler, required, min, max, errorMsg}:
                          { label: string, id: string, name: string, value: number | undefined,
                              placeholder: string, changeHandler: IFormHandler, errorMsg: string,
                              required: boolean, min: number | undefined, max: number | undefined})=> {
    return (
        <div className="form-floating mb-2">
            <Form.Control
                onChange={(e) => changeHandler(e.target)}
                id={id}
                type="number"
                size="sm"
                className="form-control"
                placeholder={placeholder}
                name={name}
                value={value}
                required={required}
                min={min}
                max={max}
            />
            <Form.Label htmlFor={id}>{label}</Form.Label>
            <Form.Control.Feedback type="invalid">{errorMsg}</Form.Control.Feedback>
        </div>
    );
}