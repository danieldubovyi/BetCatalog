import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import './BankAccountCreate.css';

interface BankAccount {
  bankName: string;
  pinCode: string;
  inn: string;
  cardNumber: string;
  cvv: string;
  paymentType: number;
  validationEndDate: string;
  personId: number;
}

function BankAccountCreate() {
  const [bankAccount, setBankAccount] = useState<BankAccount>({
    bankName: '',
    pinCode: '',
    inn: '',
    cardNumber: '',
    cvv: '',
    paymentType: 0,
    validationEndDate: '',
    personId: 0
  });

  const navigate = useNavigate();

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = event.target;
    const newValue = name === 'paymentType' || name === 'personId' ? Number(value) : value;

    setBankAccount({ ...bankAccount, [name]: newValue });
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    // API POST request to create new BankAccount
    const response = await fetch('http://localhost:5000/api/bankAccounts', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(bankAccount)
    });

    if (response.ok) {
      // After successful creation, navigate to the BankAccounts page or give feedback
      navigate('/bankAccounts');
    } else {
      // Handle errors (e.g., display an error message)
      alert('Error creating BankAccount');
    }
  };

  return (
    <div>
      <h1>Создать банк</h1>
      <form onSubmit={handleSubmit}>

        <div className="form-group">
          <label htmlFor="bankName">Bank Name</label>
          <input
            type="text"
            name="bankName"
            value={bankAccount.bankName}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="pinCode">Пин(Число):</label>
          <input
            type="text"
            name="pinCode"
            value={bankAccount.pinCode}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="inn">ИНН:</label>
          <input
            type="text"
            name="inn"
            value={bankAccount.inn}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="cardNumber">Номер карты:</label>
          <input
            type="text"
            name="cardNumber"
            value={bankAccount.cardNumber}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="cvv">CVV:</label>
          <input
            type="text"
            name="cvv"
            value={bankAccount.cvv}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="paymentType">Метод оплаты:</label>
          <select
            name="paymentType"
            value={bankAccount.paymentType}
            onChange={handleInputChange}
            required
          >
            <option value={1}>Visa</option>
            <option value={2}>Mastercard</option>
          </select>
        </div>

        <div className="form-group">
          <label htmlFor="validationEndDate">Дата на карте:</label>
          <input
            type="date"
            name="validationEndDate"
            value={bankAccount.validationEndDate}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="personId">Person Id(Число):</label>
          <input
            type="number"
            name="personId"
            value={bankAccount.personId}
            onChange={handleInputChange}
            required
          />
        </div>

        <button type="submit" className="btn">Создать банк</button>
      </form>
    </div>
  );
}

export default BankAccountCreate;
