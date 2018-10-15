namespace common {

	public class Envelope<T> : FunctionResult {
		public T Result { get; set; }
		
		public static Envelope<T> Ok(T output, string info = "") {
			return new Envelope<T> {
				Sucess = true,
				Info = info,
				Result = output
			};
		}

		public new static Envelope<T> Error(string info) {
			return new Envelope<T> {
				Sucess = false,
				Info = info
			};
		}
	}
}