import './App.css';
import { BrowserRouter as Router, Route, Link, Routes } from "react-router-dom";
import Accounts from "./Accounts/Accounts";
import AccountCreate from "./Accounts/AccountCreate";
import AccountDetails from "./Accounts/AccountsDetails";
import BankAccounts from "./BankAccounts/BankAccounts";
import BankAccountsCreate from "./BankAccounts/BankAccountCreate";
import BankAccountsDetails from "./BankAccounts/BankAccountDetails";
import Persons from "./Persons/Persons";
import PersonCreate from "./Persons/PersonCreate";
import PersonDetails from "./Persons/PersonDetails";
import PersonAccounts from "./Persons/PersonAccounts";
import PersonBankAccounts from "./Persons/PersonBankAccounts";

function App() {
    return (
        <div className="App">
            <Router>
                <div className='Sidebar'>
                    <nav>
                        <ul>
                            <li><Link to="/accounts">Аккаунты</Link></li>
                            <li><Link to="/bankAccounts">Банки</Link></li>
                            <li><Link to="/persons">Дропы</Link></li>
                        </ul>
                    </nav>
                </div>
                <div className='Content'>
                    <Routes>
                        <Route path="/accounts" element={<Accounts />} />
                        <Route path="/accounts/create" element={<AccountCreate />} />
                        <Route path="/accounts/:id" element={<AccountDetails />} />
                        <Route path="/bankAccounts" element={<BankAccounts />} />
                        <Route path="/bankAccounts/create" element={<BankAccountsCreate />} />
                        <Route path="/bankAccounts/:id" element={<BankAccountsDetails />} />
                        <Route path="/persons" element={<Persons />} />
                        <Route path="/persons/create" element={<PersonCreate />} />
                        <Route path="/persons/:id" element={<PersonDetails />} />
                        <Route path="/persons/:id/accounts" element={<PersonAccounts />} />
                        <Route path="/persons/:id/bankAccounts" element={<PersonBankAccounts />} />
                    </Routes>
                </div>
            </Router>
        </div>
    );
}

export default App;
