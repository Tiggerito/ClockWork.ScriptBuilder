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
    /// Creates renderable variable names that compress to short names based on a sequence:
    /// a,b,c,...,x,y,z,aa,ab,ac,...ax,ay,az,ba,bb,bc,......zzzzzzz...
    /// </summary>
    public class JsVariableFactory
    {
        /// <summary>
        /// Create a renderable variable names that compress to the next short name based on a sequence:
        /// a,b,c,...,x,y,z,aa,ab,ac,...ax,ay,az,ba,bb,bc,......zzzzzzz...
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ScriptCompressible Create(object name)
        {
            lock (this)
            {
                string compressedName = RecursiveLetterSetName(_Index);
                ScriptCompressible n = new ScriptCompressible(name, compressedName);

                _Index++;

                return n;
            }
        }

        private int _Index = 0;

        private string _LetterSet = "abcdefghijklmnopqrstuvwxyz";
        /// <summary>
        /// Letters to include in the RecursiveLetterSetName algorythm
        /// defaults to
        /// abcdefghijklmnopqrstuvwxyz
        /// </summary>
        public string LetterSet
        {
            get { return _LetterSet; }
            set { _LetterSet = value; }
        }

        /// <summary>
        /// Generates a name based on a sequence of letters like this
        /// a,b,c,...,x,y,z,aa,ab,ac,...ax,ay,az,ba,bb,bc,......zzzzzzz...
        /// 
        /// LetterSet defines the letters used in the sequence
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string RecursiveLetterSetName(int i)
        {
            if (i >= _LetterSet.Length)
            {
                int bi = (int)(i / _LetterSet.Length); // rounds down?

                return RecursiveLetterSetName(bi - 1) + _LetterSet[i - (bi * _LetterSet.Length)];
            }
            else
                return _LetterSet[i].ToString();
        }
    }
}
