using System;
using System.Diagnostics;

namespace DoItYourself
{
	public static class Utility
	{
		/// <summary>
		/// 実行時間を計測します。（ミリ秒単位）
		/// </summary>
		/// <param name="action">計測する処理</param>
		/// <returns>実行時間（ミリ秒単位）</returns>
		public static long Time(Action action)
		{
			var watch = Stopwatch.StartNew();
			action();
			watch.Stop();
			return watch.ElapsedMilliseconds;
		}
	}
}
