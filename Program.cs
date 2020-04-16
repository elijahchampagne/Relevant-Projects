using System;

namespace EmailUtility
{
    public class Program
    {
        static void Main(string[] args)
        {
            UserInput userInput = GetUserInput();
            bool sentEmail = SMTP.SendEmail(userInput.To, userInput.Subject, userInput.Body, userInput.CC);
            Console.WriteLine($"Email Sent: {sentEmail}");
        }

        public static UserInput GetUserInput()
        {
            UserInput userInput = new UserInput();
            Console.WriteLine("Welcome to SMTP Email Utility!");
            Console.WriteLine("Enter an email address");
            userInput.To = Console.ReadLine();
            // Console.WriteLine($"Email To: {to}");
            Console.WriteLine("Enter a subject");
            userInput.Subject = Console.ReadLine();
            // Console.WriteLine($"Email Subject: {subject}");
            Console.WriteLine("Enter a body");
            userInput.Body = Console.ReadLine();
            // Console.WriteLine($"Email Body: {body}");
            Console.WriteLine("Enter a cc");
            userInput.CC = Console.ReadLine();
            // Console.WriteLine($"Email CC: {cc}");
            return userInput;
        }


    }
}
