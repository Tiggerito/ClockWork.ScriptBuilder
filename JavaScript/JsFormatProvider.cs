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

namespace ClockWork.ScriptBuilder.JavaScript
{
	/// <summary>
	/// Formats types to conform with javascript on top of the normal ScriptFormatProvider features
	/// Use the singleton Instance to save resources
	/// 
	/// Bool = true|false
	/// DateTime = yyyy/MM/dd HH:mm:ss
	/// </summary>
	public class JsFormatProvider : ScriptFormatProvider
	{
		#region Singleton
		private static JsFormatProvider _Instance = null;
		/// <summary>
		/// A reusable instance of the format provider
		/// </summary>
		public new static JsFormatProvider Instance
		{
			get
			{
				if (_Instance == null)
					_Instance = new JsFormatProvider();

				return _Instance;
			}
		}
		#endregion

		#region ICustomFormatter Members
		/// <summary>
		/// Formats booleans and dates in a javascript compliant way
		/// 
		/// Bool = true|false
		/// DateTime = yyyy/MM/dd HH:mm:ss
		/// </summary>
		/// <param name="format"></param>
		/// <param name="arg"></param>
		/// <param name="formatProvider"></param>
		/// <returns></returns>
		public override string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (arg is bool)
				return arg.ToString().ToLower();

			// Using - in dates is not supported on all browsers so we use /
			if (arg is DateTime)
				return ((DateTime)arg).ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss");


			return base.Format(format, arg, formatProvider);
		}

		#endregion
	}
}
