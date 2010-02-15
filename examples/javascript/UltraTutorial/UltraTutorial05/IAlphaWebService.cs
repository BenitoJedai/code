using System;
namespace UltraTutorial05
{
	public interface IAlphaWebService
	{
		void DownloadData(string url, DownloadDataResult result);
		void GetTime(string prefix, GetTimeResult result);
	}
}
