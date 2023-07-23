import "./LiveLog.css";
import React, { useState, useEffect } from "react";
import { Container } from "@mui/material";
import UserNavbar from "./UserNavbar.tsx";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
 // LogDto'nuzu uygun şekilde import edin.

const FakeMenu = () => (
    <div className="fakeMenu">
        <div className="fakeButtons fakeClose"></div>
        <div className="fakeButtons fakeMinimize"></div>
        <div className="fakeButtons fakeZoom"></div>
    </div>
);

const FakeScreen = ({ logs }: { logs: LogDto[] }) => (
    <div className="fakeScreen">
        {logs.map((log) => (
            <p className="line1" key={log.id}>
                {log.message}
                <span className="cursor1"> <br />{log.sentOn}</span>
            </p>
        ))}
    </div>
);

const LiveLog: React.FC = () => {
    const [logs, setLogs] = useState<LogDto[]>([]);
    const [hubConnection, setHubConnection] = useState<HubConnection | null>(null);

    useEffect(() => {
        const url = "https://localhost:7239/Hubs/OrderHub";
        const connection = new HubConnectionBuilder()
            .withUrl(url)
            .withAutomaticReconnect()
            .build();

        connection.on("NewLog", (logDto: LogDto) => {
            setLogs((prevLogs) => [...prevLogs, logDto]);
        });

        async function startConnection() {
            try {
                await connection.start();
                setHubConnection(connection);
            } catch (err) {
                console.error("Error while establishing connection: ", err);
            }
        }

        startConnection();

        return () => {
            if (hubConnection) {
                hubConnection.off("NewLog");
                hubConnection.stop();
            }
        };
    }, []);

    return (
        <>
            <Container>
                <UserNavbar />
            </Container>

            <Container>
                <FakeMenu />
                <FakeScreen logs={logs} />
            </Container>
        </>
    );
};

export default LiveLog;
