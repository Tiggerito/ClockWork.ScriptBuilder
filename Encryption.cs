/*
 * Copyright (c) 2008, Anthony James McCreath
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     1 Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     2 Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     3 Neither the name of the project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY Anthony James McCreath "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL Anthony James McCreath BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace ClockWork.ScriptBuilder
{
	/// <summary>
	/// Provides utilities to help encrypt/decrypt data
	/// </summary>
	public sealed class Encryption
	{
		#region SecureString
		/// <summary>
		/// Utility to read a secure string which is encrypted in memory
		/// For best security, only read a secure string when necesary, i.e. writing
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string ReadSecureString(SecureString text)
		{
			IntPtr bstr = Marshal.SecureStringToBSTR(text);
			string s = Marshal.PtrToStringBSTR(bstr);

			Marshal.ZeroFreeBSTR(bstr);

			return s;
		}

		/// <summary>
		/// Encryts a string in memory
		/// For best security, convert a string to a secure string as soon as possible
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static SecureString CreateSecureString(string text)
		{
			SecureString ss = new SecureString();

			foreach (char c in text.ToCharArray())
			{
				ss.AppendChar(c);
			}

			ss.MakeReadOnly();

			return ss;
		}
		#endregion
	}
}
