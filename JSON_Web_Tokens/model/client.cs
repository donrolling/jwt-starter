using System;

namespace jwt.services.model {
	public class client {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Secret { get; set; }
	}
}