using common;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.Collections.Generic;
using System.Linq;

namespace jwt.services {
	public class AuthService {
		//get a secret:
		//var hmac = new System.Security.Cryptography.HMACSHA256();
		//var key = Convert.ToBase64String(hmac.Key);
		private const string _secret = "RUBI+JLE1KDsvozNEoonTTPTo1KWVdWUcU21Znt5nyV/ZsxwLUCXocuFa3tPWPdnKYHY8J7Ffu6XeYhzRcKyiQ==";

		public string Login(string username, string password) {
			var payload = new Dictionary<string, object>  {
				{"UserId", username },
				{"Message", "Don't foul your own nest."}
			};
			var tokenCreationResult = this.makeToken(payload);
			if (tokenCreationResult.Failure) {
				return string.Empty;
			}
			return tokenCreationResult.Result;
		}

		public string ReadToken(string token) {
			var claimsResult = this.decode(token);
			if (claimsResult.Failure) {
				return string.Empty;
			}
			var claims = claimsResult.Result;
			var userId = claims.Where(a => a.Key == "UserId").First();
			var message = claims.Where(a => a.Key == "Message").First();
			return $"User did stuff as UserId: { userId.Value } with Message: { message.Value }";
		}

		private Envelope<Dictionary<string, object>> decode(string token) {
			try {
				var serializer = new JsonNetSerializer();
				var provider = new UtcDateTimeProvider();
				var validator = new JwtValidator(serializer, provider);
				var urlEncoder = new JwtBase64UrlEncoder();
				var decoder = new JwtDecoder(serializer, validator, urlEncoder);
				var payload = decoder.DecodeToObject<Dictionary<string, object>>(token, _secret, true);
				return Envelope<Dictionary<string, object>>.Ok(payload);
			} catch (TokenExpiredException) {
				return Envelope<Dictionary<string, object>>.Error("Token has expired.");
			} catch (SignatureVerificationException) {
				return Envelope<Dictionary<string, object>>.Error("Token has invalid signature.");
			}
		}

		private Envelope<string> makeToken(Dictionary<string, object> payload) {
			if (payload == null || !payload.Any()) {
				return Envelope<string>.Error("Token could not be created. Payload was empty.");
			}
			try {
				var algorithm = new HMACSHA256Algorithm();
				var serializer = new JsonNetSerializer();
				var urlEncoder = new JwtBase64UrlEncoder();
				var encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
				var token = encoder.Encode(payload, _secret);
				return Envelope<string>.Ok(token);
			} catch {
				return Envelope<string>.Error("Token could not be created.");
			}
		}
	}
}