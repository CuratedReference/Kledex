﻿using System;

namespace OpenCqrs.Domain
{
    public class AggregateStoreModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}
