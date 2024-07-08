namespace testAssistant.exception
{
    public class Exception
    {
        public delegate void sendMessage(string msg);

        public static event sendMessage messageTrigger;
            
        public static void error(string message) {
            messageTrigger(message);
            
        }
    }

    public class ExceptionHandler
    {
        
    }
}