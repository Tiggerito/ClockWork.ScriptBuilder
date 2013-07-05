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
	/// A property of an object.
	/// name: value
	/// </summary>
	public class JsProperty : ScriptItem
	{
		#region Constructors
		/// <summary>
		/// create an objects property
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
        public JsProperty(object name, object value)
            :base()
        {
            _Name = name;
            _Value = value;
        }
		/// <summary>
		/// create an objects property
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="name"></param>
		/// <param name="value"></param>
        public JsProperty(ScriptLayout layout, object name, object value)
			: base(layout)
		{
			_Name = name;
			_Value = value;
		}
		#endregion

		#region Data
        private object _Name;
		/// <summary>
		/// The property name
		/// </summary>
        public object Name
		{
			get { return _Name; }
			set { _Name = value; }
		}
		private object _Value;
		/// <summary>
		/// the value the property will be set to.
		/// If it is a string ensure you use Js.Q("text") to place it in quotes
		/// </summary>
		public object Value
		{
			get { return _Value; }
			set { _Value = value; }
		}
		#endregion

		#region Rendering
		/// <summary>
		/// Rneders as
		/// name: value
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
        {
			base.OnRender(e);

			IScriptWriter writer = e.Writer;


            if (this.Layout == ScriptLayout.Block)
                writer.WriteNewLineAndIndent();

			// TODO: it looks like names can be quoted (single or double). not sure when that is required or if any escaping is needed!

            writer.Write(Name);

            writer.Write(": ");

            writer.Write(Value);
        }

		/// <summary>
		/// True if it has a name
		/// </summary>
		public override bool HasRenderContent
        {
			get
			{
                
				return Sb.HasRenderContent(Name); // ????
			}
		}
		#endregion
	}
}