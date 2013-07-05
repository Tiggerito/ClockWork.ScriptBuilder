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
using System.IO;

namespace ClockWork.ScriptBuilder.XmlScript
{
	/// <summary>
	/// Provides a quick way to create XML Items
	/// This makes building xml more readable
	/// </summary>
	public class Xs
	{
		#region Element
		/// <summary>
		/// Create an XML Element
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="name">name of the element</param>
		/// <param name="properties">itmes within the element</param>
		/// <returns></returns>
		public static XsElement Element(ScriptLayout layout, string name, params object[] properties)
		{
			return new XsElement(layout, name, properties);
		}
		/// <summary>
		/// Create an XML Element
		/// </summary>
		/// <param name="name">name of the element</param>
		/// <param name="properties">itmes within the element</param>
		/// <returns></returns>
		public static XsElement Element(string name, params object[] properties)
		{
			return new XsElement(name, properties);
		}
		#endregion

		#region Attribute

		/// <summary>
		/// Create an XML Attribute
		/// </summary>
		/// <param name="name">name of the attribute</param>
		/// <param name="value">value for the attribute</param>
		/// <returns></returns>
		public static XsAttribute Attribute(string name, object value)
		{
			return new XsAttribute(name, value);
		}
		#endregion

		#region CData

		/// <summary>
		/// Wrap the data in a CData section
		/// </summary>
		/// <param name="data">object to be rendered inside a CDATA</param>
		/// <returns></returns>
		public static XsCData CData(object data)
		{
			return new XsCData(data);
		}
		#endregion

		#region Text

		/// <summary>
		/// Render content as text
		/// raw text is automatically turned into one of these so that encoding is handled
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static XsText Text(object text)
		{
			return new XsText(text);
		}
		#endregion

        #region Rendering
        /// <summary>
        /// Provide a string representation of the object
        /// using an xml format provider
        /// </summary>
        /// <param name="o">object to render</param>
        /// <returns></returns>
        public static string Render(object o)
        {
            return Render(o, 0);
        }
        /// <summary>
        /// Provide a string representation of the object
        /// using an xml format provider
        /// </summary>
        /// <param name="o">object to render</param>
        /// <param name="indentations">starting indentation level</param>
        /// <returns></returns>
        public static string Render(object o, int indentations)
        {
            if (o == null)
                return String.Empty;

            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw, XsFormatProvider.Instance);
            sw.CurrentIndentLevel = indentations;

            sw.Write(o);

            return tw.ToString();
        }
        #endregion
	}
}
