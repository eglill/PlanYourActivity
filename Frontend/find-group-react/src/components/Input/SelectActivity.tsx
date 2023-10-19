import {IFormHandler} from "../../helpers/IFormHandler";
import {IActivity} from "../../domain/IActivity";
import Form from "react-bootstrap/Form";
import Feedback from "react-bootstrap/Feedback";

export const SelectActivity = ({ label, id, name, value, changeHandler, activities, placeholder, errorMsg, required}:
                                  { label: string, id: string, name: string, value: string | undefined,
                                      changeHandler: IFormHandler, activities: IActivity[],
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
                {activities.map(activity => (
                    <option key={activity.id} value={activity.id}>{activity.name}</option>))}
            </Form.Select>
            <Form.Label htmlFor={id}>{label}</Form.Label>
            <Feedback type="invalid">{errorMsg}</Feedback>
        </div>
    );
}