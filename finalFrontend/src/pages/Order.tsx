

import "./styles.scss";
import React, { useState, useEffect } from 'react'
import OrderDropDown from "../components/DropDown/OrderDropDown.tsx";
import {Form, Grid, Input, Segment} from "semantic-ui-react";
import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import OrderDto from "../types/OrderTypes.tsx";
import {Container} from "@mui/material";
import UserNavbar from "./UserNavbar.tsx";

const BASE_SIGNALR_URL = import.meta.env.VITE_API_SIGNALR_URL;
function Order1() {

    const [orderHubConnection, setOrderHubConnection] = useState<HubConnection | undefined>(undefined);

    useEffect(() =>{

        const startConnection = async () => {
            const connection = new HubConnectionBuilder()
                .withUrl(`${BASE_SIGNALR_URL}Hubs/OrderHub`)
                .withAutomaticReconnect()
                .build();

            await connection.start();

            setOrderHubConnection(connection);
        }

        if(!orderHubConnection){
            startConnection();
        }

    },[])

    const [order, setOrder] = useState<OrderDto>(new OrderDto());

    const [scrapeCount, setScrapeCount] = useState<string>("");

    const [productType, setProductType] = useState<string>("");
    const [allOrCount, setAllOrCount] = useState<string>("");

    const [showDropDown, setShowDropDown] = useState<boolean>(false);
    const options = () => {
        return ["All", "On Sale", "Regular Prices "];
    };
    const options1 = () => {
        return ["All", "Enter the Number"];
    };

    /**
     * Toggle the drop down menu
     */


    const toggleDropDown = () => {
        setShowDropDown(!showDropDown);
    };

    /**
     * Hide the drop down menu if click occurs
     * outside of the drop-down element.
     *
     * @param event  The mouse event
     */
    const dismissHandler = (event: React.FocusEvent<HTMLButtonElement>): void => {
        if (event.currentTarget === event.target) {
            setShowDropDown(false);
        }
    };

    /**
     * Callback function to consume the
     * city name from the child component
     *
     * @param options  The selected city
     */
    const handleProductType = (option: string): void => {
        setProductType(option);
    };
    const handleAllOrCount = (option: string): void => {
        setProductType(option);
    };

    /*test için var*/
    const handleSubmit = async() => {
        console.log(productType);

        order.Count = scrapeCount.toString();
        order.ProductType = productType;

        await orderHubConnection?.invoke<OrderDto>("SendLogNotification", order);
    };

    const handleScrapeCount = (value:string) => {
        setScrapeCount(String(value));
    };

    return (
        <>
            <Container>
                <UserNavbar />
            </Container>


            <Container>
                <Segment raised>

                    <Grid >
                        <Grid.Column>
                            <Form>
                                <Form.Field>
                                    <label>How many items to scrape?</label>
                                    <Input
                                        id="scrapeCountSelector"
                                        value={scrapeCount}
                                        onChange={(_, data) => handleScrapeCount(data.value)}
                                    />
                                </Form.Field>
                                <Form.Field>
                                    <label>What products do you want to scraping?</label>
                                    <button
                                        className={showDropDown ? "active" : undefined}
                                        onClick={(): void => toggleDropDown()}
                                        onBlur={(e: React.FocusEvent<HTMLButtonElement>): void =>
                                            dismissHandler(e)
                                        }
                                    >

                                        <div>{productType ? "Select: " + productType : "Select ..."} </div>
                                        {showDropDown && (
                                            <OrderDropDown
                                                options={options()}
                                                showDropDown={false}
                                                toggleDropDown={(): void => toggleDropDown()}
                                                optionSelection={handleProductType}
                                            />
                                        )}

                                    </button>
                                </Form.Field>

                                <br />
                                <button onClick={handleSubmit}>Gönder</button>
                            </Form>
                        </Grid.Column>
                    </Grid>
                </Segment>
            </Container>

        </>
    );
}

export default Order1
