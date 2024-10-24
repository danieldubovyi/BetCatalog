import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import './PersonBankAccounts.css';

interface PersonBankAccounts {
  bankName: string;
  id: number;
  paymentType: number;
  cardNumber: string;
  cvv: string;
  validationEndDate: Date;
}

function PersonBankAccounts() {
  const [personBankAccounts, setPersonBankAccounts] = useState<PersonBankAccounts[]>();
  const { id } = useParams();

  useEffect(() => {
    populatePersonsBankAccountsData(id);
  }, []);

  const navigate = useNavigate();

  const goToBankAccountDetailsPage = (id: number) => {
    navigate(`/bankAccounts/${id}`)
  }

  const createBankAccount = () => {
    navigate(`/bankAccounts/create`)
  }

  const contents = personBankAccounts === undefined
    ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
    : <table className="table table-striped" aria-labelledby="tabelLabel">
      <thead>
        <tr>
          <th>Id</th>
          <th>Bank Name</th>
          <th>Метод оплаты</th>
          <th>Номер карты</th>
          <th>CVV</th>
          <th>Дата на карте</th>
          <th>Button</th>
        </tr>
      </thead>
      <tbody>
        {personBankAccounts.map(personBankAccounts =>
          <tr key={personBankAccounts.id}>
            <td>{personBankAccounts.id}</td>
            <td>{personBankAccounts.bankName}</td>
            <td>{personBankAccounts.paymentType === 1 ? 'Visa' : 'Mastercard'}</td>
            <td>{personBankAccounts.cardNumber}</td>
            <td>{personBankAccounts.cvv}</td>
            <td>{personBankAccounts.validationEndDate.toString()}</td>
            <td><button onClick={() => goToBankAccountDetailsPage(personBankAccounts.id)} className="btn">Детали</button></td>
          </tr>
        )}
      </tbody>
    </table>;

  return (
    <div>
      <h1 id="tabelLabel">Банки этого дропа</h1>
      <p>Список банков</p>
      {contents}
      <button onClick={() => createBankAccount()} className="btn" >Создать банк</button>
    </div>
  );

  async function populatePersonsBankAccountsData(id: string | undefined) {
    const response = await fetch(`http://localhost:5000/api/persons/${id}/BankAccounts`);
    const data = await response.json();
    setPersonBankAccounts(data);
  }
}

export default PersonBankAccounts;
