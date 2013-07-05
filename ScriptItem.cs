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
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;

namespace ClockWork.ScriptBuilder
{
    /// <summary>
    /// Base class for objects in a script that can be structured and know how to render themselves to a ScriptWriter
    /// </summary>
	public class ScriptItem : IScriptItem, IScriptIfCondition
	{
		#region Constructors
		/// <summary>
		/// Creates an empty script
		/// </summary>
		public ScriptItem()
			: this(ScriptLayout.None)
        {
            
        }
		/// <summary>
		/// Create an empty script with a custom layout
		/// </summary>
		/// <param name="layout">override the default layout</param>
		public ScriptItem(ScriptLayout layout)
		{
			this.TrySetLayout(layout, false); // construct layout should always be able to set the layout, hence false

			this.OnInitialise();
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// A good place to set default values for an item
		/// Called by the constructor
		/// </summary>
		protected virtual void OnInitialise()
		{
		}
		#endregion

		#region Layout Control
		/// <summary>
		/// Defines the default layout that this item with use
		/// </summary>
		public virtual ScriptLayout DefaultLayout
		{
			get { return ScriptLayout.Inline; }
		}

		private ScriptLayout _Layout = ScriptLayout.NotAlreadyEstablished;  
		/// <summary>
		/// How an item is going to be layed out.
		/// Inline = render without newlines
		/// InlineBlock = start on same line but each following internal item should be on a new line
		/// Block = start on a new line, all internal items should be on a new line 
		/// 
		/// Note: internal items may cause the content to render over several lines even though this item is Inline
		/// </summary>
		public ScriptLayout Layout
		{
			get
			{
				// will only return a real style
				switch (_Layout)
				{
					// virtual styles return the default
					case ScriptLayout.None:
						throw new Exception("Invalid Layout Value: " + _Layout);
					case ScriptLayout.NotAlreadyEstablished:
					case ScriptLayout.Default:
						{
							ScriptLayout defaultLayout = this.DefaultLayout;
							switch (defaultLayout)
							{
								case ScriptLayout.None:
								case ScriptLayout.Default:
								case ScriptLayout.NotAlreadyEstablished:
									throw new Exception("Invalid Default Layout: " + defaultLayout);
								case ScriptLayout.Inline:
								case ScriptLayout.InlineBlock:
								case ScriptLayout.Block:
								default:
									return defaultLayout;
							}							
						}

					// real styles
					case ScriptLayout.Inline:
					case ScriptLayout.InlineBlock:
					case ScriptLayout.Block:
					default:
						return _Layout;
				};

			}
		}
		/// <summary>
		/// Attempt to set the layout.
		/// General policy is to only set the layout if it has not been set yet.
		/// The presedence of layout control is:
		///		Constructor Based Layout if supplied directly by user (e.g. specifically created in script structure with layout)
		///		Constructor Based Layout if supplied by parent item (e.g. created by parent item with a layout)
		///		Set by Parent after contruction (e.g. parent has a preferred layout)
		///		The items Default layout
		/// 
		/// Initially the layout is set to Auto which indicates
		/// 
		/// Default: use this if you want to 
		/// </summary>
		/// <param name="layout">the layout preferred</param>
		/// <param name="onlyIfNotAlreadyEstablished">Use false if you wish to override natural presidence and for a layout change</param>
		public void TrySetLayout(ScriptLayout layout, bool onlyIfNotAlreadyEstablished)
		{
			if (!onlyIfNotAlreadyEstablished || !IsLayoutEstablished)
			{
				if (_Layout != layout)
				{

					switch (layout)
					{
						case ScriptLayout.None: // do nothing, cant set to None!
							break;
						case ScriptLayout.Default:
						case ScriptLayout.NotAlreadyEstablished: // a reset?
						case ScriptLayout.Inline:
						case ScriptLayout.InlineBlock:
						case ScriptLayout.Block:
						default:
							_Layout = layout;
							OnLayoutChanged(new LayoutChangedEventArgs(_Layout));
							break;
					}
				}
			}
		}
		/// <summary>
		/// Updates the layout if it has not already been set
		/// </summary>
		/// <param name="layout"></param>
		public void TrySetLayout(ScriptLayout layout)
		{
			this.TrySetLayout(layout, true);
		}


		/// <summary>
		/// Helper to try and set a layout for an item
		/// First checks if the sent object is an IScriptItem
		/// </summary>
		/// <param name="item">object to check</param>
		/// <param name="layout">preferred layout</param>
		public static void ObjectTrySetLayout(object item, ScriptLayout layout)
		{
			if (item is IScriptItem)
				((IScriptItem)item).TrySetLayout(layout);
		}

		/// <summary>
		/// Has a layout been requested
		/// This could be via the constructor or using the TrySetLayout method
		/// </summary>
		public bool IsLayoutEstablished
		{
			get
			{
				return _Layout != ScriptLayout.NotAlreadyEstablished;
			}
		}

		/// <summary>
		/// Triggered when the itmes layout is changed
		/// </summary>
		public event EventHandler<LayoutChangedEventArgs> LayoutChanged;
		/// <summary>
		/// Triggered when the items layout is changed
		/// subclasses may alter internal layouts when this is striggered
		/// </summary>
		/// <param name="e">includes the new layout</param>
		protected virtual void OnLayoutChanged(LayoutChangedEventArgs e)
		{
			if (LayoutChanged!=null)
				LayoutChanged(this, e);
		}
		#endregion

		#region Indentation
		private int _Indents = 0;
		/// <summary>
		/// How much this item should be indented by
		/// </summary>
        public int Indents
        {
            get { return _Indents; }
            set { _Indents = value; }
        }

		#endregion

		#region Rendering
		/// <summary>
		/// Render straight to string using the default script writer 
		/// </summary>
		/// <returns></returns>
		public string Render()
		{
			TextWriter writer = new StringWriter();
			Render(writer);

			return writer.ToString();
		}

		/// <summary>
		/// Rnder straight to a string using the specified format provider
		/// </summary>
		/// <param name="formatProvider">used when rendering</param>
		/// <returns></returns>
		public string Render(IFormatProvider formatProvider)
		{
			TextWriter textWriter = new StringWriter();
			ScriptWriter scriptWriter = new ScriptWriter(textWriter, formatProvider);

			Render(scriptWriter);

			return textWriter.ToString();
		}

		/// <summary>
		/// Render to a stream
		/// </summary>
		/// <param name="stream"></param>
		public void Render(Stream stream)
		{
			StreamWriter writer = new StreamWriter(stream);
			Render(writer);
		}

		/// <summary>
		/// Render to a stream using the specified format provider
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="formatProvider"></param>
		public void Render(Stream stream, IFormatProvider formatProvider)
		{
			StreamWriter writer = new StreamWriter(stream);
			Render(writer, formatProvider);
		}

		/// <summary>
		/// Render to a string builder
		/// </summary>
		/// <param name="stringBuilder"></param>
		public void Render(StringBuilder stringBuilder)
		{
			StringWriter writer = new StringWriter(stringBuilder);
			Render(writer);
		}

		/// <summary>
		/// Render to a string builder using the specified format provider
		/// </summary>
		/// <param name="stringBuilder"></param>
		/// <param name="formatProvider"></param>
		public void Render(StringBuilder stringBuilder, IFormatProvider formatProvider)
		{
			StringWriter writer = new StringWriter(stringBuilder);
			Render(writer, formatProvider);
		}

		/// <summary>
		/// Render to a stream writer
		/// </summary>
		/// <param name="writer"></param>
		public void Render(StreamWriter writer)
		{
			IScriptWriter scriptWriter = new ScriptWriter(writer);
			Render(scriptWriter);
		}

		/// <summary>
		/// Render to a stream writer using the specified format provider
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="formatProvider"></param>
		public void Render(StreamWriter writer, IFormatProvider formatProvider)
		{
			IScriptWriter scriptWriter = new ScriptWriter(writer, formatProvider);
			Render(scriptWriter);
		}

		/// <summary>
		/// Render to a text writer
		/// </summary>
		/// <param name="writer"></param>
		public void Render(TextWriter writer)
		{
			IScriptWriter scriptWriter = new ScriptWriter(writer);
			Render(scriptWriter);
		}

		/// <summary>
		/// Render to a text writer using the specified format provider
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="formatProvider"></param>
		public void Render(TextWriter writer, IFormatProvider formatProvider)
		{
			IScriptWriter scriptWriter = new ScriptWriter(writer,formatProvider);
			Render(scriptWriter);
		}

		/// <summary>
		/// Render the content to a script writer
		/// </summary>
		/// <param name="writer"></param>
		public void Render(IScriptWriter writer)
        {
            Render(writer, 0);
        }


		/// <summary>
		/// Render the content to a script writer indenting the content as specified
		/// </summary>
		/// <param name="writer">the script writer to render to</param>
		/// <param name="indents">how many indents to add. These indents are added to the item own Indent level</param>
		public void Render(IScriptWriter writer, int indents)
		{
			try
			{

				int totalIndents = 0;

				if (this.Layout == ScriptLayout.Block || this.Layout == ScriptLayout.InlineBlock) // don't indent if not multiline
					totalIndents = Indents + indents;

				try
				{
					writer.BeginIndent(totalIndents);



					OnRender(new RenderingEventArgs(writer));
				}
				finally
				{
					writer.EndIndent(totalIndents);
				}

			}
			finally
			{
				writer.Flush();
			}
        }


		/// <summary>
		/// Triggered when the content is to being rendered
		/// You can use this with a baisc ScriptItem to create a rendeing proxy 
		/// i.e. place a script item where you want to render and listen to this event. 
		/// when triggered render to the writer in the arguments
		/// </summary>
		public event EventHandler<RenderingEventArgs> Rendering;


		/// <summary>
		/// This is called when the content is to be rendered
		/// Subclasses will do what has to be done!
		/// </summary>
		/// <param name="e">includes the script writer to render content to</param>
		protected virtual void OnRender(RenderingEventArgs e)
		{
			if (Rendering != null)
				Rendering(this, e);
		}

		/// <summary>
		/// Will this item rendered to more than en empty string
		/// Subclasses should override this if nned be
		/// Used as the default result for ScriptIf testing
		/// </summary>
		/// <returns></returns>
		public virtual bool HasRenderContent
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Helper to test if an object will render as more than an empty string
		/// </summary>
		/// <param name="item">object to check</param>
		/// <returns></returns>
		public static bool ObjectHasRenderContent(object item)
		{
			if (item == null)
				return false;

			if (item is IScriptItem)
				return ((IScriptItem)item).HasRenderContent;

			if (item is string)
				return !String.IsNullOrEmpty((string)item);

			return true;

		}

		#endregion

		#region IScriptIfCondition
		/// <summary>
		/// For example a list with no values may return false
		/// Used in Sb.If testing
		/// A good use is to indicate if an item needs to be included
		/// default behavior is to return HasRenderContent
		/// can be overriden by subclasses
		/// </summary>
		/// <returns></returns>
		public virtual bool ScriptIfResult
		{
			get
			{
				return this.HasRenderContent;
			}
		}
		#endregion

	}
}
