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

namespace ClockWork.ScriptBuilder.JavaScript
{
	/// <summary>
	/// Renders function syntax:
	/// function name(param, param) { script } 
	/// function(param, param) { script }
	/// </summary>
	public class JsFunction : ScriptItem
	{
		#region Constructors

		/// <summary>
		/// Create a function
		/// </summary>
		public JsFunction()
        {
        }

		/// <summary>
		/// Create a named function
		/// function name(param, param) { script } 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parameters"></param>
		/// <param name="script"></param>
		public JsFunction(string name, JsParameters parameters, JsBlock script)
		{
			Name = name;
			Parameters = parameters;
			Block = script;
		}

		// Named functions

		/// <summary>
		/// Create a named function
		/// function name() { script } 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="scriptBlock"></param>
		public JsFunction(string name, JsBlock scriptBlock)
        {
			Name = name;
			Block = scriptBlock;
        }

		/// <summary>
		/// Create a named function
		/// function name(param, param) { lines } 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="parameters"></param>
		/// <param name="lines"></param>
		public JsFunction(string name, JsParameters parameters, params object[] lines)
		{
			Name = name;
			Parameters = parameters;
			Block = Js.Block(lines);
		}

		/// <summary>
		/// Create a named function
		/// function name() { lines } 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="lines"></param>
		public JsFunction(string name, params object[] lines)
		{
			Name = name;
			Block = Js.Block(lines);
		}


		/// <summary>
		/// Create an anonymous function
		/// function(param, param) { script } 
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="scriptBlock"></param>
		public JsFunction(JsParameters parameters, JsBlock scriptBlock)
			: this(null, parameters, scriptBlock)
		{
			Parameters = parameters;
			Block = scriptBlock;
		}
		/// <summary>
		/// Create an anonymous function
		/// function() { script } 
		/// </summary>
		/// <param name="scriptBlock"></param>
		public JsFunction(JsBlock scriptBlock)
		{
			Block = scriptBlock;
		}
		/// <summary>
		/// Create an anonymous function
		/// function(param, param) {  } 
		/// </summary>
		/// <param name="parameters"></param>
		public JsFunction(JsParameters parameters)
		{
			Parameters = parameters;
		}
		/// <summary>
		/// Create an anonymous function
		/// function(param, param) { lines } 
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="lines"></param>
		public JsFunction(JsParameters parameters, params object[] lines)
		{
			Parameters = parameters;
			Block = Js.Block(lines);
		}


		/// <summary>
		/// Create an anonymous function
		/// function() { lines } 
		/// </summary>
		/// <param name="lines"></param>
		public JsFunction(params object[] lines)
		{
			Block = Js.Block(lines);
		}


		/// <summary>
		/// Create an anonymous function
		/// function() {  } 
		/// </summary>
		/// <param name="layout"></param>
		public JsFunction(ScriptLayout layout)
			: base(layout)
		{
		}

		/// <summary>
		/// Create a named function
		/// function name(param, param) { lines } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="parameters"></param>
		/// <param name="script"></param>
		public JsFunction(ScriptLayout layout, string name, JsParameters parameters, JsBlock script)
			: base(layout)
		{
			Name = name;
			Parameters = parameters;
			Block = script;
		}


		/// <summary>
		/// Create a named function
		/// function name() { script } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="scriptBlock"></param>
		public JsFunction(ScriptLayout layout, string name, JsBlock scriptBlock)
			: base(layout)
		{
			Name = name;
			Block = scriptBlock;
		}

		/// <summary>
		/// Create a named function
		/// function name(param, param) { lines } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="parameters"></param>
		/// <param name="lines"></param>
		public JsFunction(ScriptLayout layout, string name, JsParameters parameters, params object[] lines)
			: base(layout)
		{
			Name = name;
			Parameters = parameters;
			Block = Js.Block(lines);
		}

		/// <summary>
		/// Create a named function
		/// function name() { lines } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="lines"></param>
		public JsFunction(ScriptLayout layout, string name, params object[] lines)
			: base(layout)
		{
			Name = name;
			Block = Js.Block(lines);
		}


		/// <summary>
		/// Create an anonymous function
		/// function(param, param) { script }  
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="parameters"></param>
		/// <param name="scriptBlock"></param>
		public JsFunction(ScriptLayout layout, JsParameters parameters, JsBlock scriptBlock)
			: base(layout)
		{
			Parameters = parameters;
			Block = scriptBlock;
		}

