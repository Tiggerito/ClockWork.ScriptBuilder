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
using System.Security;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ClockWork.ScriptBuilder
{

	/// <summary>
	/// A writer that adds the following to the normal TextWriter
	/// Indentation Control - tracks and adds indentation while writing
	/// Accepts objects and formats then based on a format provider (default is ScriptFormatProvider)
	/// Supports IScriptItem based classes and their self rendering ability
	/// 
	/// </summary>
	public class ScriptWriter : IScriptWriter
	{
		#region Constructors
		/// <summary>
		/// Create a Script Writer that writes to a specific TextWriter
		/// </summary>
		/// <param name="writer"></param>
		public ScriptWriter(TextWriter writer)
		{
			if (writer == null)
				throw new Exception("ScriptWriter does not like null TextWriters");

			Writer = writer;

		}
		/// <summary>
		/// Create a Script Writer that writes to a Stream
		/// </summary>
		/// <param name="stream"></param>
		public ScriptWriter(Stream stream)
		{
			if (stream == null)
				throw new Exception("ScriptWriter does not like null Streams");

			Writer = new StreamWriter(stream);

		}

		/// <summary>
		/// Create a Script Writer that writes to a specific TextWriter using a particular format provider
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="formatProvider"></param>
		public ScriptWriter(TextWriter writer, IFormatProvider formatProvider)
		{
			if (writer == null)
				throw new Exception("ScriptWriter does not like null TextWriters");

			_FormatProvider = formatProvider;
			Writer = writer;

		}
		/// <summary>
		/// Create a Script Writer that writes to a Stream using a particular format provider
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="formatProvider"></param>
		public ScriptWriter(Stream stream, IFormatProvider formatProvider)
		{
			if (stream == null)
				throw new Exception("ScriptWriter does not like null Streams");

			_FormatProvider = formatProvider;
			Writer = new StreamWriter(stream);

		}

        /// <summary>
        /// Create a Script Writer that writes to a specific TextWriter based on another ScriptWriters settings
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="scriptWriter"></param>
        public ScriptWriter(TextWriter writer, IScriptWriter scriptWriter)
        {
            if (writer == null)
                throw new Exception("ScriptWriter does not like null TextWriters");

            Writer = writer;

            this.CopySettings(scriptWriter);

        }
        /// <summary>
        /// Create a Script Writer that writes to a Stream using a particular format provider
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="scriptWriter"></param>
        public ScriptWriter(Stream stream, IScriptWriter scriptWriter)
        {
            if (stream == null)
                throw new Exception("ScriptWriter does not like null Streams");

            
            Writer = new StreamWriter(stream);

            this.CopySettings(scriptWriter);

        }
        /// <summary>
        /// Copies settings from the provided writer so that this writer will write in the same way
        /// </summary>
        /// <param name="scriptWriter"></param>
        protected void CopySettings(IScriptWriter scriptWriter)
        {
            this._FormatProvider = scriptWriter.FormatProvider;

            if (scriptWriter is ScriptWriter)
            {
                ScriptWriter sw = (ScriptWriter)scriptWriter;

                this.IncludeIndentation = sw.IncludeIndentation;
                this.IndentText = sw.IndentText;
                this.NewLine = sw.NewLine;
                this.Compress = sw.Compress;
            }
        }
		#endregion

		#region Data
		/// <summary>
		/// The underlying writer
		/// </summary>
		private TextWriter Writer = null;
		#endregion

		#region Writers
		/// <summary>
		/// Write an object
		/// If its a IScriptItem then use its Render method
		/// First formats the object using the Format method
		/// </summary>
		/// <param name="o">object to write to the undelying TextWriter</param>
		public virtual void Write(object o)
		{
			if (Sb.HasRenderContent(o))
			{

				if (o is IScriptItem)
					((IScriptItem)o).Render(this);
				else
				{
					string formatted = Format(o);

			//		Debug.Write(formatted);


					Writer.Write(formatted);
				}

				IsStartOfLine = false; //  do before as could end up being reset while writing object
			}
		}

		/// <summary>
		/// Start a new line and indent to the required position
		/// Prefered way to move to the next line
		/// </summary>
		/// <param name="unlessAtStartOfLine">If true don't do anything if currently at the start of a line</param>
		public virtual void WriteNewLineAndIndent(bool unlessAtStartOfLine)
		{
			if (!unlessAtStartOfLine || !IsStartOfLine)
			{
				WriteNewLine();
				WriteIndent();

				IsStartOfLine = true;
			}

		}
		/// <summary>
		/// Start a new line and indent to the required position
		/// Prefered way to move to the next line
		/// Does nothing if currently at the start of a line
		/// </summary>
		public virtual void WriteNewLineAndIndent()
		{
			WriteNewLineAndIndent(true);
		}

		/// <summary>
		/// Starts a new line.
		/// Should generaly be folled by a WriteIndent() so following text is in the correct position
		/// WriteNewLineAndIndent() may be preferable.
		/// </summary>
		public virtual void WriteNewLine()
		{
			this.Write(this.NewLine);
			indentsOnOneLine = 0;
		}

		/// <summary>
		/// Adds the indent string
		/// Should always be after a newline to make sense 
		/// WriteNewLineAndIndent() may be preferable.
		/// </summary>
		public virtual void WriteIndent()
		{
			this.Write(this.IndentString);
		}
		#endregion

		#region Formatting

		private IFormatProvider _FormatProvider = null;
		/// <summary>
		/// The format provider to use when formatting
		/// </summary>
		public  IFormatProvider FormatProvider
		{
			get 
			{
				if (_FormatProvider == null)
					_FormatProvider = new ScriptFormatProvider();

				return _FormatProvider; 
			}
			//set
			//{
			//    _FormatProvider = value;
			//}
		}


		/// <summary>
		/// Formats the object using the current FormatProvider
		/// </summary>
		/// <param name="o">object to format</param>
		/// <returns>string representation of the object</returns>
		public string Format(object o)
		{
			if (o == null)
				return String.Empty;

			return this.Format("{0}", o);
		}
		/// <summary>
		/// Formats the object using the current FormatProvider
		/// </summary>
		/// <param name="format">format string to use</param>
		/// <param name="o">object to format</param>
		/// <returns>string representation of the object</returns>
		public string Format(string format, object o)
		{
			return String.Format(this.FormatProvider, format, o);
		}

        private bool _Compress;
        /// <summary>
        /// writer requests that compressed content is provided when writing
        /// </summary>
        public bool Compress
        {
            get { return _Compress; }
            set { _Compress = value; }
        }


		#endregion

		#region Indentation
		/// <summary>
		/// Increase the indentation level by one
		/// BeginIndent() and EndIndent() should be used in pairs
		/// Preferably using a try finally pattern so as to avoid messed up formatting on errors
		/// </summary>
		public void BeginIndent()
		{
			BeginIndent(1);
		}
		/// <summary>
		/// Increase the indentation level by the supplied amount
		/// BeginIndent() and EndIndent() should be used in pairs
		/// Preferably using a try finally pattern so as to avoid messed up formatting on errors
		/// </summary>
		/// <param name="levels">How many indentations to add</param>
		public void BeginIndent(int levels)
		{
			CurrentIndentLevel += levels;
			indentsOnOneLine += levels;
			//		Writer.Write("+" + levels + "=" + this.CurrentIndentLevel +"#"+indentsOnOneLine + "+");

		}

		private int indentsOnOneLine = 0; // experimental idea to handle pre-un-indentingy type thing

		/// <summary>
		/// Decrease the indentation level by one
		/// BeginIndent() and EndIndent() should be used in pairs
		/// Preferably using a try finally pattern so as to avoid messed up formatting on errors
		/// </summary>
		public void EndIndent()
		{
			EndIndent(1);
		}

		/// <summary>
		/// Decrease the indentation level by the supplied amount
		/// BeginIndent() and EndIndent() should be used in pairs
		/// Preferably using a try finally pattern so as to avoid messed up formatting on errors
		/// </summary>
		/// <param name="levels">How many indentations to remove</param>
		public void EndIndent(int levels)
		{
			if (CurrentIndentLevel >= levels)
			{
				CurrentIndentLevel -= levels;
				indentsOnOneLine -= levels;
			}
			else
				CurrentIndentLevel = 0;
			//		Writer.Write("-" + levels + "=" + this.CurrentIndentLevel+"#"+indentsOnOneLine  + "-");

		}


		private Int32 _CurrentIndentLevel;
		/// <summary>
		/// Tracks how many indentations we are currently at
		/// Increased by BeginIndent()
		/// Reduced by EndIndent()
		/// </summary>
		public Int32 CurrentIndentLevel
		{
			get { return _CurrentIndentLevel; }
			set { _CurrentIndentLevel = value; }
		}

		private string _IndentText = "\t";
		/// <summary>
		/// The string to use for each indentation
		/// Repeated fore each IndentLevel
		/// Default: tab
		/// </summary>
		public string IndentText
		{
			get { return _IndentText; }
			set { _IndentText = value; }
		}

		private bool _IncludeIndentation = true;
		/// <summary>
		/// If we should Include indents when writing
		/// If false then IndentString is always empty
		/// </summary>
		public bool IncludeIndentation
		{
			get { return _IncludeIndentation; }
			set { _IncludeIndentation = value; }
		}

		/// <summary>
		/// The current string to use for indentation
		/// Empty if IncludeIndents is false
		/// otherwise IndentationLevel * IndentText
		/// </summary>
		public virtual string IndentString
		{
			get
			{
				if (IncludeIndentation)
				{
					if (Indents.ContainsKey(CurrentIndentLevel))
						return Indents[CurrentIndentLevel];

					string indent = String.Empty;

					for (int i = 0; i < CurrentIndentLevel; i++)
						indent += IndentText;

					Indents[CurrentIndentLevel] = indent;

					return indent;
				}
				else
					return String.Empty;
			}
		}

		private Dictionary<int, string> _Indents = null;
		/// <summary>
		/// Cache of indent levels already created
		/// </summary>
		protected Dictionary<int, string> Indents
		{
			get
			{
				if (_Indents==null)
					_Indents = new Dictionary<int, string>();

				return _Indents;
			}
		}


		#endregion

		#region NewLines

		private bool _IsStartOfLine = true;
		/// <summary>
		/// Used to stop repeated WriteNewLineAndIndent
		/// </summary>
		public bool IsStartOfLine
		{
			get { return _IsStartOfLine; }
			set { _IsStartOfLine = value; }
		}

		private string _NewLine = Environment.NewLine;
		/// <summary>
		/// What to use for a new line
		/// </summary>
		public  string NewLine
		{
			get
			{
				return _NewLine;
			}
			set
			{
				_NewLine = value;
			}
		}

		#endregion

		#region Stream Handing
		/// <summary>
		/// Closes the underlying writer/stream
		/// </summary>
		public void Close()
		{
			this.Writer.Close();
		}

		/// <summary>
		/// Flush the underying writer/stream
		/// </summary>
		public void Flush()
		{
			this.Writer.Flush();
		}
		#endregion

		#region ToString
		/// <summary>
		/// Adds current indent and position information
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return base.ToString() + (this.IsStartOfLine ? " at start of line " : " writing line ") + this.CurrentIndentLevel;
		}
		#endregion
	}
}

