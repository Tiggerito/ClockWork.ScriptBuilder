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
	/// Renders if syntax:
	/// if (condition) { trueItem } else { falseItem }
	/// if (condition) { trueItem }
	/// </summary>
	public class JsIf : ScriptItem
	{
		#region Constructors

		/// <summary>
		/// Renders if syntax:
		/// if (condition) { trueItem } else { falseItem }
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="trueItem"></param>
		/// <param name="falseItem"></param>
		public JsIf(object condition, object trueItem, object falseItem)
		{
			Initialise(condition, trueItem, falseItem);
		}

		/// <summary>
		/// Renders if syntax:
		/// if (condition) { trueItem } else { falseItem }
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="condition"></param>
		/// <param name="trueItem"></param>
		/// <param name="falseItem"></param>
		public JsIf(ScriptLayout layout, object condition, object trueItem, object falseItem)
			: base(layout)
		{
			Initialise(condition, trueItem, falseItem);
		}

		/// <summary>
		/// Renders if syntax:
		/// if (condition) { trueItem }
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="trueItem"></param>
		public JsIf(object condition, object trueItem)
		{
			Initialise(condition, trueItem, null);
		}

		/// <summary>
		/// Renders if syntax:
		/// if (condition) { trueItem }
		/// </summary>
		/// <param name="layout"></param>
		/// <param name="condition"></param>
		/// <param name="trueItem"></param>
		public JsIf(ScriptLayout layout, object condition, object trueItem)
			: base(layout)
		{
			Initialise(condition, trueItem, null);
		}

		#endregion

		#region Initialisation

		/// <summary>
		/// Shared routine for the constructors
		/// ensures the items are blocks or placed in blocks
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="trueItem"></param>
		/// <param name="falseItem"></param>
		private void Initialise(object condition, object trueItem, object falseItem)
		{
			this.Condition = condition;

			if (trueItem is JsBlock)
				this.TrueBlock = (JsBlock)trueItem;
			else
				this.TrueBlock.Add(trueItem);

			if (falseItem != null)
			{
				if (falseItem is JsBlock)
					this.FalseBlock = (JsBlock)falseItem;
				else
					this.FalseBlock.Add(falseItem);
			}
		}
		#endregion

		#region Data


		private object _Condition;
		/// <summary>
		/// should render as a boolean expression for the if clause
		/// </summary>
		public object Condition
		{
			get
			{
				return _Condition;
			}
			set { _Condition = value; }
		}

		private JsBlock _TrueBlock;

		/// <summary>
		/// contains the script for the success/true clause
		/// </summary>
		public JsBlock TrueBlock
		{
			get
			{
				if (_TrueBlock == null)
					_TrueBlock = new JsBlock(ScriptLayout.InlineBlock);
				return _TrueBlock;
			}
			set { _TrueBlock = value; }
		}
		private JsBlock _FalseBlock;
		/// <summary>
		/// contains the script for the fail/false clause
		/// </summary>
		public JsBlock FalseBlock
		{
			get
			{
				if (_FalseBlock == null)
					_FalseBlock = new JsBlock();
				return _FalseBlock;
			}
			set { _FalseBlock = value; }
		}
		#endregion

		#region Rendering

		/// <summary>
		/// Render the if clause
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

			IScriptWriter writer = e.Writer;


			if (this.Layout == ScriptLayout.Block)
				writer.WriteNewLineAndIndent();

			writer.Write("if (");

			try
			{
				writer.BeginIndent();

				writer.Write(Condition);
			}
			finally
			{
				writer.EndIndent();
			}

			writer.Write(") ");

			writer.Write(this.TrueBlock);


			if (Sb.HasRenderContent(this._FalseBlock))
			{
				writer.Write(" else ");
				writer.Write(this.FalseBlock);
			}
		}
		#endregion
	}
}
