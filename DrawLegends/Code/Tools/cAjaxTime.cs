
using System;


namespace COR.AJAX
{


	public class Time
	{


		public static DateTime FromUnixTicks(Int64 lngMilliseconds)
		{
			DateTime d1 = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			DateTime d2 = d1.AddMilliseconds(lngMilliseconds);

			DateTime dt = d2.ToLocalTime();
			return dt;
		}
		// FromUnixTicks


		public static Int64 ToUnixTicks()
		{
			return ToUnixTicks(DateTime.Now);
		}
		// ToUnixTicks


		public static Int64 ToUnixTicks(string strPathToFile)
		{
			DateTime dLastWriteTime = System.IO.File.GetLastWriteTimeUtc(strPathToFile);
			return ToUnixTicks(dLastWriteTime);
		}
		// ToUnixTicks


		public static Int64 ToUnixTicksMapped(string strPathToFile)
		{
			return ToUnixTicksMapped(strPathToFile, false);
		}


		public static Int64 ToUnixTicksMapped(string strPathToFile, bool bNoChek)
		{
			strPathToFile = System.Web.HttpContext.Current.Server.MapPath(strPathToFile);

			if (bNoChek) {
				return ToUnixTicks(strPathToFile);
			}

			if (System.IO.File.Exists(strPathToFile)) {
				return ToUnixTicks(strPathToFile);
			} else {
				if (!(strPathToFile.EndsWith("\\DMS", StringComparison.OrdinalIgnoreCase) | strPathToFile.EndsWith("\\DMS\\bilder\\{0}", StringComparison.OrdinalIgnoreCase))) {
					throw new System.IO.FileNotFoundException("Die Datei \"" + strPathToFile + "\" existiert nicht");
				}

			}

			return 0;
		}
		// ToUnixTicksMapped


		public static Int64 ToUnixTicks(DateTime dt)
		{
			DateTime d1 = new DateTime(1970, 1, 1);
			DateTime d2 = dt.ToUniversalTime();
			TimeSpan ts = new TimeSpan(d2.Ticks - d1.Ticks);
			return System.Convert.ToInt64(ts.TotalMilliseconds);
		}
		// ToUnixTicks


	}
	// Time


}
// COR.AJAX
