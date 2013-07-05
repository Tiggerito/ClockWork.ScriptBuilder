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
	/// Represents an xml text node
	/// Ensures the content is correctly encoded when rendered
	/// </summary>
	public class XsText : ScriptItem
	{
		#region Constructors
		/// <summary>
		/// Create a text node containing the text
		/// </summary>
		/// <param name="text"></param>
		public XsText(object text)
		{
			Text = text;
		}
		#endregion

		#region Data
		private object _Text;
		/// <summary>
		/// Content of the text node
		/// </summary>
		public object Text
		{
			get { return _Text; }
			set { _Text = value; }
		}
		#endregion

		#region Rendering
		/// <summary>
		/// Renders the text content after encoding it
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

            sw.Write(this.Text);

            string text = tw.ToString();

            writer.Write(Encode(text));
		}
		#endregion

		#region Encoding
		/// <summary>
		/// Encodes a string of use in xml
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string Encode(string s)
		{
			return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;"); //.Replace("'", "&apos;").Replace("\"", "&quote;");
		}
		#endregion

		#region IXmlRenderer
		/// <summary>
		/// Adds a text node to the parent element which contains the supplied text
		/// </summary>
		/// <param name="parentElement"></param>
		public void Render(XmlElement parentElement)
		{
			XmlDocument doc = parentElement.OwnerDocument;

            // as we have to process the contents we will have to render it seperately
            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw); // use same format provider

            sw.Write(this.Text);

            string text = tw.ToString();


            XmlText textNode = doc.CreateTextNode(text);

			parentElement.AppendChild(textNode);


		}
		#endregion
	}
}
