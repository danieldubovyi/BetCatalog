import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import './PersonDetails.css';

interface PersonDetails {
  id: number;
  fio: string;
  telegramId: string;
  phoneNumber: string;
  birthDate: Date;
  passportDate: Date;
  personType: number;
}

function PersonDetails() {
  const [PersonDetails, setPersonDetails] = useState<PersonDetails>();
  const { id } = useParams();

  useEffect(() => {
    populateComponentDetailsData(id);
  }, []);
  const navigate = useNavigate();

  const goToUpdatePersonPage = () => {
    navigate(`/persons/${id}/update`)
  }

  const goToPersonAccountsListPage = () => {
    navigate(`/persons/${id}/accounts`)
  }

  const goToPersonBankAccountsListPage = () => {
    navigate(`/persons/${id}/bankAccounts`)
  }


  const contents = PersonDetails === undefined
    ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
    : (
      <div className="account-details">
        <div className="account-field">
          <strong>Id:</strong> {PersonDetails.id}
        </div>
        <div className="account-field">
          <strong>ФИО</strong> {PersonDetails.fio}
        </div>
        <div className="account-field">
          <strong>Telegram ID:</strong> {PersonDetails.telegramId}
        </div>
        <div className="account-field">
          <strong>Номер телефона:</strong> {PersonDetails.phoneNumber}
        </div>
        <div className="account-field">
          <strong>Дата рождения:</strong> {PersonDetails.birthDate.toString()}
        </div>
        <div className="account-field">
          <strong>Дата паспорта:</strong> {PersonDetails.passportDate.toString()}
        </div>
        <div className="account-field">
          <strong>Тип:</strong> {PersonDetails.personType === 1 ? 'Drop' : 'Dropovod'}
        </div>
      </div>
    );


  return (
    <div>
      <h1 id="tabelLabel">Дроп</h1>
      <p>Детали Дропа</p>
      <p>ID ARG: {id}</p>
      {contents}
      <button onClick={() => goToPersonAccountsListPage()} className="btn">Список аккаунтов</button>
      <button onClick={() => goToPersonBankAccountsListPage()} className="btn">Список банков</button>
      <button onClick={() => goToUpdatePersonPage()} className="btn">Update Person Data</button>
    </div>
  );

  async function populateComponentDetailsData(id: string | undefined) {
    const response = await fetch(`http://localhost:5000/api/persons/${id}`);
    const data = await response.json();
    setPersonDetails(data);
  }

}
export default PersonDetails;
