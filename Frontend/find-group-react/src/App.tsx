import {Route, createBrowserRouter, createRoutesFromElements, RouterProvider} from "react-router-dom";
import Root from "./routes/Root";
import Login from "./routes/identity/Login";
import Register from "./routes/identity/Register";
import Info from "./routes/identity/Info";
import ErrorPage from "./routes/ErrorPage";
import PersistLogin from "./components/PersistLogin";
import RequireAuth from "./components/RequireAuth";
import Home from "./routes/Home";
import PublicGroups from "./routes/group/public/PublicGroups";
import UserGroups from "./routes/group/UserGroups";
import FilterEvents from "./routes/event/FilterEvents";
import UserEvents from "./routes/event/UserEvents";
import PublicGroup from "./routes/group/public/PublicGroup";
import Group from "./routes/group/Group";
import CreateGroup from "./routes/group/create/CreateGroup";
import GroupInvitations from "./routes/group/invite/GroupInvitations";
import GroupUsers from "./routes/group/users/GroupUsers";
import InviteToGroup from "./routes/group/invite/InviteToGroup";
import BlockedUsers from "./routes/blockedUsers/BlockedUsers";
import GroupConversation from "./routes/group/conversation/GroupConversation";
import GroupEvents from "./routes/group-event/GroupEvents";
import CreateGroupEvent from "./routes/group-event/create/CreateGroupEvent";
import GroupEvent from "./routes/group-event/GroupEvent";
import UserEvent from "./routes/event/UserEvent";
import CreateUserEvent from "./routes/event/create/CreateUserEvent";

const router = createBrowserRouter(
    createRoutesFromElements(
        <Route errorElement={<ErrorPage />}>
            <Route path="/" element={<Root />}>
                {/* public routes */}
                <Route path="login/" element={<Login />} />
                <Route path="register/" element={<Register />} />

                {/* protected routes */}
                <Route element={<PersistLogin />}>
                    <Route element={<RequireAuth />}>
                        <Route path="info/" element={<Info />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="/" element={<UserEvents />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="groups/" element={<PublicGroups />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="events/" element={<FilterEvents />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="group/" element={<PublicGroup />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="group/:groupId" element={<Group/>}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="group/conversation/:groupId" element={<GroupConversation/>}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="group/users/:groupId" element={<GroupUsers/>}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="group/invite/:groupId" element={<InviteToGroup/>}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="group/events/:groupId" element={<GroupEvents/>}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="group/event/:eventId/:groupId" element={<GroupEvent/>}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="group/add-event/:groupId" element={<CreateGroupEvent/>}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="user/groups/" element={<UserGroups />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="user/add-group" element={<CreateGroup />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="user/events/" element={<UserEvents />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="user/event/:eventId" element={<UserEvent />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="user/add-event/" element={<CreateUserEvent />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="user/invitations/" element={<GroupInvitations />}/>
                    </Route>

                    <Route element={<RequireAuth />}>
                        <Route path="user/blocked-users/" element={<BlockedUsers />}/>
                    </Route>


                </Route>
            </Route>
        </Route>
    )
)

function App() {
    return (
        <RouterProvider router={router} />
    )
}
export default App;