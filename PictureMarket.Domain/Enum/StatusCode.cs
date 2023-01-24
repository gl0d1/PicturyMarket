﻿namespace PicturyMarket.Domain.Enum
{
    public enum StatusCode
    {
        UserNotFound = 0,
        UserAlreadyExists = 1,

        PicturyNotFound = 10,

        OrderNotFound = 20,

        OK = 200,
        InternalServerError = 500,
    }
}