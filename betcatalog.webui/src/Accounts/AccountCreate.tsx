import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import './AccountCreate.css';

interface Account {
  status: number;
  login: string;
  password: string;
  personId: number;
}

function AccountCreate() {
  const [account, setAccount] = useState<Account>({
    status: 0,
    login: '',
    password: '',
    personId: 0,
  });

  const navigate = useNavigate();

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = event.target;
    const newValue = name === 'status' || name === 'personId' ? Number(value) : value;

    setAccount({ ...account, [name]: newValue });
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    // API POST request to create new account
    const response = await fetch('http://localhost:5000/api/accounts', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(account)
    });

    if (response.ok) {
      // After successful creation, navigate to the accounts page or give feedback
      navigate('/accounts');
    } else {
      // Handle errors (e.g., display an error message)
      alert('Error creating account');
    }
  };

  return (
    <div>
      <h1>Создать аккаунт</h1>
      <form onSubmit={handleSubmit}>

        <div className="form-group">
          <label htmlFor="status">Статус(Число):</label>
          <select
            name="status"
            value={account.status}
            onChange={handleInputChange}
            required
          >
            <option value={1}>Ok</option>
            <option value={2}>Limit</option>
            <option value={3}>Banned</option>
          </select>
        </div>

        <div className="form-group">
          <label htmlFor="login">Логин:</label>
          <input
            type="text"
            name="login"
            value={account.login}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="password">Пароль:</label>
          <input
            type="password"
            name="password"
            value={account.password}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="personId">Person Id(Число):</label>
          <input
            type="number"
            name="personId"
            value={account.personId}
            onChange={handleInputChange}
            required
          />
        </div>

        <button type="submit" className="btn">Создать аккаунт</button>
      </form>
    </div>
  );
}

export default AccountCreate;
