
import {Container} from "semantic-ui-react";
//import './App.css'
import {Route, Routes} from "react-router-dom";
import LoginPage from "./pages/LoginPage.tsx";
import Dashboard from "./pages/Dashboard.tsx";
import SocialLogin from "./pages/SocialLogin.tsx";


import OrderHistory from "./pages/OrderHistory.tsx";
import Order from "./pages/Order.tsx";
import Order1 from "./pages/Order1.tsx";

import LiveLog from "./pages/LiveLog.tsx";
//import ProtectedRoute from "./components/ProtectedRoute.tsx";


function App() {


  return (
    <>
        <Container className="App">
            <Routes>

                <Route path="/login" element={<LoginPage/>}/>
                <Route path="/social-login" element={<SocialLogin/>}/>
                <Route path="/dashboard" element={
                  //  <ProtectedRoute>
                        <Dashboard />
                //    </ProtectedRoute>
                }/>


                <Route path="/orderhistory" element={
                    //  <ProtectedRoute>
                    <OrderHistory />
                    //    </ProtectedRoute>
                }/>
                <Route path="/order" element={
                    //  <ProtectedRoute>
                    <Order />
                    //    </ProtectedRoute>
                }/>
                <Route path="/livelog" element={
                    //  <ProtectedRoute>
                    <LiveLog />
                    //    </ProtectedRoute>
                }/>
                <Route path="/order1" element={
                    //  <ProtectedRoute>
                    <Order1 />
                    //    </ProtectedRoute>
                }/>


            </Routes>
        </Container>
    </>
  )
}

export default App
