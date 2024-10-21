import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import './PersonAccounts.css';

interface PersonAccounts {
    id: number;
    status: string;
    login: string;
    password: string;
}

function PersonAccounts() {
    const [PersonAccounts, setPersonAccounts] = useState<PersonAccounts[]>();
    const { id } = useParams();

    useEffect(() => {
        populatePersonsAccountsData(id);
    }, []);

    const navigate = useNavigate();

    const goToAccountDetailsPage = (id: number) => {
        navigate(`/accounts/${id}`)
    }

    const createAccount = () => {
        navigate(`/accounts/create`)
    }

    const contents = PersonAccounts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>ФИО</th>
                    <th>Telegram ID</th>
                    <th>Button</th>
                </tr>
            </thead>
            <tbody>
                {PersonAccounts.map(PersonAccounts =>
                    <tr key={PersonAccounts.id}>
                        <td>{PersonAccounts.id}</td>
                        <td>{PersonAccounts.status}</td>
                        <td>{PersonAccounts.login}</td>
                        <td>{PersonAccounts.password}</td>
                        <td><button onClick={() => goToAccountDetailsPage(PersonAccounts.id)} className="btn">Детали</button></td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Аккаунты этого дропа</h1>
            <p>Список аккаунтов</p>
            {contents}
            <button onClick={() => createAccount()} className="btn" >Создать аккаунт</button>
        </div>
    );

    async function populatePersonsAccountsData(id: string | undefined) {
        const response = await fetch(`http://localhost:5000/api/persons/${id}/accounts`);
        const data = await response.json();
        setPersonAccounts(data);
    }
}

export default PersonAccounts;