
import { Button, Grid, Header, Segment } from 'semantic-ui-react';

// eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
const BASE_URL = import.meta.env.VITE_API_URL;
function LoginPage () {



    const onGoogleLoginClick = (e:React.FormEvent) => {
        e.preventDefault();

        // eslint-disable-next-line @typescript-eslint/restrict-template-expressions
        window.location.href = `${BASE_URL}/Authentication/GoogleSignInStart`;
    };

    return (
        <div className="login-form">
            <Grid textAlign="center" style={{ height: '100vh' }} verticalAlign="middle">
                <Grid.Column style={{ maxWidth: 450 }}>
                    <Header as="h2" color="teal" textAlign="center">
                        Log-in to your account
                    </Header>
                    <Segment stacked>
                        <Button color="google plus" onClick={onGoogleLoginClick} fluid size="large" >
                            Login with Google
                        </Button>
                    </Segment>
                </Grid.Column>
            </Grid>
        </div>
    );
}

export default LoginPage;