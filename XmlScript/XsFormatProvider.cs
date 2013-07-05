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
using System.Xml;

namespace ClockWork.ScriptBuilder.XmlScript
{
	/// <summary>
	/// Adds formatting using the XmlConvert class
	/// </summary>
	public class XsFormatProvider : ScriptFormatProvider
	{
		#region Singleton
		private static XsFormatProvider _Instance = null;
		/// <summary>
		/// A reusable instance of the format provider
		/// </summary>
		public new static XsFormatProvider Instance
		{
			get
			{
				if (_Instance == null)
					_Instance = new XsFormatProvider();

				return _Instance;
			}
		}
		#endregion

		#region Formatting

		private XmlDateTimeSerializationMode _DateTimeMode = XmlDateTimeSerializationMode.RoundtripKind;
		/// <summary>
		/// How DateTimes will get renderred
		/// </summary>
		public XmlDateTimeSerializationMode DateTimeMode
		{
			get { return _DateTimeMode; }
			set { _DateTimeMode = value; }
		}

		#endregion

		#region ICustomFormatter Members

		/// <summary>
		/// Use XmlConvert where possible
		/// </summary>
		/// <param name="format"></param>
		/// <param name="arg"></param>
		/// <param name="formatProvider"></param>
		/// <returns></returns>
		public override string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (arg is DateTime)
				return XmlConvert.ToString((DateTime)arg, DateTimeMode);

			if (arg is Boolean)
				return XmlConvert.ToString((Boolean)arg);

			if (arg is TimeSpan)
				return XmlConvert.ToString((TimeSpan)arg);

			if (arg is Guid)
				return XmlConvert.ToString((Guid)arg);

			if (arg is Int16)
				return XmlConvert.ToString((Int16)arg);

			if (arg is Int32)
				return XmlConvert.ToString((Int32)arg);

			if (arg is Int64)
				return XmlConvert.ToString((Int64)arg);

			if (arg is Decimal)
				return XmlConvert.ToString((Decimal)arg);

			if (arg is Double)
				return XmlConvert.ToString((Double)arg);

			return base.Format(format, arg, formatProvider);
		}

		#endregion
	}
}

