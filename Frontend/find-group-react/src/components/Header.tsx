import { Link } from "react-router-dom";
import IdentityHeader from "./IdentityHeader";
import useAuth from "../hooks/useAuth";
import {NavDropdown} from "react-bootstrap";
const Header = () => {
    const { jwtResponse } = useAuth();

    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container-fluid">
                    <Link className="navbar-brand" to="/">Activity Planner</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                            aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item" style={{ 'display': jwtResponse == null ? 'none' : '' }}>
                                <Link to="user/groups" className="nav-link text-dark">Groups</Link>
                            </li>
                            <li className="nav-item" style={{ 'display': jwtResponse == null ? 'none' : '' }}>
                                <Link to="user/events" className="nav-link text-dark">Events</Link>
                            </li>
                            <NavDropdown title="User" id="basic-nav-dropdown" style={{ 'display': jwtResponse == null ? 'none' : '' }}>
                                <NavDropdown.Item as={Link} to="user/invitations/">Invitations</NavDropdown.Item>
                                <NavDropdown.Item as={Link} to="user/blocked-users">Blocked users</NavDropdown.Item>
                            </NavDropdown>
                            {/*<li className="nav-item" style={{ 'display': jwtResponse == null ? 'none' : '' }}>*/}
                            {/*    <Link to="user/blocked-users" className="nav-link text-dark">Blocked users</Link>*/}
                            {/*</li>*/}
                        </ul>

                        <ul className="navbar-nav">
                            <IdentityHeader/>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
    );
}

export default Header;
