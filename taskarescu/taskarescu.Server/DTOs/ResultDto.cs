﻿namespace taskarescu.Server.DTOs
{
    public class ResultDto<TResponse>
    {
        public const string Type = "ResultDto";

        public bool IsSuccess { get; set; }

        public TResponse? Response { get; set; }

        public IEnumerable<string>? Errors { get; set; }

        public ResultDto(bool isSuccess, TResponse response, IEnumerable<string>? errors)
        {
            IsSuccess = isSuccess;
            Response = response;
            Errors = errors;
        }
    }
}
