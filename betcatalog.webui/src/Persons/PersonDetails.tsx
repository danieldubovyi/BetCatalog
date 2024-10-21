import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import './PersonDetails.css';

interface PersonDetails {
    id: number;
    fio: string;
    telegramId: string;
    phoneNumber: string;
    bank: string;
    birthDate: Date;
    passportDate: Date;
}

function PersonDetails() {
    const [PersonDetails, setPersonDetails] = useState<PersonDetails>();
    const { id } = useParams();

    useEffect(() => {
        populateComponentDetailsData(id);
    }, []);
    const navigate = useNavigate();

    const goToCPersonAccountsListPage = (id: number) => {
        navigate(`/persons/${id}/accounts`)
    }


    const contents = PersonDetails === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>ФИО</th>
                    <th>Telegram ID</th>
                    <th>Номер телефона</th>
                    <th>Банк</th>
                    <th>Дата рождения</th>
                    <th>Дата паспорта</th>
                    <th>Button</th>
                </tr>
            </thead>
            <tbody>
                <td>{PersonDetails.id}</td>
                <td>{PersonDetails.fio}</td>
                <td>{PersonDetails.telegramId}</td>
                <td>{PersonDetails.phoneNumber}</td>
                <td>{PersonDetails.bank}</td>
                <td>{PersonDetails.birthDate.toString()}</td>
                <td>{PersonDetails.passportDate.toString()}</td>
                <td><button onClick={() => goToCPersonAccountsListPage(PersonDetails.id)} className="btn">Список аккаунтов</button></td>
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Дроп</h1>
            <p>Детали Дропа</p>
            <p>ID ARG: {id}</p>
            {contents}
        </div>
    );

    async function populateComponentDetailsData(id: string | undefined) {
        const response = await fetch(`http://localhost:5000/api/persons/${id}`);
        const data = await response.json();
        setPersonDetails(data);
    }

}
export default PersonDetails;