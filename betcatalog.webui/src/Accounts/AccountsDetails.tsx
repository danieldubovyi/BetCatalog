import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import './AccountsDetails.css';

interface AccountsDetails {
    id: number;
    status: string;
    login: string;
    password: string;
}

function AccountsDetails() {
    const [AccountsDetails, setAccountDetails] = useState<AccountsDetails>();
    const { id } = useParams();

    useEffect(() => {
        populateAccountDetailsData(id);
    }, []);
    

    const contents = AccountsDetails === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Статус</th>
                    <th>Логин</th>
                    <th>Пароль</th>
                    <th>Button</th>
                </tr>
            </thead>
            <tbody>
                    <tr key={AccountsDetails.id}>
                    <td>{AccountsDetails.id}</td>
                    <td>{AccountsDetails.status}</td>
                    <td>{AccountsDetails.login}</td>
                    <td>{AccountsDetails.password}</td>
                </tr>
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Аккаунт</h1>
            <p>Детали аккаунта</p>
            <p>ID ARG: {id}</p>
            {contents}
        </div>
    );

    async function populateAccountDetailsData(id: string | undefined) {
        const response = await fetch(`http://localhost:5000/api/accounts/${id}`);
        const data = await response.json();
        setAccountDetails(data);
    }

}
export default AccountsDetails;