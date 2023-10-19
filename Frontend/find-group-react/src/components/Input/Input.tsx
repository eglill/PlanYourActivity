import {IFormHandler} from "../../helpers/IFormHandler";
import Form from "react-bootstrap/Form";

export const Input = ({ label, type, id, name, value, placeholder, changeHandler, autoComplete, pattern, errorMsg, required}:
                          { label: string, type: string, id: string, name: string, value: string,
                              placeholder: string, changeHandler: IFormHandler, autoComplete: string,
                          pattern: string, errorMsg: string, required: boolean})=> {
    return (
        <div className="form-floating mb-2">
            <Form.Control
                onChange={(e) => changeHandler(e.target)}
                id={id}
                size="sm"
                type={type}
                className="form-control"
                placeholder={placeholder}
                name={name}
                value={value}
                autoComplete={autoComplete}
                pattern={pattern}
                required={required}
            />
            <Form.Label htmlFor={id}>{label}</Form.Label>
            <Form.Control.Feedback type="invalid">{errorMsg}</Form.Control.Feedback>
        </div>
    );
}