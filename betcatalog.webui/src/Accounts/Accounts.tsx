import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import './Accounts.css';

interface Account {
    id: number;
    type: string;
    isOrdered: boolean
    isDelivered: boolean;
}

function Accounts() {
    const [Accounts, setAccounts] = useState<Account[]>();

    useEffect(() => {
        populateAccountsData();
    }, []);
    const navigate = useNavigate();

    const goToItemPage = (id: number) => {
        navigate(`/Accounts/${id}`)
    }

    const contents = Accounts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Type</th>
                    <th>Is Ordered</th>
                    <th>Is Delivered</th>
                    <th>Button</th>
                </tr>
            </thead>
            <tbody>
                {Accounts.map(Account =>
                    <tr key={Account.id}>
                        <td>{Account.id}</td>
                        <td>{Account.type}</td>
                        <td>{Account.isOrdered === true ? "True" : "False"}</td>
                        <td>{Account.isDelivered === true ? "True" : "False"}</td>
                        <td><button onClick={() => goToItemPage(Account.id)} className="btn">See</button></td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Accounts</h1>
            <p>Getting Accounts list</p>
            {contents}
        </div>
    );

    async function populateAccountsData() {
        const response = await fetch('http://localhost:5000/api/accounts');
        const data = await response.json();
        setAccounts(data);
    }

}


export default Accounts;