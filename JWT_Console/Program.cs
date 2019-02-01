using jwt.services;
using System; 

namespace JWT_Console {
    public class Program {
        static void Main(string[] args) {
            var username = "test";
            var password = "test";
            var authService = new AuthService();
            var token = authService.Login(username, password);
            Console.WriteLine(token); 
            var stuff = authService.ReadToken(token);
            Console.WriteLine(stuff);
			Console.ReadLine();
        }
    }
}
