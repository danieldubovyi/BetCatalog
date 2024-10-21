import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import './PersonDetails.css';

interface PersonDetails {
    id: number;
    price: number;
    isCreated: boolean
    isSent: boolean;
}

function PersonDetails() {
    const [PersonDetails, setPersonDetails] = useState<PersonDetails>();
    const { id } = useParams();

    useEffect(() => {
        populateComponentDetailsData(id);
    }, []);
    const navigate = useNavigate();

    const goToComponentsListPage = () => {
        navigate(`/persons/${id}/accounts`)
    }


    const contents = PersonDetails === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Type</th>
                    <th>Is Created</th>
                    <th>Is Sent</th>
                </tr>
            </thead>
            <tbody>
                <td>{PersonDetails.id}</td>
                <td>{PersonDetails.price}</td>
                <td>{PersonDetails.isCreated === true ? "True" : "False"}</td>
                <td>{PersonDetails.isSent === true ? "True" : "False"}</td>
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Person</h1>
            <p>Getting Person details</p>
            <p>ID ARG: {id}</p>
            {contents}
            <button onClick={() => goToComponentsListPage()} className="btn">See components</button>
        </div>
    );

    async function populateComponentDetailsData(id: string | undefined) {
        const response = await fetch(`http://localhost:5000/api/persons/${id}`);
        const data = await response.json();
        setPersonDetails(data);
    }

}
export default PersonDetails;