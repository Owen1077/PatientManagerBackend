﻿using System.Net;

namespace PatientManagerBackend.Core.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message, int errorCode = 99, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError, Exception innerException = null) : base(message, innerException)
        {
            ErrorCode = errorCode;
            HttpStatusCode = httpStatusCode;
        }

        public int ErrorCode { get; protected set; }
        public HttpStatusCode HttpStatusCode { get; protected set; }
    }
}
