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
	/// Renders function call syntax:
	/// functionName(arg, arg) 
	/// </summary>
	public class JsCall : ScriptItem
	{
		#region Construction


		/// <summary>
		/// create a function call:
		/// functionName(arguments)
		/// </summary>
		/// <param name="functionName"></param>
		/// <param name="arguments"></param>
        public JsCall(object functionName, JsArguments arguments)
            : base()
        {
			FunctionName = functionName;

			Arguments = arguments;

        }
		/// <summary>
		/// create a function call:
		/// functionName(arguments)
		/// </summary>
		/// <param name="functionName"></param>
		/// <param name="arguments"></param>
        public JsCall(object functionName, params object[] arguments)
            : base()
        {
			FunctionName = functionName;
			Arguments = Js.Arguments(ScriptLayout.Inline, arguments);
        }
		/// <summary>
		/// create a function call:
		/// functionName(arguments)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="functionName"></param>
		/// <param name="arguments"></param>
        public JsCall(ScriptLayout layout, object functionName, JsArguments arguments)
			: base(layout)
		{
			FunctionName = functionName;

			Arguments = arguments;

		}
		/// <summary>
		/// create a function call:
		/// functionName(arguments)
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="functionName"></param>
		/// <param name="arguments"></param>
        public JsCall(ScriptLayout layout, object functionName, params object[] arguments)
			: base(layout)
        {
			FunctionName = functionName;
			Arguments = Js.Arguments(ScriptLayout.Inline, arguments);
		}

		#endregion

		#region Data


		private JsArguments _Arguments;
		/// <summary>
		/// The arguments passed in the call
		/// </summary>
		public JsArguments Arguments
		{
			get
			{
				if (_Arguments == null)
					_Arguments = Js.Arguments();

				return _Arguments;
			}

			set { _Arguments = value; }
		}

        private object _FunctionName;
		/// <summary>
		/// The name of the function to call
		/// </summary>
        public object FunctionName
		{
			get { return _FunctionName; }
			set { _FunctionName = value; }
		}

		#endregion

		#region Rendering
		/// <summary>
		/// render the call
		/// functionName(arg, arg, arg)
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
        {
			base.OnRender(e);

			IScriptWriter writer = e.Writer;


			if (this.Layout == ScriptLayout.Block)
                writer.WriteNewLineAndIndent();

            writer.Write(FunctionName);

			try
			{
				writer.BeginIndent();

				writer.Write(Arguments);
			}
			finally
			{
				writer.EndIndent();
			}

		//	if (this.MultiLine && !Parameters.IsNothing)
        //        writer.WriteNewLineAndIndent();

		}

		#endregion
	}
}