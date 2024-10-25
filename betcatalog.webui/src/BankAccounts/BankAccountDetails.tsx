import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
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

  const navigate = useNavigate();

  const goToUpdateAccountPage = () => {
    navigate(`/bankAccounts/${id}/update`)
  }

  const contents = BankAccountsDetails === undefined
    ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
    : (
      <div className="account-details">
        <div className="account-field">
          <strong>Id:</strong> {BankAccountsDetails.id}
        </div>
        <div className="account-field">
          <strong>Bank Name:</strong> {BankAccountsDetails.bankName}
        </div>
        <div className="account-field">
          <strong>Пин:</strong> {BankAccountsDetails.pinCode}
        </div>
        <div className="account-field">
          <strong>ИНН:</strong> {BankAccountsDetails.inn}
        </div>
        <div className="account-field">
          <strong>CVV:</strong> {BankAccountsDetails.cvv}
        </div>
        <div className="account-field">
          <strong>Метод оплаты:</strong> {BankAccountsDetails.paymentType === 1 ? 'Visa' : 'Mastercard'}
        </div>
        <div className="account-field">
          <strong>Дата на карте:</strong> {BankAccountsDetails.validationEndDate}
        </div>
      </div>
    );


  return (
    <div>
      <h1 id="tabelLabel">Банк</h1>
      <p>Детали банка</p>
      <p>ID ARG: {id}</p>
      {contents}
      <button onClick={() => goToUpdateAccountPage()} className="btn">Update Account Data</button>
    </div>
  );

  async function populateBankAccountDetailsData(id: string | undefined) {
    const response = await fetch(`http://localhost:5000/api/bankAccounts/${id}`);
    const data = await response.json();
    setBankAccountDetails(data);
  }

}
export default BankAccountsDetails;
