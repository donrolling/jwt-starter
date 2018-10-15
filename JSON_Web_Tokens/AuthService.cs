using System;
using System.Collections.Generic;
using System.Security.Claims;
using JWT.Builder;
using System.Security.Cryptography;
using JWT.Algorithms;
using JWT.Serializers;
using JWT;
using System.Linq;

namespace JSON_Web_Tokens {
    public class AuthService {
        //get a secret:
        //var hmac = new System.Security.Cryptography.HMACSHA256();
        //var key = Convert.ToBase64String(hmac.Key);
        private const string _secret = "RUBI+JLE1KDsvozNEoonTTPTo1KWVdWUcU21Znt5nyV/ZsxwLUCXocuFa3tPWPdnKYHY8J7Ffu6XeYhzRcKyiQ=="; 

        public string Login(string username, string password) {
            var userId = 123456789; 

            var payload = new Dictionary<string, object>  {
                {"UserId", userId },  
                {"Message", "Don't foul your own nest."}
            }; 
            
            var token = this.makeToken(payload);
            return token; 
        }

         public string ReadToken(string token) {
            var claims = this.decode(token);

            var userId = claims.Where(a => a.Key == "UserId").First(); 
            var message = claims.Where(a => a.Key == "Message").First(); 
            
            return $"User did stuff as UserId: { userId.Value } with Message: { message.Value }"; 
        }

        private Dictionary<string, object> decode(string token)
		{
            var serializer = new JsonNetSerializer();
            var provider = new UtcDateTimeProvider();
            var validator = new JwtValidator(serializer, provider);
            var urlEncoder = new JwtBase64UrlEncoder();
            var decoder = new JwtDecoder(serializer, validator, urlEncoder);
            //var json = decoder.Decode(token, _secret, verify: true);
            var payload = decoder.DecodeToObject<Dictionary<string, object>>(token, _secret, true);
            return payload;
		}


		private string makeToken(Dictionary<string, object> payload)
		{
			var algorithm = new HMACSHA256Algorithm();
			var serializer = new JsonNetSerializer();
			var urlEncoder = new JwtBase64UrlEncoder();
			var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
			var token = encoder.Encode(payload, _secret);
			return token;
		}        
    }
}
