using System;
using System.Security.Cryptography;
using System.Text;

namespace RealesApi.Helpers.HashService
{
	public class HashService : IHashService
	{

		//Encrypting/Hashing passwords
		public string ConvertKeyToHash(string input)
		{
			try
			{
				byte[] data = SHA1.HashData(Encoding.Unicode.GetBytes(input));
				return Convert.ToHexString(data).ToLowerInvariant();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Ex=", ex.Message);
				throw;
			}
		}
	}
}

