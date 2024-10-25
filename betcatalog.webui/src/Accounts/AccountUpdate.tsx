import { useState, useEffect } from 'react';
import { useParams, useNavigate } from "react-router-dom";
import './AccountUpdate.css'; // You can reuse the same CSS file

interface Account {
  status: number;
  login: string;
  password: string;
  personId: number;
}

function AccountUpdate() {
  const [account, setAccount] = useState<Account>({
    status: 0,
    login: '',
    password: '',
    personId: 0,
  });

  const { id } = useParams(); // Get the account ID from the URL
  const navigate = useNavigate();

  // Fetch existing account data when the component mounts
  useEffect(() => {
    const fetchAccount = async () => {
      try {
        const response = await fetch(`http://localhost:5000/api/accounts/${id}`);
        if (response.ok) {
          const data = await response.json();
          setAccount(data); // Populate form fields with the existing account data
        } else {
          console.error("Failed to fetch account data");
        }
      } catch (error) {
        console.error("Error:", error);
      }
    };

    fetchAccount();
  }, [id]);

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = event.target;
    const newValue = name === 'status' || name === 'personId' ? Number(value) : value;
    setAccount({ ...account, [name]: newValue });
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    // API PUT request to update the account
    const response = await fetch(`http://localhost:5000/api/accounts/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(account) // Send the updated account data
    });

    if (response.ok) {
      // After successful update, navigate to the accounts page or give feedback
      navigate('/accounts');
    } else {
      // Handle errors (e.g., display an error message)
      alert('Error updating account');
    }
  };

  return (
    <div>
      <h1>Обновить аккаунт</h1>
      <form onSubmit={handleSubmit}>

        <div className="form-group">
          <label htmlFor="status">Статус(Число):</label>
          <select
            name="status"
            value={account.status}
            onChange={handleInputChange}
            required
          >
            <option hidden>Choose here</option>
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

        <button type="submit" className="btn">Обновить аккаунт</button>
      </form>
    </div>
  );
}

export default AccountUpdate;
