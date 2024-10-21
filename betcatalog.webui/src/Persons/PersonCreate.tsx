import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import './PersonCreate.css';

interface Person {
    fio: string;
    telegramId: string;
    phoneNumber: string;
    bank: string;
    birthDate: string;
    passportDate: string;
}

function PersonCreate() {
    const [person, setPerson] = useState<Person>({
        fio: '',
        telegramId: '',
        phoneNumber: '',
        bank: '',
        birthDate: '',
        passportDate: '',
    });
    
    const navigate = useNavigate();

    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = event.target;

        setPerson({ ...person, [name]: value });
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
                    <label htmlFor="bank">Банк:</label>
                    <input
                        type="text"
                        name="bank"
                        value={person.bank}
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

                <button type="submit" className="btn">Создать</button>
            </form>
        </div>
    );
}

export default PersonCreate;
