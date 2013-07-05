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

namespace ClockWork.ScriptBuilder
{
	/// <summary>
	/// An item that lets you wrap objects before and/or after another item
	/// Useful to create effects like surrounding brackets
	/// </summary>
	public class ScriptWrapper : ScriptItem
	{
		#region Constructors
		/// <summary>
		/// Create an empty wrapper
		/// </summary>
		public ScriptWrapper()
		{
		}

		/// <summary>
		/// Create an empty wrapper with a custom layout
		/// </summary>
		/// <param name="layout"></param>
		public ScriptWrapper(ScriptLayout layout)
			: base(layout)
		{
		}

		/// <summary>
		/// Create a wrapper with a custom layout
		/// </summary>
		/// <param name="layout">override the default layout</param>
		/// <param name="before">object to render before the item</param>
		/// <param name="content">the item to render</param>
		/// <param name="after">object to render after the item</param>
		/// <returns></returns>
		public ScriptWrapper(ScriptLayout layout, object before, object content, object after)
			: base(layout)
		{
			this.SetWrapper(before, after);
			this.Content = content;
		}

		/// <summary>
		/// Create a wrapper
		/// </summary>
		/// <param name="before">object to render before the item</param>
		/// <param name="content">the item to render</param>
		/// <param name="after">object to render after the item</param>
		/// <returns></returns>
		public ScriptWrapper(object before, object content, object after)
			: base()
		{
			this.SetWrapper(before, after);
			this.Content = content;
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// Quck wat to set both Before and After objects
		/// </summary>
		/// <param name="before">object to render before the content</param>
		/// <param name="after">object to render after the content</param>
		public void SetWrapper(object before, object after)
		{
			_Before = before;
			_After = after;
		}
		#endregion

		#region Data
		private object _Before = null;
		/// <summary>
		/// Object to render Before the Content
		/// </summary>
		public object Before
		{
			get { return _Before; }
		}


		private object _Content;
		/// <summary>
		/// object to render as the Content
		/// </summary>
		public object Content
		{
			get { return _Content; }
			set
			{

				if (_Content != value)
				{
					_Content = value;
				}
			}
		}

		private object _After = null;
		/// <summary>
		/// Object to render After the Content
		/// </summary>
		public object After
		{
			get { return _After; }
		}



		private ScriptLayout _ContentLayout = ScriptLayout.None;
		/// <summary>
		/// The layout to apply to the content if not already stated
		/// </summary>
		public ScriptLayout ContentLayout
		{
			get { return _ContentLayout; }
			set
			{
				if (_ContentLayout != value)
				{
					_ContentLayout = value;
				}
			}
		}

		private int _InternalIndents = 1; // defaults to adding an indent to content
		/// <summary>
		/// The indentation level for the Content. Does not apply to the Begin and End parts
		/// </summary>
		public int InternalIndents
		{
			get { return _InternalIndents; }
			set { _InternalIndents = value; }
		}
		#endregion

		#region Layout Control
		/// <summary>
		/// Updates the content layout based on the wrappers layout
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLayoutChanged(LayoutChangedEventArgs e)
		{
			base.OnLayoutChanged(e);

			ScriptLayout layout = e.Layout;



			// a change in our layout changes our preferred layout for the Item
			switch (layout)
			{
				case ScriptLayout.None:
				case ScriptLayout.NotAlreadyEstablished:
				case ScriptLayout.Default:
					break;
				case ScriptLayout.Inline:
					ContentLayout = ScriptLayout.Inline;
					break;
				case ScriptLayout.InlineBlock:
					ContentLayout = ScriptLayout.Block;
					break;
				case ScriptLayout.Block:
					ContentLayout = ScriptLayout.Block;
					break;
				default:
					break;
			}
		}
		#endregion

		#region Rendering
		/// <summary>
		/// Renders the before, content and after objects
		/// content is indented by the InternalIndents value (default 1)
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

			IScriptWriter writer = e.Writer;


			Sb.TrySetLayout(Content, ContentLayout); // try and get it to use our preferred layout

			if (Before != null)
			{
				if (this.Layout == ScriptLayout.Block && Sb.HasRenderContent(Content))
					writer.WriteNewLineAndIndent();

				writer.Write(Before);
			}

			try
			{
				writer.BeginIndent(InternalIndents);

				writer.Write(Content);
			}
			finally
			{
				writer.EndIndent(InternalIndents);
			}

			if (this.After != null)
			{
				if ((this.Layout == ScriptLayout.Block || this.Layout == ScriptLayout.InlineBlock) && Sb.HasRenderContent(Content))
					writer.WriteNewLineAndIndent();

				writer.Write(After);
			}

		}

		/// <summary>
		/// True if Before, Content or After objects have content
		/// </summary>
		public override bool HasRenderContent
		{
			get
			{

				return
					Sb.HasRenderContent(Before) ||
					Sb.HasRenderContent(After) ||
					Sb.HasRenderContent(Content);
			}
		}
		#endregion

		#region IScriptIfCondition
		/// <summary>
		/// Asks the Content object for the result
		/// Before and After are not considdered
		/// </summary>
		public override bool ScriptIfResult
		{
			get
			{
				return ScriptIf.ObjectScriptIfResult(Content);
			}
		}
		#endregion
	}
}
