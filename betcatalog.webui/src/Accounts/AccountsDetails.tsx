import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import { useParams } from "react-router-dom";
import './AccountsDetails.css';

interface AccountsDetails {
  id: number;
  status: number;
  login: string;
  password: string;
}

function AccountsDetails() {
  const [AccountsDetails, setAccountDetails] = useState<AccountsDetails>();
  const { id } = useParams();

  useEffect(() => {
    populateAccountDetailsData(id);
  }, []);

  const navigate = useNavigate();

  const goToUpdateAccountPage = () => {
    navigate(`/accounts/${id}/update`)
  }

  const convertStatusNumToString = (statusNum: number) => {
    switch (statusNum) {
      case 1:
        return 'Ok';
      case 2:
        return 'Limit';
      case 3:
        return 'Banned';
    }
  }

  const contents = AccountsDetails === undefined
    ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
    : (
      <div className="account-details">
        <div className="account-field">
          <strong>Id:</strong> {AccountsDetails.id}
        </div>
        <div className="account-field">
          <strong>Статус:</strong> {convertStatusNumToString(AccountsDetails.status)}
        </div>
        <div className="account-field">
          <strong>Логин:</strong> {AccountsDetails.login}
        </div>
        <div className="account-field">
          <strong>Пароль:</strong> {AccountsDetails.password}
        </div>
      </div>
    );
  return (
    <div>
      <h1 id="tabelLabel">Аккаунт</h1>
      <p>Детали аккаунта</p>
      <p>ID ARG: {id}</p>
      {contents}
      <button onClick={() => goToUpdateAccountPage()} className="btn">Update Account Data</button>
    </div>
  );

  async function populateAccountDetailsData(id: string | undefined) {
    const response = await fetch(`http://localhost:5000/api/accounts/${id}`);
    const data = await response.json();
    setAccountDetails(data);
  }

}
export default AccountsDetails;
