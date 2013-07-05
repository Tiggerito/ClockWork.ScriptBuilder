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
	/// Represents an xml element.
	/// can contain any item. 
	/// XsAttribute items will be rendered as attributes
	/// xml specific items implement IXmlRenderer which lets you render directly to an XmlDocument or XmlElement
	/// other objects are wrapped into a XsText item that encodes their rendered content as a text node
	/// </summary>
	public class XsElement : ScriptSet, IXmlRenderer
	{
		#region Constructors
		/// <summary>
		/// creates an empty element
		/// </summary>
		/// <param name="name"></param>
		public XsElement(string name) : base() { Name = name; }
		/// <summary>
		/// creates an empty element
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		public XsElement(ScriptLayout layout, string name) : base(layout) { Name = name; }
		/// <summary>
		/// creates an element containing items
		/// </summary>
		/// <param name="name"></param>
		/// <param name="items"></param>
		public XsElement(string name, IEnumerable<object> items) : base(items) { Name = name; }
		/// <summary>
		/// creates an element containing items
		/// </summary>
		/// <param name="name"></param>
		/// <param name="items"></param>
		public XsElement(string name, params object[] items) : base(items) { Name = name; }
		/// <summary>
		/// creates an element containing items
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="items"></param>
		public XsElement(ScriptLayout layout, string name, IEnumerable<object> items) : base(layout, items) { Name = name; }
		/// <summary>
		/// creates an element containing items
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="items"></param>
		public XsElement(ScriptLayout layout, string name, params object[] items) : base(layout, items) { Name = name; }
		#endregion

		#region Data
		private string _Name;
		/// <summary>
		/// element name
		/// can include namespace (I think!)
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		#endregion

		#region Layout
		/// <summary>
		/// Defines the default layout that this item with use
		/// </summary>
		public override ScriptLayout DefaultLayout
		{
			get { return ScriptLayout.Inline; }
		}

		#endregion

		#region Rendering
		/// <summary>
		/// Render the xml element
		/// detects XsAttributes form within its collection and renders appropriately
		/// non IXmlRenderer objects are wrapped within XsText rendering i.e. encoded 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
		{

			IScriptWriter writer = e.Writer;


			// this uses EncodeName which means the name can include the namespace
			// Iif we implement namespace as a seperate entitiy we should use EncodeLocalName

			List<XsAttribute> attributes = new List<XsAttribute>();
			bool hasInnerNodes = false;
			bool hasElements = false;

			foreach (object o in this)
			{
				if (o is XsAttribute)
				{
					attributes.Add((XsAttribute)o);
				}
				else
					hasInnerNodes = true;

				if (o is XsElement)
					hasElements = true;
			}

			if (hasElements)
				this.TrySetLayout(ScriptLayout.Block);

			// do the tag and attributes
			if (this.Layout == ScriptLayout.Block && this.HasRenderContent)
				writer.WriteNewLineAndIndent();

			writer.Write("<" + XmlConvert.EncodeName(Name));

			foreach (XsAttribute a in attributes)
			{
				writer.Write(" ");
				writer.Write(a);
			}

			if (hasInnerNodes)
			{
				writer.Write(">");

				if (this.Layout == ScriptLayout.InlineBlock)
					writer.WriteNewLineAndIndent();

				writer.BeginIndent();

				base.OnRender(e); // does inner items

				writer.EndIndent();

				if (this.Layout == ScriptLayout.InlineBlock || this.Layout == ScriptLayout.Block)
					writer.WriteNewLineAndIndent();

				// do the end tag
				writer.Write("</" + XmlConvert.EncodeName(Name) + ">");
			}
			else
			{
				writer.Write("/>");
			}
		}
		/// <summary>
		/// remove attributes from render list
		/// </summary>
		/// <param name="dest"></param>
		/// <param name="o"></param>
		protected override void AddToRenderList(List<object> dest, object o)
		{
			if (o is XsAttribute)
			{
				// skip
			}
			else if (o is IXmlRenderer)
			{
				base.AddToRenderList(dest,o);
			}
			else
				base.AddToRenderList(dest, new XsText(o)); // wrap unknowns as text nodes

		}
		#endregion

		#region IXmlRenderer
		/// <summary>
		/// add as the root XmlElement
		/// </summary>
		/// <param name="doc"></param>
		public void Render(XmlDocument doc)
		{
			doc.AppendChild(CreateXmlElement(doc));
		}
		/// <summary>
		/// add as an XmlElement to the children of parentElement
		/// </summary>
		/// <param name="parentElement"></param>
		public void Render(XmlElement parentElement)
		{
			parentElement.AppendChild(CreateXmlElement(parentElement.OwnerDocument));
		}

		/// <summary>
		/// Creates an XmlElement based on this
		/// Adds all children
		/// </summary>
		/// <param name="doc"></param>
		/// <returns></returns>
		protected XmlElement CreateXmlElement(XmlDocument doc)
		{


			XmlElement element = doc.CreateElement(this.Name);


			foreach (object o in this)
			{
				if (o is IXmlRenderer)
				{
					((IXmlRenderer)o).Render(element);
				}
				else
				{
					Xs.Text(o).Render(element);
				}
			}

			return element;
		}
		#endregion
	}
}
