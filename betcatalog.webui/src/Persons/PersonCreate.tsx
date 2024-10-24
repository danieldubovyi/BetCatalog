import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import './PersonCreate.css';

interface Person {
  fio: string;
  telegramId: string;
  phoneNumber: string;
  birthDate: string;
  passportDate: string;
  personType: number;
}

function PersonCreate() {
  const [person, setPerson] = useState<Person>({
    fio: '',
    telegramId: '',
    phoneNumber: '',
    birthDate: '',
    passportDate: '',
    personType: 0,
  });

  const navigate = useNavigate();

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = event.target;
    const newValue = name === 'personType' ? Number(value) : value;

    setPerson({ ...person, [name]: newValue });
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    // API POST request to create new Person
    const response = await fetch('http://localhost:5000/api/persons', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(person)
    });

    if (response.ok) {
      // After successful creation, navigate to the Persons page or give feedback
      navigate('/persons');
    } else {
      // Handle errors (e.g., display an error message)
      alert('Error creating Person');
    }
  };

  return (
    <div>
      <h1>Создать Дропа</h1>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="fio">ФИО:</label>
          <input
            type="text"
            name="fio"
            value={person.fio}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="telegramId">Telegram ID:</label>
          <input
            type="text"
            name="telegramId"
            value={person.telegramId}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="phoneNumber">Номер телефона:</label>
          <input
            type="text"
            name="phoneNumber"
            value={person.phoneNumber}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="birthDate">Дата рождения:</label>
          <input
            type="date"
            name="birthDate"
            value={person.birthDate}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="passportDate">Дата паспорта:</label>
          <input
            type="date"
            name="passportDate"
            value={person.passportDate}
            onChange={handleInputChange}
            required
          />
        </div>

        <div className="form-group">
          <label htmlFor="personType">Тип:</label>
          <select
            name="personType"
            value={person.personType}
            onChange={handleInputChange}
            required
          >
            <option selected hidden>Choose here</option>
            <option value={1}>Drop</option>
            <option value={2}>Dropovod</option>
          </select>
        </div>

        <button type="submit" className="btn">Создать</button>
      </form>
    </div>
  );
}

export default PersonCreate;
