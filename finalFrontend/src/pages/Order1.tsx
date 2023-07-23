

import {Form, Grid, Header, Input, Segment} from "semantic-ui-react";
import "./styles.scss";
import {Container} from "@mui/material";
import UserNavbar from "./UserNavbar.tsx";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import React, {useEffect, useState} from "react";
import {LocalJwt} from "../types/AuthTypes.ts";
import OrderDto from "../types/OrderTypes.tsx";





const BASE_SIGNALR_URL = import.meta.env.VITE_API_SIGNALR_URL;


const Field = ({ label, id, ...rest }) => (
    <div>
        <label htmlFor={id}>{label}</label>
        <input id={id} {...rest} />
    </div>
);

const Select = ({ label, id, children, ...rest }) => (
    <div>
        <label htmlFor={id}>{label}</label>
        <select id={id} {...rest}>
            {children}
        </select>
    </div>
);



function Order() {

    const [orderHubConnection,setOrderHubConnection] = useState<HubConnection | undefined>(undefined);



    useEffect(() => {

        const startConnection = async () => {

            const jwtJson = localStorage.getItem("upstorage_user");
            if(jwtJson){
                const localJwt:LocalJwt =JSON.parse(jwtJson);

                const connection = new HubConnectionBuilder()
                    .withUrl(`${BASE_SIGNALR_URL}Hubs/OrderHub`)
                    .withAutomaticReconnect()
                    .build();

                await connection.start();

                setOrderHubConnection(connection);
            }



        }

        if(!orderHubConnection){
            startConnection();
        }


    },[])

    const toggleDropDown = () => {
        setShowDropDown(!showDropDown);
    };

    //const [order, setOrder] = useState<OrderDto>(new OrderDto());
    const [scrapeCount, setScrapeCount] = useState<string>("");

    const [productType, setProductType] = useState<string>("");
    const [allOrCount, setAllOrCount] = useState<string>("");
    const handleSubmit = async  () => {

        const accountId = await orderHubConnection?.invoke<string>("SendLogNotification",order);

        console.log(accountId)


        /*   const response = await api.post<ApiResponse<string>>("/Accounts", account);
           if(response.data) {
               console.log(`Account with ID: ${response.data.data} added successfully.`);
               // You can redirect to accounts page or show success message here.
           }*/
    }
    const [order, setOrder] = useState<OrderDto>(new OrderDto());



    const [showDropDown, setShowDropDown] = useState<boolean>(false);
    const options = () => {
        return ["All", "OnDiscount", "NonDiscount"];
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setOrder({
            ...order,
            [e.target.name]: e.target.value
        });
    }
    // const { form } = useForm({
    //     defaultValues: { firstName: "", lastName: "", framework: "", message: "" },
    //   onSubmit: (values) => alert(JSON.stringify(values, undefined, 2))
    //   });

    return (
        <>
            <Container>
                <Container>
                    <UserNavbar />
                </Container>

                <Container>

                    <form onSubmit={handleSubmit} >
                        <Select label="ProductType" id="ProductType" name="ProductType" onChange={handleChange}>

                            <option value="All">All</option>
                            <option value="On Sale">On Sale</option>
                            <option value="Regular Prices">Regular Prices</option>

                        </Select>
                        <Select label="AllOrCount" id="AllOrCount" name="AllOrCount" onChange={handleChange}>

                            <option value="All">All</option>
                            <option value="Enter the Number">Enter the Number</option>

                        </Select>
                        <Field label="Enter the Number" id="ExpectedCount" name="ExpectedCount" onChange={handleChange} />

                        <input type="submit" />
                    </form>
                </Container>
            </Container>



        </>

    );
}

export default Order;
