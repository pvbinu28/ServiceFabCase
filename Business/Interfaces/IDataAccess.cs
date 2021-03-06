﻿using DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IDataAccess<T> where T: CaseBaseModel
    {
        Task<List<T>> Get(CaseType currentCaseType);

        Task<T> Get(string id);

        Task<T> Create(T order);

        Task Update(T order);

        Task Delete(string id);
    }
}
