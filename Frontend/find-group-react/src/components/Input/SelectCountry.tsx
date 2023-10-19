import {IFormHandler} from "../../helpers/IFormHandler";
import {ICountry} from "../../domain/ICountry";
import Form from "react-bootstrap/Form";
import Feedback from "react-bootstrap/Feedback";

export const SelectCountry = ({ label, id, name, value, changeHandler, countries, placeholder, errorMsg, required}:
                                 { label: string, id: string, name: string, value: string | undefined,
                                     changeHandler: IFormHandler, countries: ICountry[],
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
                {countries.map(country => (
                    <option key={country.id} value={country.id}>{country.name}</option>))}
            </Form.Select>
            <Form.Label htmlFor={id}>{label}</Form.Label>
            <Feedback type="invalid">{errorMsg}</Feedback>
        </div>
    );
}