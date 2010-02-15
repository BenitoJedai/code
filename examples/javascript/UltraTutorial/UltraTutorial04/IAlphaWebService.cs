using System;
namespace UltraTutorial04
{
	interface IAlphaWebService
	{
		void DownloadData(string url, DownloadDataResult result);
		void GetTime(string prefix, GetTimeResult result);
	}
}
