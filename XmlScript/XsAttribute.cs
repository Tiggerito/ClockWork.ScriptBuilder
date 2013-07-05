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
using System.Xml;
using System.IO;

namespace ClockWork.ScriptBuilder.XmlScript
{
	/// <summary>
	/// Renders as an xml attribute
	/// </summary>
	public class XsAttribute : ScriptItem, IXmlRenderer
	{
		#region Constructors
		/// <summary>
		/// Create an xml attribute 
		/// </summary>
		/// <param name="name">name of the attribute</param>
		/// <param name="value">value of the attribute</param>
		public XsAttribute(string name, object value) 
		{ 
			Name = name;
			Value = value;
		}

		#endregion

		#region Data
		private string _Name;
		/// <summary>
		/// Name of the attribute
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		private object _Value;
		/// <summary>
		/// Value to put in the attribute
		/// </summary>
		public object Value
		{
			get { return _Value; }
			set { _Value = value; }
		}
		#endregion

		#region Rendering
		/// <summary>
		/// Renders as an attribute 
		/// name="value"
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

			IScriptWriter writer = e.Writer;


			if (this.Layout == ScriptLayout.Block && this.HasRenderContent)
				writer.WriteNewLineAndIndent();

            // as we have to process the contents we will have to render it seperately
            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw, e.Writer); // use same format provider

            sw.Write(this.Value);

            string text = tw.ToString();

			writer.Write(XmlConvert.EncodeName(Name) + "=\"");
            writer.Write(Encode(text)); // need to do some encoding
			writer.Write("\"");
		}
		#endregion

		#region Encoding
		/// <summary>
		/// Encodes value for an xml attribute
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string Encode(string s)
		{
			return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&apos;").Replace("\"", "&quote;").Replace(Environment.NewLine, "&#10;");
		}
		#endregion

		#region IXmlRenderer
		/// <summary>
		/// Adds the attribute to the supplied element
		/// </summary>
		/// <param name="parentElement"></param>
		public void Render(XmlElement parentElement)
		{
            // as we have to process the contents we will have to render it seperately
            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw); // use same format provider

            sw.Write(this.Value);

            string text = tw.ToString();

            parentElement.SetAttribute(this.Name, text);
		}
		#endregion
	}
}
