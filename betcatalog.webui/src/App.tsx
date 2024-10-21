import './App.css';
import { BrowserRouter as Router, Route, Link, Routes } from "react-router-dom";
import Accounts from "./Accounts/Accounts";
import AccountDetails from "./Accounts/AccountsDetails";
import Persons from "./Persons/Persons";
import PersonDetails from "./Persons/PersonDetails";

function App() {
    return (
        <div className="App">
            <Router>
                <div className='Sidebar'>
                    <nav>
                        <ul>
                            <li><Link to="/Accounts">Accounts</Link></li>
                            <li><Link to="/Persons">Persons</Link></li>
                        </ul>
                    </nav>
                </div>
                <div className='Content'>
                    <Routes>
                        <Route path="/accounts" element={<Accounts />} />
                        <Route path="/accounts/:id" element={<AccountDetails />} />
                        <Route path="/persons" element={<Persons />} />
                        <Route path="/persons/:id" element={<PersonDetails />} />
                    </Routes>
                </div>
            </Router>
        </div>
    );
}

export default App;
