export const ErrorList = ({ errors}: { errors: string[] })=> {
    return (
        <div className="form-floating mb-3">
            <ul className="list-group" style={{'display': errors.length === 0 ? 'none' : ''}}>
                {errors.map(e => (
                    <li className="list-group-item list-group-item-danger">{e}</li>
                ))}
            </ul>
        </div>
    );
}