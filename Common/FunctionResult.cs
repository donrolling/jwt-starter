namespace common {

	public class FunctionResult {
		private bool _success = true;

		public bool Sucess {
			get {
				return _success;
			}
			set {
				_success = value;
			}
		}

		public bool Failure {
			get {
				return !_success;
			}
			set {
				_success = !value;
			}
		}

		public string Info { get; set; }

		public static FunctionResult Ok(string info = "") {
			return new FunctionResult {
				Sucess = true,
				Info = info
			};
		}

		public static FunctionResult Error(string info) {
			return new FunctionResult {
				Sucess = false,
				Info = info
			};
		}
	}
}