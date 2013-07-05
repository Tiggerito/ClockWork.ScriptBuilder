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
using System.IO;

namespace ClockWork.ScriptBuilder.JavaScript
{
	/// <summary>
	/// Wrap single or double quotes round some text.
	/// single quotes by default.
	/// </summary>
	public class JsQuote : ScriptItem
	{
		#region Constructors
		/// <summary>
		/// Wrap single or double quotes round the text.
		/// </summary>
		/// <param name="text">text to be placed in the quotes</param>
		/// <param name="doubleQuotes">true if it should use double quotes</param>
		public JsQuote(object text, bool doubleQuotes)
            : base()
        {
            Text = text;
            DoubleQuotes = doubleQuotes;
        }
		/// <summary>
		/// Wrap single quotes round the text.
		/// </summary>
		/// <param name="text"></param>
        public JsQuote(object text)
			: this(text, false)
        {
        }
		/// <summary>
		/// Creates an empty set of single quotes.
		/// add content using the Text property.
		/// change to double quotes uing the DoubleQuote property.
		/// </summary>
        public JsQuote()
			: this(null, false)
        {
		}
		#endregion

		#region Data
		private object _Text;
		/// <summary>
		/// Text to go inside the quotes.
		/// it will be rendered to a string then quotes and new lines escaped
		/// </summary>
        public object Text
        {
            get { return _Text; }
            set { _Text = value; }
		}
		

		private bool _DoubleQuotes = false;
		/// <summary>
		/// Set to true if double quotes are required
		/// </summary>
        public bool DoubleQuotes
        {
            get { return _DoubleQuotes; }
            set { _DoubleQuotes = value; }
		}
		#endregion

		#region Rendering
		/// <summary>
		/// Wraps the quotes around the rendering of the text object
		/// escapes internal quotes and new lines
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
        {
			base.OnRender(e);

			IScriptWriter writer = e.Writer;


			if (this.Layout == ScriptLayout.Block)
                writer.WriteNewLineAndIndent();

            string quote = this.DoubleQuotes ? "\"" : "'";
            if (Text == null)
                writer.Write(quote + quote);

            // as we have to process the contents we will have to render it seperately
            StringWriter tw = new StringWriter();
            ScriptWriter sw = new ScriptWriter(tw, e.Writer); // use same format provider
            
            sw.Write(this.Text);

            string text = tw.ToString();

            // now process it
			text = text.Replace(quote, @"\" + quote);

			text = text.Replace(Environment.NewLine, "\\n\\r");

			writer.Write(quote + text + quote);
		}
		#endregion
	}
}
