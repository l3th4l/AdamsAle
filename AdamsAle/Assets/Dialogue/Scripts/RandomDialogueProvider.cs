/*
 * MIT License
 * 
 * Copyright (c) 2018 Mathias Wagner Nielsen
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
 
namespace Dialogue
{
    using UnityEngine;

    public sealed class RandomDialogueProvider : DialogueProvider
    {
        [SerializeField]
        private DialogueItem[] items;

        private int lastIndex = -1;

        public override bool TryGetNext(out DialogueItem item)
        {
            item = new DialogueItem();
            switch (this.items.Length)
            {
                case 0:
                    this.lastIndex = -1;
                    return false;

                case 1:
                    if (this.lastIndex == -1)
                    {
                        this.SetText(ref item, 0);
                        return true;
                    }

                    this.lastIndex = -1;
                    return false;

                default:
                    int index = this.lastIndex;

                    do
                    {
                        index = Random.Range(0, this.items.Length);
                    } while (index == this.lastIndex);

                    this.SetText(ref item, index);
                    return true;
            }
        }

        private void SetText(ref DialogueItem item, int index)
        {
            item = this.items[index];
            this.lastIndex = index;
        }
    }
}