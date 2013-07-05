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
	/// renders as a single line ended with a semi colon (;)
	/// each items is concatinated together using a ScriptLine
	/// </summary>
	public class JsStatement : ScriptSetWrapper
	{
		#region Constructors
		/// <summary>
		/// Create an empty statement
		/// ;
		/// </summary>
		public JsStatement() 
		{
			Line = new ScriptLine();
		}
		/// <summary>
		/// Create a statement
		/// parts;
		/// </summary>
		/// <param name="parts"></param>
        public JsStatement(IEnumerable<object> parts) 
		{
			Line = new ScriptLine(parts);
		}
		/// <summary>
		/// Create a statement
		/// parts;
		/// </summary>
		/// <param name="parts"></param>
		public JsStatement(params object[] parts)  
		{
			Line = new ScriptLine(parts);
		}
		/// <summary>
		/// Create a statement
		/// line;
		/// </summary>
		/// <param name="line"></param>
		public JsStatement(ScriptLine line)
		{
			Line = line;
		}

		/// <summary>
		/// Create an empty statement
		/// ;
		/// </summary>
		/// <param name="layout"></param>
		public JsStatement(ScriptLayout layout)
			: base(layout)
		{
			Line = new ScriptLine();
		}

		/// <summary>
		/// Create a statement
		/// parts;
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="parts"></param>
		public JsStatement(ScriptLayout layout, IEnumerable<object> parts)
			: base(layout)
		{
			Line = new ScriptLine(parts);
		}

		/// <summary>
		/// Create a statement
		/// parts;
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="parts"></param>
		public JsStatement(ScriptLayout layout, params object[] parts)
			: base(layout)
		{
			Line = new ScriptLine(parts);
		}

		/// <summary>
		/// Create a statement
		/// line;
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="line"></param>
		public JsStatement(ScriptLayout layout, ScriptLine line)
			: base(layout)
		{
			Line = line;
		}
		#endregion

		#region Initialisation
		/// <summary>
		/// make it render ending in a semi colon (;)
		/// </summary>
		protected override void OnInitialise()
        {
            base.OnInitialise();

            this.SetWrapper(null, ";");
			this.InternalIndents = 0;
		}
		#endregion

		#region Data
		/// <summary>
		/// The contents of the wrapper is a ScriptLine
		/// </summary>
		public ScriptLine Line
		{
			get { return Set as ScriptLine; }
			set { Set = value; }
		}
		#endregion
	}
}
