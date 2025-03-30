using System;
namespace RealesApi.Helpers.HashService
{
	public interface IHashService
	{
        string ConvertKeyToHash(string input);
    }
}