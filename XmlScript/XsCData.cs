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
	/// Wrap the data in a CData section
	/// </summary>
	public class XsCData : ScriptItem, IXmlRenderer
	{
		#region Constructors
		/// <summary>
		/// A CData section with the supplied data as content
		/// </summary>
		/// <param name="data"></param>
		public XsCData(object data) 
		{
			Data = data;
		}
		#endregion

		#region Data
		private object _Data;
		/// <summary>
		/// The content of the cdata section
		/// </summary>
		public object Data
		{
			get { return _Data; }
			set { _Data = value; }
		}
		#endregion

		#region Rendering
		/// <summary>
		/// Renders the cdata section
		/// Special handing is done to cope if the data contains the illegal closing cdata character sequence (which I can't use here!)
		/// </summary>
		/// <param name="e">includes the script writer to render content to</param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

			IScriptWriter writer = e.Writer;

			if (this.Layout == ScriptLayout.Block && this.HasRenderContent)
				writer.WriteNewLineAndIndent();

            // as we have to process the contents we will have to render it seperately
            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw, e.Writer); // use same format provider

            sw.Write(this.Data);

            string text = tw.ToString();


            text = text.Replace("]]>", "]]>]]&gt;<![CDATA["); // resolve embedded ]]> by changing to two cdatas with a text node

			writer.Write("<![CDATA[");
            writer.Write(text);
			writer.Write("]]>");
		}
		#endregion

		#region IXmlRenderer
		/// <summary>
		/// Add a CData section to the supplied element
		/// Special handing is done to cope if the data contains the illegal closing cdata character sequence
		/// </summary>
		/// <param name="parentElement"></param>
		public void Render(XmlElement parentElement)
		{
			XmlDocument doc = parentElement.OwnerDocument;

            // as we have to process the contents we will have to render it seperately
            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw); // use same format provider

            sw.Write(this.Data);

            string text = tw.ToString();


            while (text.Contains("]]>"))
			{
                string subData = text.Substring(0, text.IndexOf("]]>"));
                text = text.Substring(text.IndexOf("]]>") + 3);

				XmlCDataSection subCdata = doc.CreateCDataSection(subData);
				parentElement.AppendChild(subCdata);

				XmlText textNode = doc.CreateTextNode("]]>");
				parentElement.AppendChild(textNode);
			}

            if (!String.IsNullOrEmpty(text))
			{
                XmlCDataSection cdata = doc.CreateCDataSection(text);

				parentElement.AppendChild(cdata);
			}


		}
		#endregion
	}
}
