import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import './Accounts.css';

interface Account {
  id: number;
  status: number;
  login: string;
  password: string;
}

function Accounts() {
  const [Accounts, setAccounts] = useState<Account[]>();

  useEffect(() => {
    populateAccountsData();
  }, []);
  const navigate = useNavigate();

  const goToItemPage = (id: number) => {
    navigate(`/accounts/${id}`)
  }
  const goToAccountCreate = () => {
    navigate(`/accounts/create`)
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

  const contents = Accounts === undefined
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
        {Accounts.map(Account =>
          <tr key={Account.id}>
            <td>{Account.id}</td>
            <td>{convertStatusNumToString(Account.status)}</td>
            <td>{Account.login}</td>
            <td>{Account.password}</td>
            <td><button onClick={() => goToItemPage(Account.id)} className="btn">Детали аккаунта</button></td>
          </tr>
        )}
      </tbody>
    </table>;

  return (
    <div>
      <h1 id="tabelLabel">Аккаунты</h1>
      <p>Список аккаунтов</p>
      {contents}
      <button onClick={() => goToAccountCreate()} className="btn">Создать аккаунт</button>
    </div>
  );

  async function populateAccountsData() {
    const response = await fetch('http://localhost:5000/api/accounts');
    const data = await response.json();
    setAccounts(data);
  }

}


export default Accounts;
