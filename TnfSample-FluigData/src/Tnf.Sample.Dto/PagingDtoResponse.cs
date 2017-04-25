﻿using System.Collections.Generic;
using Tnf.Dto;

namespace Tnf.Sample.Dto
{
    public class PagingDtoResponse<TEntity> : DtoResponseBase<List<TEntity>>
        where TEntity : class
    {
        public PagingDtoResponse() => Data = new List<TEntity>();
        public PagingDtoResponse(List<TEntity> entities) => Data = entities;

        public int Count { get; set; }
        public int TotalHits { get; set; }
        public int Took { get; set; }
    }
}