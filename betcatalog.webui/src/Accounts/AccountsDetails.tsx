import { useEffect, useState } from 'react';
import { useParams } from "react-router-dom";
import './AccountsDetails.css';

interface AccountsDetails {
    id: number;
    type: string;
    modelName: string;
    link: string;
    price: number;
    deliverAndCommision: number;
    priceUsd: number;
    currencyUsd: number;
    isOrdered: boolean;
    isDelivered: boolean;
    droneId: number;
}

function AccountsDetails() {
    const [AccountsDetails, setAccountDetails] = useState<AccountsDetails>();
    const { id } = useParams();

    useEffect(() => {
        populateAccountDetailsData(id);
    }, []);
    

    const contents = AccountsDetails === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Type</th>
                    <th>Model Name</th>
                    <th>Link</th>
                    <th>Price</th>
                    <th>Delivery And Commision</th>
                    <th>Price Usd</th>
                    <th>Currency Usd</th>
                    <th>Is Ordered</th>
                    <th>Is Delivered</th>
                    <th>Drone ID</th>
                </tr>
            </thead>
            <tbody>
                    <tr key={AccountsDetails.id}>
                    <td>{AccountsDetails.id}</td>
                    <td>{AccountsDetails.type}</td>
                    <td>{AccountsDetails.modelName}</td>
                    <td>{AccountsDetails.link}</td>
                    <td>{AccountsDetails.price}</td>
                    <td>{AccountsDetails.deliverAndCommision}</td>
                    <td>{AccountsDetails.priceUsd}</td>
                    <td>{AccountsDetails.currencyUsd}</td>
                    <td>{AccountsDetails.isOrdered === true ? "True" : "False"}</td>
                    <td>{AccountsDetails.isDelivered === true ? "True" : "False"}</td>
                    <td>{AccountsDetails.droneId}</td>
                </tr>
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Account</h1>
            <p>Getting Account details</p>
            <p>ID ARG: {id}</p>
            {contents}
        </div>
    );

    async function populateAccountDetailsData(id: string | undefined) {
        const response = await fetch(`http://localhost:5000/api/accounts/${id}`);
        const data = await response.json();
        setAccountDetails(data);
    }

}
export default AccountsDetails;