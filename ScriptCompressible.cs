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

namespace ClockWork.ScriptBuilder
{
    /// <summary>
    /// Changes the object it renders based on the writers Compress value
    /// </summary>
    public class ScriptCompressible : ScriptItem
    {
        #region Constructors
        /// <summary>
        /// Changes the object it renders based on the writers Compress value
        /// </summary>
        /// <param name="full">The object to render if not set to Compress</param>
        /// <param name="compressed">The object to render if set to Compress</param>
        public ScriptCompressible(object full, object compressed)
        {
            this.Full = full;
            this.Compressed = compressed;
        }
        #endregion

        private object _Full;
        /// <summary>
        /// The object to render if not set to Compress
        /// </summary>
        public object Full
        {
            get { return _Full; }
            set { _Full = value; }
        }

        private object _Compressed;
        /// <summary>
        /// The object to render if set to Compress
        /// </summary>
        public object Compressed
        {
            get { return _Compressed; }
            set { _Compressed = value; }
        }

        /// <summary>
        /// render Compressed if Writer.Compress is true, otherwise render Full
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRender(RenderingEventArgs e)
        {
            if (e.Writer.Compress)
            {
                e.Writer.Write(this.Compressed);
            }
            else
            {
                e.Writer.Write(this.Full);
            }
        }
    }
}
