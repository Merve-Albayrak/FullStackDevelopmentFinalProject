namespace Application.Common.Helpers;

public static class MessagesHelper
{
    public static class Email
    {
        public static class Confirmation
        {
            public static string Subject => "About Your Order";
          //  public static string ActivationMessage => "Thank you for signing up to UpStorage! In order to activate your account please click the activation button given below.";
            
            public static string Message(string message) =>
                $"Your order status : {message}";
            
            //public static string ButtonText => "Activate My Account";

            //public static string Buttonlink(string email, string emailToken) =>
            //    $"https://upstorage.app/account/account-activation?email={email}&token={emailToken}";
            
            ///*public static string Buttonlink(string email, string emailToken)
            //{
            //    return $"https://upstorage.app/account/account-activation?email={email}&token={emailToken}";
            //}*/
                
        }
    }
}