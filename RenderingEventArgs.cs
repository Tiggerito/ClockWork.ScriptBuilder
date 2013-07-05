using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWork.ScriptBuilder
{
	/// <summary>
	/// Contains the writer so event handlers can write durring rendering
	/// </summary>
	public class RenderingEventArgs :EventArgs 
	{
		private IScriptWriter _Writer;

		/// <summary>
		/// The writer
		/// </summary>
		public IScriptWriter Writer
		{
			get { return _Writer; }
			set { _Writer = value; }
		}

		/// <summary>
		/// passed the writer used for rendering
		/// </summary>
		/// <param name="writer"></param>
		public RenderingEventArgs(IScriptWriter writer)
		{
			Writer = writer;
		}
	}
}
