import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import './BankAccounts.css';

interface BankAccount {
  id: number;
  bankName: string;
  paymentType: number;
  cardNumber: string;
  cvv: string;
  validationEndDate: Date;
}

function BankAccounts() {
  const [BankAccounts, setBankAccounts] = useState<BankAccount[]>();

  useEffect(() => {
    populateBankAccountsData();
  }, []);
  const navigate = useNavigate();

  const goToItemPage = (id: number) => {
    navigate(`/bankAccounts/${id}`)
  }
  const goToBankAccountCreate = () => {
    navigate(`/bankAccounts/create`)
  }

  const contents = BankAccounts === undefined
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
        {BankAccounts.map(BankAccount =>
          <tr key={BankAccount.id}>
            <td>{BankAccount.id}</td>
            <td>{BankAccount.bankName}</td>
            <td>{BankAccount.paymentType === 1 ? 'Visa' : 'Mastercard'}</td>
            <td>{BankAccount.cardNumber}</td>
            <td>{BankAccount.cvv}</td>
            <td>{BankAccount.validationEndDate.toString()}</td>
            <td><button onClick={() => goToItemPage(BankAccount.id)} className="btn">Детали банка</button></td>
          </tr>
        )}
      </tbody>
    </table>;

  return (
    <div>
      <h1 id="tabelLabel">Банки</h1>
      <p>Список банков</p>
      {contents}
      <button onClick={() => goToBankAccountCreate()} className="btn">Создать банк</button>
    </div>
  );

  async function populateBankAccountsData() {
    const response = await fetch('http://localhost:5000/api/bankAccounts');
    const data = await response.json();
    setBankAccounts(data);
  }

}


export default BankAccounts;
