import './App.css';
import { BrowserRouter as Router, Route, Link, Routes } from "react-router-dom";
import Accounts from "./Accounts/Accounts";
import AccountCreate from "./Accounts/AccountCreate";
import AccountDetails from "./Accounts/AccountsDetails";
import Persons from "./Persons/Persons";
import PersonCreate from "./Persons/PersonCreate";
import PersonDetails from "./Persons/PersonDetails";
import PersonAccounts from "./Persons/PersonAccounts";

function App() {
    return (
        <div className="App">
            <Router>
                <div className='Sidebar'>
                    <nav>
                        <ul>
                            <li><Link to="/accounts">Аккаунты</Link></li>
                            <li><Link to="/persons">Дропы</Link></li>
                        </ul>
                    </nav>
                </div>
                <div className='Content'>
                    <Routes>
                        <Route path="/accounts" element={<Accounts />} />
                        <Route path="/accounts/create" element={<AccountCreate />} />
                        <Route path="/accounts/:id" element={<AccountDetails />} />
                        <Route path="/persons" element={<Persons />} />
                        <Route path="/persons/create" element={<PersonCreate />} />
                        <Route path="/persons/:id" element={<PersonDetails />} />
                        <Route path="/persons/:id/accounts" element={<PersonAccounts />} />
                    </Routes>
                </div>
            </Router>
        </div>
    );
}

export default App;
