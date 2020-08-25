/*
    The MIT License (MIT)

    Copyright (c) 2017 Stéphane Lepin

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using Newtonsoft.Json.Linq;
using OBSWebsocketDotNet.Types;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace OBSWebsocketDotNet
{
    /// <summary>
    /// Holds data about an error from the OBS connection.
    /// </summary>
    public class OBSErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Additional information about the error.
        /// </summary>
        public readonly string? Message;
        /// <summary>
        /// Exception associated with the error, if any.
        /// </summary>
        public readonly Exception? Exception;
        /// <summary>
        /// JSON data associated with the error, if any.
        /// </summary>
        public readonly JToken? Data;

        public OBSErrorEventArgs(string? message, Exception? exception, JToken? data)
        {
            Message = message;
            Exception = exception;
            Data = data;
        }
        public OBSErrorEventArgs(Exception? exception)
            : this(null, exception, null) { }

        public OBSErrorEventArgs(string? message)
            : this(message, null, null) { }
        public OBSErrorEventArgs(string? message, Exception? exception)
            : this(message, exception, null) { }

        public OBSErrorEventArgs(string? message, JToken? data)
            : this(message, null, data) { }

        public OBSErrorEventArgs(Exception? exception, JToken? data)
            : this(null, exception, data) { }

    }

    /// <summary>
    /// Thrown if authentication fails
    /// </summary>
    public class AuthFailureException : Exception
    {
        public AuthFailureException()
        {
        }

        public AuthFailureException(string message) : base(message)
        {
        }

        public AuthFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Thrown when the server responds with an error
    /// </summary>
    public class ErrorResponseException : Exception
    {
        /// <summary>
        /// Response JSON.
        /// </summary>
        public readonly JToken? Response;
        private ErrorResponseException()
            : base() { }

        /// <summary>
        /// Returns a standard <see cref="ErrorResponseException"/> for a response missing a required property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static ErrorResponseException FromMissingProperty(string propertyName, JObject response)
        {
            return new ErrorResponseException($"Response missing '{propertyName}' property.", response);
        }

        /// <summary>
        /// Returns a standard <see cref="ErrorResponseException"/> for a response missing a required property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static ErrorResponseException FromNullResponseObject<T>(JObject? response)
        {
            return new ErrorResponseException($"'{typeof(T).Name}' could not be constructed from the response.", response);
        }

        /// <summary>
        /// Returns a standard <see cref="ErrorResponseException"/> for a response missing a required property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static ErrorResponseException FromNullResponseProperty<T>(string propertyName, JObject? response)
        {
            return new ErrorResponseException($"'{typeof(T).Name}' could not be constructed from the response's property '{propertyName}'.", response);
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        public ErrorResponseException(string message)
            : base(message) { }

        /// <summary>
        /// Creates a new <see cref="ErrorResponseException"/> with a message and the response JSON.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        public ErrorResponseException(string message, JToken? response)
            : base(message)
        {
            Response = response;
        }
        /// <summary>
        /// Creates a new <see cref="ErrorResponseException"/> with a message, response JSON, and inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="response"></param>
        /// <param name="innerException"></param>
        public ErrorResponseException(string message, JToken? response, Exception innerException)
            : base(message, innerException)
        {
            Response = response;
        }

        /// <summary>
        /// Creates a new <see cref="ErrorResponseException"/> with a message and inner <see cref="Exception"/>.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ErrorResponseException(string message, Exception innerException)
            : base(message, innerException) 
        { 
            
        }
    }

    public class SocketErrorResponseException : ErrorResponseException
    {
        public SocketError SocketErrorCode { get; }
        public int ErrorCode => (int)SocketErrorCode;
        public SocketErrorResponseException(string message, SocketException socketException)
            : base(message, socketException)
        {
            SocketErrorCode = socketException.SocketErrorCode;
        }
        public SocketErrorResponseException(SocketException socketException)
           : base(socketException.Message, socketException)
        {
            SocketErrorCode = socketException.SocketErrorCode;
        }
    }
}