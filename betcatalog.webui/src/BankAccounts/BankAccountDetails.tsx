import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import './BankAccountDetails.css';

interface BankAccountsDetails {
  id: number;
  bankName: string;
  pinCode: string;
  inn: string;
  cardNumber: string;
  cvv: string;
  paymentType: number;
  validationEndDate: string;
}

function BankAccountsDetails() {
  const [BankAccountsDetails, setBankAccountDetails] = useState<BankAccountsDetails>();
  const { id } = useParams();

  useEffect(() => {
    populateBankAccountDetailsData(id);
  }, []);


  const contents = BankAccountsDetails === undefined
    ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
    : <table className="table table-striped" aria-labelledby="tabelLabel">
      <thead>
        <tr>
          <th>Id</th>
          <th>Bank Name</th>
          <th>Пин</th>
          <th>ИНН</th>
          <th>Номер карты</th>
          <th>CVV</th>
          <th>Метод оплаты</th>
          <th>Дата на карте</th>
        </tr>
      </thead>
      <tbody>
        <tr key={BankAccountsDetails.id}>
          <td>{BankAccountsDetails.id}</td>
          <td>{BankAccountsDetails.bankName}</td>
          <td>{BankAccountsDetails.pinCode}</td>
          <td>{BankAccountsDetails.inn}</td>
          <td>{BankAccountsDetails.cardNumber}</td>
          <td>{BankAccountsDetails.cvv}</td>
          <td>{BankAccountsDetails.paymentType === 1 ? 'Visa' : 'Mastercard'}</td>
          <td>{BankAccountsDetails.validationEndDate}</td>
        </tr>
      </tbody>
    </table>;

  return (
    <div>
      <h1 id="tabelLabel">Банк</h1>
      <p>Детали банка</p>
      <p>ID ARG: {id}</p>
      {contents}
    </div>
  );

  async function populateBankAccountDetailsData(id: string | undefined) {
    const response = await fetch(`http://localhost:5000/api/bankAccounts/${id}`);
    const data = await response.json();
    setBankAccountDetails(data);
  }

}
export default BankAccountsDetails;
