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

using System.Collections.Generic;
using System.Text;

namespace ClockWork.ScriptBuilder.JavaScript
{
	/// <summary>
	/// Renders block syntax:
	/// { 
	///		line 
	///		line 
	///		line 
	/// }
	/// </summary>
    public class JsBlock : ScriptSetWrapper
	{
		#region Constructors

		
		/// <summary>
		/// Create an empty block
		/// {}
		/// </summary>
		public JsBlock() 
		{
			Set = new Script();
		}

		/// <summary>
		/// Create a block
		/// {
		///		line
		///		line
		///		line
		/// }
		/// </summary>
		/// <param name="lines"></param>
        public JsBlock(IEnumerable<object> lines) 
		{
			Set = new Script(lines);
		}

		/// <summary>
		/// Create a block
		/// {
		///		line
		///		line
		///		line
		/// }
		/// </summary>
		/// <param name="lines"></param>
		public JsBlock(params object[] lines)  
		{
			Set = new Script(lines);
		}

		/// <summary>
		/// Create a block
		/// {
		///		script
		/// }
		/// </summary>
		/// <param name="script"></param>
		public JsBlock(Script script)
		{
			Set = script;
		}

		/// <summary>
		/// Create an empty block
		/// {}
		/// </summary>
		/// <param name="layout"></param>
		public JsBlock(ScriptLayout layout)
			: base(layout)
		{
			Set = new Script();
		}

		/// <summary>
		/// Create a block
		/// {
		///		line
		///		line
		///		line
		/// }
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="lines"></param>
		public JsBlock(ScriptLayout layout, IEnumerable<object> lines)
			: base(layout)
		{
			Set = new Script(lines);
		}

		/// <summary>
		/// Create a block
		/// {
		///		line
		///		line
		///		line
		/// }
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="lines"></param>
		public JsBlock(ScriptLayout layout, params object[] lines)
			: base(layout)
		{
			Set = new Script(lines);
		}

		/// <summary>
		/// Create a block
		/// {
		///		script
		/// }
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="script"></param>
		public JsBlock(ScriptLayout layout, Script script)
			: base(layout)
		{
			Set = script;
		}

		#endregion

		#region Initialisation

		/// <summary>
		/// wraps the items on curly brackets
		/// </summary>
		protected override void OnInitialise()
        {
            base.OnInitialise();

            this.SetWrapper("{", "}");

			this.InternalIndents = 1;
		}

		#endregion

		#region Data
		/// <summary>
		/// The script placed within the block
		/// </summary>
		public Script Script
		{
			get { return Set as Script; }
			set { Set = value; }
		}
		#endregion

		#region Layout Control
		/// <summary>
		/// Defines the default layout that this item with use
		/// </summary>
		public override ScriptLayout DefaultLayout
		{
			get { return ScriptLayout.Block; }
		}
		#endregion

	}
}