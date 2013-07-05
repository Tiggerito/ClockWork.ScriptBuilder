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
	/// Renders new syntax:
	/// new className(arg, arg, arg)
	/// </summary>
	public class JsNew : ScriptItem
	{
		#region Constructors
		/// <summary>
		/// Create a new function call:
		/// new className(args)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="className"></param>
		/// <param name="args"></param>
        public JsNew(ScriptLayout layout, object className, params object[] args)
			: base(layout)
		{
			_Call = new JsCall(className, args);
		}
		/// <summary>
		/// Create a new function call:
		/// new className(args)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="className"></param>
		/// <param name="args"></param>
        public JsNew(ScriptLayout layout, object className, JsArguments args)
			: base(layout)
		{
			_Call = new JsCall(className, args);
		}
		/// <summary>
		/// Create a new function call based on an existing function call
		/// new call
		/// </summary>
		/// <param name="call"></param>
		public JsNew(JsCall call)
		{
			_Call = call;
		}
		/// <summary>
		/// Create a new function call:
		/// new className(args)
		/// </summary>
		/// <param name="className"></param>
		/// <param name="args"></param>
        public JsNew(object className, params object[] args)
		{
			_Call = new JsCall(className, args);
		}

		/// <summary>
		/// Create a new function call:
		/// new className(args)
		/// </summary>
		/// <param name="className"></param>
		/// <param name="args"></param>
		public JsNew(string className, JsArguments args)
		{
			_Call = new JsCall(className, args);
		}
		#endregion

		#region Data


		private JsCall _Call;
		/// <summary>
		/// the function call to create the new object
		/// </summary>
		public JsCall Call
		{
			get { return _Call; }
			set { _Call = value; }
		}
		#endregion

		#region Rendering

		/// <summary>
		/// Renders new syntax:
		/// new className(arg, arg, arg)
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

			IScriptWriter writer = e.Writer;


			if (this.Layout == ScriptLayout.Block)
				writer.WriteNewLineAndIndent();

			writer.Write("new ");

			writer.Write(Call);

		}

		#endregion
	}
}