		/// <summary>
		/// Create an anonymous function
		/// function() { script } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="scriptBlock"></param>
		public JsFunction(ScriptLayout layout, JsBlock scriptBlock)
			: base(layout)
		{
			Block = scriptBlock;
		}
		/// <summary>
		/// Create an anonymous function
		/// function(param, param) {  } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="parameters"></param>
		public JsFunction(ScriptLayout layout, JsParameters parameters)
			: base(layout)
		{
			Parameters = parameters;
		}
		/// <summary>
		/// Create an anonymous function
		/// function(param, param) { lines } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="parameters"></param>
		/// <param name="lines"></param>
		public JsFunction(ScriptLayout layout, JsParameters parameters, params object[] lines)
			: base(layout)
		{
			Parameters = parameters;
			Block = Js.Block(lines);
		}

		/// <summary>
		/// Create an anonymous function
		/// function() { lines } 
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="lines"></param>
		public JsFunction(ScriptLayout layout, params object[] lines)
			: base(layout)
		{
			Block = Js.Block(lines);
		}

		#endregion

		#region Data
		private string _Name = null;
		/// <summary>
		/// The name of the function
		/// if empty it will be an anonymous function
		/// </summary>
		public string Name
		{
			get { return _Name; }
			set { _Name = value; }
		}

		private JsParameters _Parameters;
		/// <summary>
		/// The parameters the function accepts
		/// </summary>
		public JsParameters Parameters
		{
			get
			{
				if (_Parameters == null)
					_Parameters = Js.Parameters();

				return _Parameters;
			}
			set { _Parameters = value; }
		}

		private JsBlock _Block;
		/// <summary>
		/// The code for the function
		/// </summary>
		public JsBlock Block
		{
			get
			{
				if (_Block == null)
					_Block = Js.Block(BlockLayout);

				return _Block;
			}
			set
			{
				if (_Block != value)
				{
					_Block = value;
				}
			}
		}

		#endregion

		#region Layout Control

		private ScriptLayout _BlockLayout = ScriptLayout.NotAlreadyEstablished;
		/// <summary>
		/// The way the code section should be layed out
		/// </summary>
		public ScriptLayout BlockLayout
		{
			get { return _BlockLayout; }
			set
			{
				if (_BlockLayout != value)
				{
					_BlockLayout = value;
				}
			}
		}
		/// <summary>
		/// Make the block layout default to the same as the functions layout
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLayoutChanged(LayoutChangedEventArgs e)
		{
			base.OnLayoutChanged(e);

			ScriptLayout layout = e.Layout;


			BlockLayout = layout;
			//switch (format)
			//{
			//    case ScriptFormat.None:
			//        BlockFormat = ScriptFormat.None;
			//        break;
			//    case ScriptFormat.MultiLine:
			//        BlockFormat = ScriptFormat.InlineBlock;
			//        break;
			//    case ScriptFormat.StartOnNewLine:
			//        BlockFormat = ScriptFormat.Block;
			//        break;
			//    case ScriptFormat.Inline:
			//        BlockFormat = ScriptFormat.Inline;
			//        break;
			//    case ScriptFormat.InlineBlock:
			//        BlockFormat = ScriptFormat.InlineBlock;
			//        break;
			//    case ScriptFormat.Block:
			//        BlockFormat = ScriptFormat.Block;
			//        break;
			//    default:
			//        break;
			//}
		}

		#endregion

		#region Rendering

		

		/// <summary>
		/// render the function
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
        {
			base.OnRender(e);

			IScriptWriter writer = e.Writer;

			// default the block layout based on the parameters layout if possible
			if (BlockLayout == ScriptLayout.NotAlreadyEstablished)
			{
				if (Parameters.List.Layout != ScriptLayout.Inline)
					Block.TrySetLayout(ScriptLayout.Block);
				else
					Block.TrySetLayout(ScriptLayout.InlineBlock);
			}
			else
				Block.TrySetLayout(BlockLayout);



			if (this.Layout == ScriptLayout.Block)
                writer.WriteNewLineAndIndent();

			
			if (!String.IsNullOrEmpty(Name))
			{
				writer.Write("function "+Name); // named function
			}
			else
			{
				writer.Write("function"); // anonymous function
			}

			try
			{
				writer.BeginIndent();

				writer.Write(Parameters);
			}
			finally
			{
				writer.EndIndent();
			}


			writer.Write(" ");



			writer.Write(Block);

		//	if (Block.MultiLine && !Block.IsNothing)
      //          writer.WriteNewLineAndIndent();

		}

		#endregion

	}
}