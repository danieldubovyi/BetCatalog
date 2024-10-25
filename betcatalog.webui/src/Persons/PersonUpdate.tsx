import { useState, useEffect } from 'react';
import { useParams, useNavigate } from "react-router-dom";
import './PersonUpdate.css'; // You can reuse the same CSS file

interface Person {
  fio: string;
  telegramId: string;
  phoneNumber: string;
  birthDate: string;
  passportDate: string;
  personType: number;
}

function PersonUpdate() {
  const [person, setPerson] = useState<Person>({
    fio: '',
    telegramId: '',
    phoneNumber: '',
    birthDate: '',
    passportDate: '',
    personType: 0,
  });

  const { id } = useParams(); // Get the account ID from the URL
  const navigate = useNavigate();

  // Fetch existing account data when the component mounts
  useEffect(() => {
    const fetchPerson = async () => {
      try {
        const response = await fetch(`http://localhost:5000/api/persons/${id}`);
        if (response.ok) {
          const data = await response.json();
          setPerson(data); // Populate form fields with the existing account data
        } else {
          console.error("Failed to fetch person data");
        }
      } catch (error) {
        console.error("Error:", error);
      }
    };

    fetchPerson();
  }, [id]);

  const handleInputChange = (event: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = event.target;
    const newValue = name === 'personType' ? Number(value) : value;
    setPerson({ ...person, [name]: newValue });
  };

  const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    // API PUT request to update the account
    const response = await fetch(`http://localhost:5000/api/persons/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(person) // Send the updated account data
    });

    if (response.ok) {
      // After successful update, navigate to the accounts page or give feedback
      navigate('/persons');
    } else {
      // Handle errors (e.g., display an error message)
      alert('Error updating person');
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
            <option hidden>Choose here</option>
            <option value={1}>Drop</option>
            <option value={2}>Dropovod</option>
          </select>
        </div>

        <button type="submit" className="btn">Создать банк</button>
      </form>
    </div>
  );
}

export default PersonUpdate;
