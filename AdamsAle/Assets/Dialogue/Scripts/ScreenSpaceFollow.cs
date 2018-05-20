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

    /// <summary>
    /// Sets the position of the UI element to the target's world position projected onto the screen space.
    /// </summary>
    [RequireComponent(typeof(RectTransform)), ExecuteInEditMode]
    internal sealed class ScreenSpaceFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private Vector3 offset;

        private new RectTransform transform;

        private void Awake()
        {
            this.transform = this.GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (this.target)
            {
                var targetPosition = Camera.main.WorldToScreenPoint(this.target.position);
                this.transform.position = targetPosition + this.offset;
            }
        }
    }
}