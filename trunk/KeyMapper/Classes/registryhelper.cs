using System;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace KeyMapper
{

	#region Public methods

	public static class RegistryHelper
	{

		const int KEY_QUERY_VALUE = 0x0001;
		const int KEY_SET_VALUE = 0x0002;

		public static DateTime GetRegistryKeyTimestamp(RegistryHive hive, string keyName)
		{
			Int64 ts = GetRawRegistryKeyTimestamp(hive, keyName);
			DateTime dt;
			if (ts != 0)
				dt = DateTime.FromFileTimeUtc(ts);
			else
				dt = DateTime.MinValue;

			return dt.ToLocalTime();
		}

		private static Int64 GetRawRegistryKeyTimestamp(RegistryHive hive, string keyname)
		{

			if (String.IsNullOrEmpty(keyname))
				return (Int64)0; // Otherwise the function opens HKLM (or HKCU) again.

			UIntPtr hkey = OpenKey(hive, keyname, KEY_QUERY_VALUE);

			if (hkey == UIntPtr.Zero)
				return (Int64)0; // Didn't open key

			Int64 timestamp;

			uint result2 = NativeMethods.RegQueryInfoKey(
				hkey, IntPtr.Zero,
				IntPtr.Zero, IntPtr.Zero,
				IntPtr.Zero, IntPtr.Zero,
				IntPtr.Zero, IntPtr.Zero,
				IntPtr.Zero, IntPtr.Zero,
				IntPtr.Zero, out timestamp);

			if (result2 != 0)
				timestamp = (Int64)0; // Failed, don't return whatever value was supplied.

			NativeMethods.RegCloseKey(hkey);

			return timestamp;
		}

		public static bool CanUserWriteToKey(RegistryHive hive, string keyName)
		{
			UIntPtr hkey = OpenKey(hive, keyName, KEY_SET_VALUE);
			if (hkey == UIntPtr.Zero)
				return false;
			else
			{
				NativeMethods.RegCloseKey(hkey);
				return true;
			}

		}

	#endregion


		#region Private methods

		private static UIntPtr OpenKey(RegistryHive hive, string keyname, int requiredAccess)
		{

			UIntPtr hiveptr;
			UIntPtr hkey;

			switch (hive)
			{
				case RegistryHive.ClassesRoot:
					hiveptr = (UIntPtr)0x80000000;
					break;
				case RegistryHive.CurrentUser:
					hiveptr = (UIntPtr)0x80000001;
					break;
				case RegistryHive.LocalMachine:
					hiveptr = (UIntPtr)0x80000002;
					break;
				case RegistryHive.Users:
					hiveptr = (UIntPtr)0x80000003;
					break;
				case RegistryHive.CurrentConfig:
					hiveptr = (UIntPtr)0x80000005;
					break;
				default:
					return UIntPtr.Zero;
			}

			int result = NativeMethods.RegOpenKeyEx(hiveptr, keyname, 0, requiredAccess, out hkey);

			if (result == 0)
				return hkey;
			else
				return UIntPtr.Zero;
		}

		#endregion


		internal class NativeMethods
		{
			// Doesn't need to be static
			private NativeMethods() { }

			[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
			public static extern int RegOpenKeyEx
				(UIntPtr hKey,
				string lpSubKey,
				uint ulOptions,
				int samDesired,
				out UIntPtr phkResult);

			[DllImport("advapi32.dll")]
			public extern static uint RegQueryInfoKey
				(UIntPtr hkey,
				IntPtr lpClass,
				IntPtr lpcbClass,
				IntPtr lpReserved,
				IntPtr lpcSubKeys,
				IntPtr lpcbMaxSubKeyLen,
				IntPtr lpcbMaxClassLen,
				IntPtr lpcValues,
				IntPtr lpcbMaxValueNameLen,
				IntPtr lpcbMaxValueLen,
				IntPtr lpcbSecurityDescriptor,
				out Int64 lpftLastWriteTime);

			[DllImport("Advapi32.dll")]
			public static extern uint RegCloseKey(UIntPtr hKey);
		}

	}

	//class GRKT_Test
	//{
	//    // Get timestamps from keys in all the hives.
	//    // Ask for keys that don't exist.

	//    public void test()
	//    {
	//        Int64 ts = GetRegKeyTimestamp.GetRegistryKeyTimestamp(RegistryHive.ClassesRoot, "*");
	//        Console.WriteLine("HKR: {0}", ts);

	//        ts = GetRegKeyTimestamp.GetRegistryKeyTimestamp(RegistryHive.CurrentUser, "Console");
	//        Console.WriteLine("HKCU: {0}", ts);

	//        ts = GetRegKeyTimestamp.GetRegistryKeyTimestamp(RegistryHive.LocalMachine, "SAM");
	//        Console.WriteLine("HKLM: {0}", ts);

	//        ts = GetRegKeyTimestamp.GetRegistryKeyTimestamp(RegistryHive.Users, ".DEFAULT");
	//        Console.WriteLine("HKU: {0}", ts);

	//        ts = GetRegKeyTimestamp.GetRegistryKeyTimestamp(RegistryHive.CurrentConfig, "Software");
	//        Console.WriteLine("HKCC: {0}", ts);

	//        ts = GetRegKeyTimestamp.GetRegistryKeyTimestamp(RegistryHive.DynData, "foo");
	//        Console.WriteLine("HKDD: {0}", ts);

	//        ts = GetRegKeyTimestamp.GetRegistryKeyTimestamp(RegistryHive.CurrentUser, "Noway_key_exists");
	//        Console.WriteLine("Bad key: {0}", ts);

	//        ts = GetRegKeyTimestamp.GetRegistryKeyTimestamp(RegistryHive.CurrentUser, null);
	//        Console.WriteLine("Null key: {0}", ts);


	//        // DateTime

	//        DateTime dt = GetRegKeyTimestamp.GetRegistryKeyTimestampDateTime(RegistryHive.ClassesRoot, "*");
	//        Console.WriteLine("HKR: {0}", dt);

	//        dt = GetRegKeyTimestamp.GetRegistryKeyTimestampDateTime(RegistryHive.CurrentUser, "Console");
	//        Console.WriteLine("HKCU: {0}", dt);

	//        dt = GetRegKeyTimestamp.GetRegistryKeyTimestampDateTime(RegistryHive.LocalMachine, "SAM");
	//        Console.WriteLine("HKLM: {0}", dt);

	//        dt = GetRegKeyTimestamp.GetRegistryKeyTimestampDateTime(RegistryHive.Users, ".DEFAULT");
	//        Console.WriteLine("HKU: {0}", dt);

	//        dt = GetRegKeyTimestamp.GetRegistryKeyTimestampDateTime(RegistryHive.CurrentConfig, "Software");
	//        Console.WriteLine("HKCC: {0}", dt);

	//        dt = GetRegKeyTimestamp.GetRegistryKeyTimestampDateTime(RegistryHive.DynData, "foo");
	//        Console.WriteLine("HKDD: {0}", dt);

	//        dt = GetRegKeyTimestamp.GetRegistryKeyTimestampDateTime(RegistryHive.CurrentUser, "Noway_key_exists");
	//        Console.WriteLine("Bad key: {0}", dt);

	//        dt = GetRegKeyTimestamp.GetRegistryKeyTimestampDateTime(RegistryHive.CurrentUser, null);
	//        Console.WriteLine("Null key: {0}", dt);

	//        // Time since last logon:

	//        ts = GetRegKeyTimestamp.GetRegistryKeyTimestamp(RegistryHive.CurrentUser, "Volatile Environment");

	//        Int64 now = DateTime.UtcNow.ToFileTimeUtc();

	//        Console.WriteLine("Logontime: {0}, Now: {1}, Difference {2}, Time since last logon {3}", ts, now, now - ts, TimeSpan.FromTicks(now - ts));

	//        dt = GetRegKeyTimestamp.GetRegistryKeyTimestampDateTime(RegistryHive.CurrentUser, "Volatile Environment");

	//        Console.WriteLine("Logontime: {0}, Now: {1}, Time since last logon {2}", dt, DateTime.UtcNow, DateTime.UtcNow - dt);

	//    }




	//}
}
