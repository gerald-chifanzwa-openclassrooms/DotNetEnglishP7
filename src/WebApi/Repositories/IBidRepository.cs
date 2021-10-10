﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dot.Net.WebApi.Domain;

namespace WebApi.Repositories
{
    public interface IBidRepository
    {
        Task<IReadOnlyCollection<BidList>> Add(BidList bidList);
    }
}