namespace TestEditAndContinue
{
	class ENC0280
	{
		internal string localString;
		internal int ManagedThreadId;

		public override string ToString()
		{
			return new { localString, ManagedThreadId }.ToString();
		}
	}
}