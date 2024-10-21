import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import './Persons.css';

interface Person {
    id: number;
    price: number;
    isCreated: boolean
    isSent: boolean;
}

function Persons() {
    const [Persons, setPersons] = useState<Person[]>();

    useEffect(() => {
        populatePersonsData();
    }, []);

    const navigate = useNavigate();

    const goToPersonDetailsPage = (id: number) => {
        navigate(`/persons/${id}`)
    }

    const createPerson = () => {
        navigate(`/persons/create`)
    }

    const contents = Persons === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Type</th>
                    <th>Is Created</th>
                    <th>Is Sent</th>
                    <th>Button</th>
                </tr>
            </thead>
            <tbody>
                {Persons.map(Person =>
                    <tr key={Person.id}>
                        <td>{Person.id}</td>
                        <td>{Person.price}</td>
                        <td>{Person.isCreated === true ? "True" : "False"}</td>
                        <td>{Person.isSent === true ? "True" : "False"}</td>
                        <td><button onClick={() => goToPersonDetailsPage(Person.id)} className="btn">See</button></td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Components</h1>
            <p>Getting Persons list</p>
            {contents}
            <button onClick={() => createPerson()} className="btn" >Add new Person</button>
        </div>
    );

    async function populatePersonsData() {
        const response = await fetch('http://localhost:5000/api/Persons');
        const data = await response.json();
        setPersons(data);
    }
}

export default Persons;